using Micro.Future.Business.MatchMaker.Abstraction.Models;
using Micro.Future.Business.MongoDB.Commo.BizObjects;
using Micro.Future.Business.MongoDB.Commo.Handler;
using Micro.Future.Business.MongoDB.Commo.MongoInterface;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Micro.Future.Business.MatchMaker.Commo.Models
{
    public class RankingMatchMaker : BaseMatchMaker
    {
        private IMatcher matcherHandler;
        private double THRESHOLD = -0.01;
        private double PRICE_DEVIANCE = 0.05;
        private char sep = ',';
        public RankingMatchMaker(IMatcher mHandler)
        {
            matcherHandler = mHandler;
        }

        public bool setThreshold(double threshold)
        {
            if (threshold > 0) return false;
            THRESHOLD = threshold;
            return true;
        }

        public IList<RequirementObject> FindReplacedRequirementsForChain(int chainId, int replacedNodeIndex, int topN = 5)
        {
            var res = new List<RequirementObject>();
            // the replaced chain status should be LOCKED
            var chain = matcherHandler.GetChain(chainId);
            if (chain.ChainStateId != ChainStatus.LOCKED) return null;
            // invalid chain
            if (chain.ChainLength != chain.RequirementIdChain.Count) return null;
            //Replace Buyer
            if (replacedNodeIndex == 0)
            {
                var listBuyers = matcherHandler.getReqSortedByAmountDesc(RequirementType.BUYER);
                if (chain.RequirementIdChain.Count < 3 && chain.RequirementIdChain.Count < chain.ChainLength) return null;
                var seller = matcherHandler.QueryRequirementInfo(chain.RequirementIdChain[chain.ChainLength - 1]);
                var midSeller = matcherHandler.QueryRequirementInfo(chain.RequirementIdChain[1]);
                foreach (var r in listBuyers)
                {
                    if (checkValidForBuyerAndSeller(r) &&
                        r.ProductName == seller.ProductName &&
                        isPriceAcceptable(r.ProductPrice, seller.ProductPrice, PRICE_DEVIANCE) &&
                        checkHardFilters(r, midSeller.Filters, FilterDirectionType.UP) &&
                        checkHardFilters(midSeller, r.Filters, FilterDirectionType.DOWN)
                        )
                    {
                        res.Add(r);
                        if (res.Count == topN) break;
                    }
                }
            }
            //Replace Seller
            else if (replacedNodeIndex == chain.ChainLength - 1)
            {
                var listSeller = matcherHandler.getReqSortedByAmountDesc(RequirementType.SELLER);
                if (chain.RequirementIdChain.Count < 3 && chain.RequirementIdChain.Count < chain.ChainLength) return null;
                var buyer = matcherHandler.QueryRequirementInfo(chain.RequirementIdChain[0]);
                var midBuyer = matcherHandler.QueryRequirementInfo(chain.RequirementIdChain[chain.ChainLength - 2]);
                foreach (var r in listSeller)
                {
                    if (checkValidForBuyerAndSeller(r) &&
                        r.ProductName == buyer.ProductName &&
                        isPriceAcceptable(buyer.ProductPrice, r.ProductPrice, PRICE_DEVIANCE) &&
                        checkHardFilters(r, midBuyer.Filters, FilterDirectionType.DOWN) &&
                        checkHardFilters(midBuyer, r.Filters, FilterDirectionType.UP)
                        )
                    {
                        res.Add(r);
                        if (res.Count == topN) break;
                    }
                }
            }
            else
            {
                var listMid = matcherHandler.getReqSortedByAmountDesc(RequirementType.MID);
                if (chain.RequirementIdChain.Count < 3 && chain.RequirementIdChain.Count < chain.ChainLength) return null;
                var midSeller = matcherHandler.QueryRequirementInfo(chain.RequirementIdChain[replacedNodeIndex + 1]);
                var midBuyer = matcherHandler.QueryRequirementInfo(chain.RequirementIdChain[replacedNodeIndex - 1]);
                foreach (var r in listMid)
                {
                    if (checkHardFilters(r, midBuyer.Filters, FilterDirectionType.DOWN) &&
                        checkHardFilters(midBuyer, r.Filters, FilterDirectionType.UP) &&
                        checkHardFilters(r, midSeller.Filters, FilterDirectionType.UP) &&
                        checkHardFilters(midSeller, r.Filters, FilterDirectionType.DOWN)
                        )
                    {
                        res.Add(r);
                        if (res.Count == topN) break;
                    }
                }
            }        
            return res;
        }

        public void make()
        {
            try
            {
                var res = new List<ChainObject>();
                var listBuyers = listToSortedList(matcherHandler.getReqSortedByAmountDesc(RequirementType.BUYER));
                var listSellers = listToSortedList(matcherHandler.getReqSortedByAmountDesc(RequirementType.SELLER));
                var listMids = listToSortedList(matcherHandler.getReqSortedByAmountDesc(RequirementType.MID));
                var dict = new Dictionary<String, SortedList<int, RequirementObject>>();
                for (int i = 0; i < listSellers.Count; i++)
                {
                    var seller = listSellers.Values[i];
                    if (!checkValidForBuyerAndSeller(seller)) continue;
                    var productName = seller.ProductName;
                    if (dict.ContainsKey(productName))
                    {
                        dict[productName].Add(i, seller);
                    }
                    else
                    {
                        var slist = new SortedList<int, RequirementObject>();
                        slist.Add(i, seller);
                        dict.Add(productName, slist);
                    }
                }

                while (listBuyers.Count > 0)
                {
                    var buyer = listBuyers.Values[0];
                    if (!checkValidForBuyerAndSeller(buyer) || !dict.ContainsKey(buyer.ProductName) || dict[buyer.ProductName].Count == 0)
                    {
                        listBuyers.RemoveAt(0);
                        continue;
                    }
                    RequirementObject seller = null;
                    // filter seller 
                    // 1. seller should has an acceptable sell price
                    // 2. seller cannot be the same company with buyer
                    for (var i = 0; i < dict[buyer.ProductName].Count; i++)
                    {
                        var s = dict[buyer.ProductName].Values[i];
                        if (isPriceAcceptable(buyer.ProductPrice, s.ProductPrice, PRICE_DEVIANCE)
                            && s.EnterpriseId != buyer.EnterpriseId)
                        {
                            seller = s;
                            break;
                        }
                    }
                    // no matched seller currently
                    if (seller == null)
                    {
                        listBuyers.RemoveAt(0);
                        continue;
                    }
                    var mlist = new List<RequirementObject>();
                    var prevUtility = calUtility(buyer, seller, mlist);

                    // TODO 当前未考虑中间商过滤条件的顺序
                    for (int i = 0; i < listMids.Count && prevUtility > 0; i++)
                    {
                        var mid = listMids.Values[i];
                        var midKey = listMids.Keys[i];

                        // TODO if mid doesn't satisfy the filter condition, continue
                        bool filterflag = false;
                        if (mid.EnterpriseId == buyer.EnterpriseId || mid.EnterpriseId == seller.EnterpriseId)
                        {
                            filterflag = true;
                        }
                        foreach (var m in mlist)
                        {
                            if (m.EnterpriseId == mid.EnterpriseId) filterflag = true;
                        }
                        if (filterflag) continue;

                        double filterUtility = 0;

                        var midBuyer = buyer;
                        if (mlist.Count > 0)
                        {
                            midBuyer = mlist[mlist.Count - 1];
                        }
                        // filter the requirement soft and hard filters
                        // Direction: Buyer To Seller <==> Up to Down
                        if (!checkHardFilters(mid, midBuyer.Filters, FilterDirectionType.DOWN)) continue;
                        if (!checkHardFilters(midBuyer, mid.Filters, FilterDirectionType.UP)) continue;
                        if (!checkHardFilters(seller, mid.Filters, FilterDirectionType.DOWN)) continue;
                        if (!checkHardFilters(mid, seller.Filters, FilterDirectionType.UP)) continue;
                        
                        mlist.Add(mid);
                        var utility = calUtility(buyer, seller, mlist);
                        var delta = utility - prevUtility - filterUtility;
                        if (delta / prevUtility < THRESHOLD)
                        {
                            mlist.Remove(mid);
                            break;
                        }
                        else
                        {
                            listMids.Remove(midKey);
                        }
                        prevUtility = utility;
                    }

                    if (mlist.Count > 0)
                    {
                        var reqlist = new List<RequirementObject>();
                        reqlist.Add(buyer);
                        foreach (var m in mlist)
                        {
                            reqlist.Add(m);
                        }
                        reqlist.Add(seller);
                        var chain = new ChainObject(reqlist);
                        res.Add(chain);
                    }
                    listBuyers.RemoveAt(0);
                    dict[buyer.ProductName].RemoveAt(0);
                }
                matcherHandler.AddMatcherChains(res);
                matcherHandler.CallOnChainAdded(res);
            }
            catch
            {
                //TODO LOGGING
            }
            finally
            {
                //TODO
            }
        }

        private double calUtility(RequirementObject buyer, RequirementObject seller, IList<RequirementObject> mids)
        {
            var min = buyer.TradeAmount;
            if (min > seller.TradeAmount) min = seller.TradeAmount;
            foreach(var m in mids)
            {
                if (min > m.TradeAmount) min = m.TradeAmount;
            }
            return (double)min;
        }

        private SortedList<int, RequirementObject> listToSortedList(IList<RequirementObject> list)
        {
            var slist = new SortedList<int, RequirementObject>();
            for(int i = 0; i < list.Count; i++) {
                slist.Add(i, list[i]);
            }
            return slist;
        }

        private bool isPriceAcceptable(double buyerPrice, double sellerPrice, double deviance)
        {
            if (buyerPrice >= sellerPrice * (1 - deviance) || buyerPrice <= sellerPrice * (1 + deviance))
            {
                return true;
            }
            else return false;
        }

        private bool checkValidForBuyerAndSeller(RequirementObject req)
        {
            if (req.ProductName == null || req.ProductPrice < 0) return false;
            return true;
        }

        private bool checkFilterOperation(FilterOperationType opType, String filterVal, String reqVal, FilterValueType valueType)
        {
            switch (opType)
            {
                case FilterOperationType.IN:
                    if (valueType != FilterValueType.SEQUENCE_STRING)
                        throw new Exception("invalid filter: " + opType.ToString() + " +  " + valueType.ToString());
                    foreach(var s in filterVal.Split(','))
                    {
                        if (s.Equals(reqVal)) return true;
                    }
                    break;
                case FilterOperationType.NIN:
                    if (valueType != FilterValueType.SEQUENCE_STRING)
                        throw new Exception("invalid filter: " + opType.ToString() + " +  " + valueType.ToString());
                    var flag = true;
                    foreach (var s in filterVal.Split(','))
                    {
                        if (s.Equals(reqVal)) flag = false;
                    }
                    return flag;
                case FilterOperationType.EQ:
                    if(valueType != FilterValueType.NUMBER && valueType != FilterValueType.STRING)
                        throw new Exception("invalid filter: " + opType.ToString() + " +  " + valueType.ToString());
                    if (valueType == FilterValueType.NUMBER)
                    {
                        if (Convert.ToDouble(filterVal) == Convert.ToDouble(reqVal)) return true;
                    }
                    else
                    {
                        if (filterVal.Equals(reqVal)) return true;
                    }
                    break;
                case FilterOperationType.NE:
                    if (valueType != FilterValueType.NUMBER && valueType != FilterValueType.STRING)
                        throw new Exception("invalid filter: " + opType.ToString() + " +  " + valueType.ToString());
                    if (valueType == FilterValueType.NUMBER)
                    {
                        if (Convert.ToDouble(filterVal) != Convert.ToDouble(reqVal)) return true;
                    }
                    else
                    {
                        if (!filterVal.Equals(reqVal)) return true;
                    }
                    break;
                case FilterOperationType.LT:
                    if (valueType != FilterValueType.NUMBER)
                        throw new Exception("invalid filter: " + opType.ToString() + " +  " + valueType.ToString());
                    if (Convert.ToDouble(reqVal) < Convert.ToDouble(filterVal)) return true;
                    break;
                case FilterOperationType.LE:
                    if (valueType != FilterValueType.NUMBER)
                        throw new Exception("invalid filter: " + opType.ToString() + " +  " + valueType.ToString());
                    if (Convert.ToDouble(reqVal) <= Convert.ToDouble(filterVal)) return true;
                    break;
                case FilterOperationType.GT:
                    if (valueType != FilterValueType.NUMBER)
                        throw new Exception("invalid filter: " + opType.ToString() + " +  " + valueType.ToString());
                    if (Convert.ToDouble(reqVal) > Convert.ToDouble(filterVal)) return true;
                    break;
                case FilterOperationType.GE:
                    if (valueType != FilterValueType.NUMBER)
                        throw new Exception("invalid filter: " + opType.ToString() + " +  " + valueType.ToString());
                    if (Convert.ToDouble(reqVal) >= Convert.ToDouble(filterVal)) return true;
                    break;           
            }
            return false;
        }

        private bool checkHardFilters(RequirementObject req, IList<RequirementFilter> filters, FilterDirectionType direct)
        {
            // TODO
            var bson = req.ToBsonDocument();
            if(filters == null) return true;
            foreach(var filter in filters)
            {
                //ignore soft filters
                if (filter.IsSoftFilter) continue;
                try
                {
                    if (filter.FilterDirectionTypeId == FilterDirectionType.BIDIRECT ||
                        filter.FilterDirectionTypeId == direct)
                    {
                        if (!bson.Contains(filter.FilterKey) || bson[filter.FilterKey] == null)
                            return false;
                        var reqValue = bson[filter.FilterKey].AsString;
                        var filterValue = filter.FilterValue;
                        if(!checkFilterOperation(filter.OperationTypeId, filterValue, reqValue, filter.FilterValueTypeId))
                            return false;
                    }
                }
                catch (Exception e)
                {
                    // invalid filter
                    continue;
                }
                
            }
            return true;
        }

    }
}
