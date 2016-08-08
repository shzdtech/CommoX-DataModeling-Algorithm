using Micro.Future.Business.DataAccess.Commo.CommonInterface;
using Micro.Future.Business.DataAccess.Commo.CommoObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Micro.Future.Business.DataAccess.Commo.CommoHandler
{
    public class TradeHandler : ITrade
    {

        public Trade submitTrade(Trade trade)
        {
            using (var db = new CommoXContext())
            {
                db.Trades.Add(trade);
                int result = db.SaveChanges();
                if (result > 0)
                    return trade;
                else
                    return null;
            }
        }

        public TradeChain submitTradeChain(TradeChain tradechain)
        {
            using (var db = new CommoXContext())
            {
                db.TradeChains.Add(tradechain);
                int result = db.SaveChanges();
                if (result > 0)
                    return tradechain;
                else
                    return null;
            }
        }
        public Trade queryTrade(int tradeId)
        {
            using (var db = new CommoXContext())
            {
                var result = db.Trades.SingleOrDefault(t => t.TradeId == tradeId);
                return result;

            }
        }

        public IEnumerable<TradeChain> queryTradeChain(int tradeId)
        {
            using (var db = new CommoXContext())
            {
                var result = db.TradeChains.Where(tc => tc.TradeId == tradeId).ToList();
                return result;
            }
        }

        public bool updateTradeState(int tradeId, string state)
        {
            using (var db = new CommoXContext())
            {
                var trade = db.Trades.SingleOrDefault(t => t.TradeId == tradeId);
                trade.CurrentState = state;
                int result = db.SaveChanges();
                if (result > 0)
                    return true;
                else
                    return false;
            }
               
        }


    }
}
