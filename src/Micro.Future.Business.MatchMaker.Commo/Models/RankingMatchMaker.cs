using Micro.Future.Business.MatchMaker.Abstraction.Models;
using Micro.Future.Business.MongoDB.Commo.BizObjects;
using Micro.Future.Business.MongoDB.Commo.Handler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Micro.Future.Business.MatchMaker.Commo.Models
{
    public class RankingMatchMaker : BaseMatchMaker
    {
        private MatcherHandler matcherHandler;
        private double THRESHOLD = -0.01;
        public RankingMatchMaker(MatcherHandler mHandler)
        {
            matcherHandler = mHandler;
        }

        public bool setThreshold(double threshold)
        {
            if (threshold > 0) return false;
            THRESHOLD = threshold;
            return true;
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
                        if (isPriceAcceptable(buyer.ProductPrice, s.ProductPrice, 0.05)
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
                        /*
                        foreach (var filter in midBuyer.HardFilterListForSeller)
                        {
                            if (!filter.check(midBuyer, mid))
                            {
                                filterflag = true;
                                break;
                            }
                        }
                        if (filterflag) continue;
                        foreach (var filter in mid.HardFilterListForBuyer)
                        {
                            if (!filter.check(mid, midBuyer))
                            {
                                filterflag = true;
                                break;
                            }
                        }
                        if (filterflag) continue;
                        foreach (var filter in midBuyer.SoftFilterListForSeller)
                        {
                            filterUtility += filter.violate(midBuyer, mid);
                        }
                        foreach (var filter in mid.SoftFilterListForBuyer)
                        {
                            filterUtility += filter.violate(mid, midBuyer);
                        }
                        */
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


    }
}
