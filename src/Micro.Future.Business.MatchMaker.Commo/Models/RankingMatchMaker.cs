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
            if (replacedNodeIndex == chain.ChainLength - 1)
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
            else if (replacedNodeIndex == 0)
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
                    // Filter for BusinessRange (Since v1.2)
                    if (r.BusinessRange != null && !r.BusinessRange.Equals("") && (!r.BusinessRange.Equals(midBuyer.BusinessRange) || !r.BusinessRange.Equals(midSeller.BusinessRange))) continue;
                    // Filter for WarehouseAccount (Since v1.2)
                    if (r.WarehouseAccount != null && !r.WarehouseAccount.Equals("") && (!r.WarehouseAccount.Equals(midBuyer.WarehouseAccount) || !r.WarehouseAccount.Equals(midSeller.WarehouseAccount))) continue;
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

                    var midMatcherFlag = true;
                    while (midMatcherFlag)
                    {
                        var prevCount = mlist.Count;
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
                            // Filter for BusinessRange (Since v1.2)
                            if (mid.BusinessRange == null || (!mid.BusinessRange.Equals("") && (!mid.BusinessRange.Equals(buyer.BusinessRange) || !mid.BusinessRange.Equals(seller.BusinessRange)))) continue;
                            // Filter for WarehouseAccount (Since v1.2)
                            if (mid.WarehouseAccount == null || (!mid.WarehouseAccount.Equals("") && (!mid.WarehouseAccount.Equals(buyer.WarehouseAccount) || !mid.WarehouseAccount.Equals(seller.WarehouseAccount)))) continue;

                            double filterUtility = 0;

                            var midBuyer = buyer;
                            if (mlist.Count > 0)
                            {
                                midBuyer = mlist[mlist.Count - 1];
                            }
                            // filter the requirement soft and hard filters
                            // Direction: Seller To Buyer <==> Up to Down
                            if (!checkHardFilters(mid, midBuyer.Filters, FilterDirectionType.UP)) continue;
                            if (!checkHardFilters(midBuyer, mid.Filters, FilterDirectionType.DOWN)) continue;
                            if (!checkHardFilters(seller, mid.Filters, FilterDirectionType.UP)) continue;
                            if (!checkHardFilters(mid, seller.Filters, FilterDirectionType.DOWN)) continue;

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
                        // 当前遍历已经找不到新的购销节点则退出
                        if (prevCount == mlist.Count) midMatcherFlag = false;
                    }
                    

                    if (mlist.Count > 0)
                    {
                        var reqlist = new List<RequirementObject>();
                        reqlist.Add(seller);
                        foreach (var m in mlist)
                        {
                            reqlist.Add(m);
                        }
                        reqlist.Add(buyer);
                        var chain = new ChainObject(reqlist);
                        res.Add(chain);
                        dict[buyer.ProductName].RemoveAt(0);
                    }
                    listBuyers.RemoveAt(0);
                }
                matcherHandler.AddMatcherChains(res);
                matcherHandler.CallOnChainAdded(res);
            }
            catch(Exception e)
            {
                //TODO LOGGING
            }
            finally
            {
                //TODO
            }
        }

        /// <summary>
        /// AutoMatchRequirements
        /// </summary>
        /// <param name="opUserId"></param>
        /// <param name="requirementIds"></param> 0 denotes 0 or many nodes to match, -1 denotes exact one node, any number greater than 0 represents the actual requirementID 
        /// <param name="fixedLength"></param>
        /// <param name="isPositionFixed"></param>
        /// <returns></returns>
        public ChainObject AutoMatchRequirements(string opUserId, IList<int> requirementIds, int fixedLength = 0, bool isPositionFixed = false, int maxLength = 6)
        {
            var res = new List<RequirementObject>();
            var mapReqs = new Dictionary<int, RequirementObject>();
            var setCompanies = new HashSet<int>();
            var numOnes = 0; // requirementId == -1
            var numManys = 0; // requirementId == 0
            var numReqs = 0; // 非占位符的需求的数量
            decimal minAmount = -1;
            for (int i = 0; i < requirementIds.Count; i++)
            {
                if (requirementIds[i] == 0) numManys += 1;
                else if (requirementIds[i] == -1) {
                    numOnes += 1; 
                }
                else
                {
                    numReqs += 1;
                    var req = matcherHandler.QueryRequirementInfo(requirementIds[i]);
                    mapReqs.Add(requirementIds[i], req);
                    setCompanies.Add(req.EnterpriseId);
                    if (minAmount < 0 || minAmount > req.TradeAmount) minAmount = req.TradeAmount;

                }
            }
            var currentLength = numOnes + numReqs; // 
            var limitLength = maxLength;
            if (fixedLength > 0)
            {
                limitLength = fixedLength;
            }
            if (limitLength < currentLength) return null;

            RequirementObject buyer = null;
            RequirementObject seller = null;
            var sellerId = requirementIds[0];
            var buyerId = requirementIds[requirementIds.Count() - 1];
            // Buyer is missing, Seller is given
            if (sellerId <= 0 && buyerId > 0)
            {
                buyer = mapReqs[buyerId];
                var sellerList = listToSortedList(matcherHandler.getBuyerSellerReqSortedByAmountAsc(RequirementType.SELLER, buyer.ProductName, minAmount));
                if (sellerList.Count == 0) return null;
                var sellerNextId = requirementIds[1];
                if (sellerNextId > 0)
                {
                    var sellerNext = mapReqs[sellerNextId];
                    foreach (var b in sellerList.Values)
                    {
                        if (!isPriceAcceptable(b.ProductPrice, buyer.ProductPrice, PRICE_DEVIANCE) || setCompanies.Contains(b.EnterpriseId)) continue;
                        if (!checkHardFilters(sellerNext, b.Filters, FilterDirectionType.DOWN)) continue;
                        if (!checkHardFilters(b, sellerNext.Filters, FilterDirectionType.UP)) continue;
                        seller = b;
                        break;
                    }
                }
                else
                {
                    seller = sellerList[0];
                }
                if (seller == null) return null;
                requirementIds[0] = seller.RequirementId;
                mapReqs.Add(seller.RequirementId, seller);
                setCompanies.Add(seller.EnterpriseId);
            }
            // Buyer is given, Seller is missing
            else if (sellerId > 0 && buyerId <= 0)
            {
                seller = mapReqs[sellerId];
                var buyerList = listToSortedList(matcherHandler.getBuyerSellerReqSortedByAmountAsc(RequirementType.BUYER, buyer.ProductName, minAmount));
                if (buyerList.Count == 0) return null;
                var buyerPrevId = requirementIds[requirementIds.Count - 2];
                if (buyerPrevId > 0)
                {
                    var buyerPrev = mapReqs[buyerPrevId];
                    foreach (var s in buyerList.Values)
                    {
                        if (!isPriceAcceptable(seller.ProductPrice, s.ProductPrice, PRICE_DEVIANCE) || setCompanies.Contains(s.EnterpriseId)) continue;
                        if (!checkHardFilters(buyerPrev, s.Filters, FilterDirectionType.UP)) continue;
                        if (!checkHardFilters(s, buyerPrev.Filters, FilterDirectionType.DOWN)) continue;
                        buyer = s;
                        break;
                    }
                }
                else
                {
                    buyer = buyerList[0];
                }
                if (buyer == null) return null;
                requirementIds[requirementIds.Count - 1] = buyer.RequirementId;
                mapReqs.Add(buyer.RequirementId, buyer);
                setCompanies.Add(buyer.EnterpriseId);
            }
            else if (buyerId > 0 && sellerId > 0)
            {
                buyer = mapReqs[buyerId];
                seller = mapReqs[sellerId];
            }
            else
            {
                throw new ArgumentException("either buyerId or sellerId should be given.");
            }
            if (!checkValidForBuyerAndSeller(seller) || !checkValidForBuyerAndSeller(buyer))
            {
                throw new ArgumentException("The given seller or buyer is Invalid");
            }
            
            var listMids = listToSortedList(matcherHandler.getMidReqSortedByAmountAsc(RequirementType.MID, minAmount));

            //情况1： 位置固定
            if (isPositionFixed)
            {
                res.Add(buyer);

                for (int i = 1; i < requirementIds.Count - 1; i++)
                {
                    if (requirementIds[i] > 0)
                    {
                        var mid = mapReqs[requirementIds[i]];
                        res.Add(mid);
                    }
                    else
                    {
                        var matched = true;
                        var prev = mapReqs[requirementIds[i - 1]];
                        RequirementObject next = null;
                        if (requirementIds[i + 1] > 0)
                        {
                            next = mapReqs[requirementIds[i + 1]];
                        }
                        do
                        {
                            if (requirementIds[i] == 0 && limitLength - currentLength <= 0) break;
                            matched = false;
                            for (int index = 0; index < listMids.Count; index++)
                            {
                                var midKey = listMids.Keys[index];
                                var mid = listMids.Values[index];
                                if (setCompanies.Contains(mid.EnterpriseId)) continue;
                                // Filter for BusinessRange (Since v1.2)
                                if (mid.BusinessRange == null || (!mid.BusinessRange.Equals("") && (!mid.BusinessRange.Equals(buyer.BusinessRange) || !mid.BusinessRange.Equals(seller.BusinessRange)))) continue;
                                // Filter for WarehouseAccount (Since v1.2)
                                if (mid.WarehouseAccount == null || (!mid.WarehouseAccount.Equals("") && (!mid.WarehouseAccount.Equals(buyer.WarehouseAccount) || !mid.WarehouseAccount.Equals(seller.WarehouseAccount)))) continue;
                                if (!checkHardFilters(mid, prev.Filters, FilterDirectionType.DOWN)) continue;
                                if (!checkHardFilters(prev, mid.Filters, FilterDirectionType.UP)) continue;
                                if (next != null)
                                {
                                    if (!checkHardFilters(next, mid.Filters, FilterDirectionType.DOWN)) continue;
                                    if (!checkHardFilters(mid, next.Filters, FilterDirectionType.UP)) continue;
                                }
                                res.Add(mid);
                                listMids.Remove(midKey);
                                mapReqs.Add(mid.RequirementId, mid);
                                setCompanies.Add(mid.EnterpriseId);
                                prev = mid;
                                matched = true;
                                if (requirementIds[i] == 0) currentLength += 1;
                                if (requirementIds[i] == -1) requirementIds[i] = mid.RequirementId;
                                break;
                            }
                        } while (requirementIds[i] == 0 && matched) ;
                        // if requirementIds[i] == -1 stop after one time
                        if (requirementIds[i] == 0)
                        {
                            numManys -= 1;
                            //当maxLength > 0， 且numManys == 0 && maxLength - currentLength > 0， 表示已经匹配不出这么长的链了
                            if (numManys == 0 && fixedLength - currentLength > 0) return null; 
                        }
                        else if (!matched) // 当requirementIds[i] == -1， 找不到该位置的链则失败退出
                        {
                            return null;
                        }
                    }
                }
                res.Add(seller);
            }
            // 情况2： 位置不固定， 不包含任何占位符
            else
            {
                var givenMidReqsList = new List<RequirementObject>();
                foreach(var id in requirementIds)
                {
                    if (id <= 0) continue;
                    var req = mapReqs[id];
                    if (req.RequirementTypeId == RequirementType.MID) givenMidReqsList.Add(req);
                }
                res.Add(buyer);
                var prev = buyer;
                var flag = true;
                var givenMidReqsSortedList = listToSortedList(givenMidReqsList);
                while(givenMidReqsSortedList.Count > 0 && flag)
                {
                    flag = false;
                    //优先匹配 givenMidReqsList
                    for (int index = 0; index < givenMidReqsSortedList.Count; index++)
                    {
                        var mid = givenMidReqsSortedList.Values[index];
                        var midKey = givenMidReqsSortedList.Keys[index];
                        if (!checkHardFilters(mid, prev.Filters, FilterDirectionType.DOWN)) continue;
                        if (!checkHardFilters(prev, mid.Filters, FilterDirectionType.UP)) continue;
                        prev = mid;
                        flag = true;
                        givenMidReqsSortedList.Remove(midKey);
                        res.Add(mid);
                    }
                    if (!flag)
                    {
                        for (int index = 0; index < listMids.Count; index++)
                        {
                            var midKey = listMids.Keys[index];
                            var mid = listMids.Values[index];
                            if (setCompanies.Contains(mid.EnterpriseId)) continue;
                            // Filter for BusinessRange (Since v1.2)
                            if (mid.BusinessRange ==null || (!mid.BusinessRange.Equals("") && (!mid.BusinessRange.Equals(buyer.BusinessRange) || !mid.BusinessRange.Equals(seller.BusinessRange)))) continue;
                            // Filter for WarehouseAccount (Since v1.2)
                            if (mid.WarehouseAccount == null || (!mid.WarehouseAccount.Equals("") && (!mid.WarehouseAccount.Equals(buyer.WarehouseAccount) || !mid.WarehouseAccount.Equals(seller.WarehouseAccount)))) continue;
                            if (!checkHardFilters(mid, prev.Filters, FilterDirectionType.DOWN)) continue;
                            if (!checkHardFilters(prev, mid.Filters, FilterDirectionType.UP)) continue;
                            res.Add(mid);
                            listMids.Remove(midKey);
                            mapReqs.Add(mid.RequirementId, mid);
                            setCompanies.Add(mid.EnterpriseId);
                            prev = mid;
                            flag = true;
                            break;
                        }
                    }
                    if (limitLength <= res.Count) break;
                }
                if (!flag || limitLength <= res.Count) return null; //当前匹配不到或者超出长度
                
                while(limitLength > res.Count + 1 && flag)
                {
                    flag = false;
                    for (int index = 0; index < listMids.Count; index++)
                    {
                        var midKey = listMids.Keys[index];
                        var mid = listMids.Values[index];
                        if (setCompanies.Contains(mid.EnterpriseId)) continue;
                        // Filter for BusinessRange (Since v1.2)
                        if (mid.BusinessRange == null || (!mid.BusinessRange.Equals("") && (!mid.BusinessRange.Equals(buyer.BusinessRange) || !mid.BusinessRange.Equals(seller.BusinessRange)))) continue;
                        // Filter for WarehouseAccount (Since v1.2)
                        if (mid.WarehouseAccount == null || (!mid.WarehouseAccount.Equals("") && (!mid.WarehouseAccount.Equals(buyer.WarehouseAccount) || !mid.WarehouseAccount.Equals(seller.WarehouseAccount)))) continue;
                        if (!checkHardFilters(mid, prev.Filters, FilterDirectionType.DOWN)) continue;
                        if (!checkHardFilters(prev, mid.Filters, FilterDirectionType.UP)) continue;
                        if (!checkHardFilters(seller, mid.Filters, FilterDirectionType.DOWN)) continue;
                        if (!checkHardFilters(mid, seller.Filters, FilterDirectionType.UP)) continue;
                        res.Add(mid);
                        listMids.Remove(midKey);
                        mapReqs.Add(mid.RequirementId, mid);
                        setCompanies.Add(mid.EnterpriseId);
                        prev = mid;
                        flag = true;
                        break;
                    }
                }
                if (!flag)
                {
                    if (!checkHardFilters(seller, res[res.Count - 1].Filters, FilterDirectionType.DOWN)) return null;
                    if (!checkHardFilters(res[res.Count - 1], seller.Filters, FilterDirectionType.UP)) return null;
                }

                res.Add(seller);
                if (fixedLength > 0 && res.Count != fixedLength) return null;
                if (res.Count < 3) return null;
            }
            var chain = new ChainObject(res);
            var chainId = matcherHandler.AddChain(chain);
            var lockFlag = matcherHandler.LockMatcherChain(chainId, opUserId);
            if (!lockFlag) return null;
            return chain;
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
            if (buyerPrice >= sellerPrice * (1 - deviance) && buyerPrice <= sellerPrice * (1 + deviance))
            {
                return true;
            }
            else return false;
        }

        private bool checkValidForBuyerAndSeller(RequirementObject req)
        {
            if (req == null || req.ProductName == null || req.ProductPrice < 0 || req.RequirementStateId != RequirementStatus.OPEN) return false;
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
                            continue;
                        var reqValue = bson[filter.FilterKey].AsString;
                        var filterValue = filter.FilterValue;
                        if (filterValue == null) continue;
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
