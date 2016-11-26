﻿using Micro.Future.Business.DataAccess.Commo.CommonInterface;
using Micro.Future.Business.DataAccess.Commo.CommoObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Micro.Future.Business.DataAccess.Commo.CommoHandler
{
    public class TradeHandler : ITrade
    {
        private CommoXContext db = null;

        public TradeHandler(CommoXContext dbContext)
        {
            db = dbContext;
        }

        public Trade submitTrade(Trade trade)
        {
                db.Trades.Add(trade);
                int result = db.SaveChanges();
                if (result > 0)
                    return trade;
                else
                    return null;
        }

        public Trade queryTrade(int tradeId)
        {
            return db.Trades.FirstOrDefault(t => t.TradeId == tradeId);
        }

        public bool updateTradeState(int tradeId, string state)
        {
            var trade = db.Trades.FirstOrDefault(t => t.TradeId == tradeId);
            trade.CurrentState = state;

            var orders = db.Orders.Where(f => f.TradeId == tradeId);
            foreach(var o in orders)
            {
                int newState = int.Parse(state);

                o.OrderStateId = newState;
                o.ModifyTime = DateTime.Now;
                if (newState == 10)
                {
                    //完成
                    o.CompleteTime = DateTime.Now;
                }
            }
            
            int result = db.SaveChanges();
            if (result > 0)
                return true;
            else
                return false;
        }

        public IList<Trade> queryAllTrade(string tradeState)
        {
            return db.Trades.Where(f => f.CurrentState == tradeState).OrderByDescending(f=>f.TradeId).ToList();
        }

        public IList<Trade> queryTradesByEnterprise(int enterpriseId, string tradeState)
        {
            var query = from o in db.Orders
                        join t in db.Trades on o.TradeId equals t.TradeId
                        where o.EnterpriseId == enterpriseId
                        select t;

            if (!string.IsNullOrWhiteSpace(tradeState))
                query = query.Where(f => f.CurrentState == tradeState);

            //排序
            query = query.OrderByDescending(t => t.TradeId);

            return query.ToList();
        }
    }
}
