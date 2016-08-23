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
        private decimal THRESHOLD = (decimal)-0.01;
        public RankingMatchMaker(MatcherHandler mHandler)
        {
            matcherHandler = mHandler;
        }

        public bool setThreshold(decimal threshold)
        {
            if (threshold > 0) return false;
            THRESHOLD = threshold;
            return true;
        }

        public void make()
        {
            var res = new List<ChainObject>();
            var listBuyers = listToSortedList(matcherHandler.getReqSortedByAmountDesc(RequirementType.BUYER));
            var listSellers = listToSortedList(matcherHandler.getReqSortedByAmountDesc(RequirementType.SELLER));
            var listMids = listToSortedList(matcherHandler.getReqSortedByAmountDesc(RequirementType.MID));
            var dict = new Dictionary<String, SortedList<int, RequirementObject>>();
            for(int i = 0; i < listSellers.Count; i++) {
                var seller = listSellers.Values[i]; 
                var productName = seller.ProductName;
                if(dict.ContainsKey(productName)) {
                    dict[productName].Add(i, seller);
                }else
                {
                    var slist = new SortedList<int, RequirementObject>();
                    slist.Add(i, seller);
                    dict.Add(productName, slist);
                }
            }
            
            while(listBuyers.Count > 0)
            {
                var buyer = listBuyers.Values[0];
                if(!dict.ContainsKey(buyer.ProductName) || dict[buyer.ProductName].Count == 0)
                {
                    listBuyers.RemoveAt(0);
                    continue;
                }
                var seller = dict[buyer.ProductName].Values[0];
                var mlist = new List<RequirementObject>();
                var prevUtility = calUtility(buyer, seller, mlist);

                for(int i = 0; i < listMids.Count; i++) {
                    var mid = listMids.Values[i];
                    var midKey = listMids.Keys[i];
                    // TODO if mid doesn't satisfy the filter condition, continue
                    mlist.Add(mid);
                    var utility = calUtility(buyer, seller, mlist);
                    var delta = utility - prevUtility;
                    if (delta / prevUtility < THRESHOLD)
                    {
                        mlist.Remove(mid);
                        break;
                    }
                    else
                    {
                        listMids.Remove(midKey);
                    }
                }

                if(mlist.Count > 0)
                {
                    var reqlist = new List<RequirementObject>();
                    reqlist.Add(buyer);
                    foreach(var m in mlist)
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

        private decimal calUtility(RequirementObject buyer, RequirementObject seller, IList<RequirementObject> mids)
        {
            var min = buyer.TradeAmount;
            if (min > seller.TradeAmount) min = seller.TradeAmount;
            foreach(var m in mids)
            {
                if (min > m.TradeAmount) min = m.TradeAmount;
            }
            return min;
        }

        private SortedList<int, RequirementObject> listToSortedList(IList<RequirementObject> list)
        {
            var slist = new SortedList<int, RequirementObject>();
            for(int i = 0; i < list.Count; i++) {
                slist.Add(i, list[i]);
            }
            return slist;
        }

        
    }
}
