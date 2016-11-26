﻿using Micro.Future.Business.DataAccess.Commo.CommonInterface;
using Micro.Future.Business.DataAccess.Commo.CommoObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Micro.Future.Business.DataAccess.Commo.CommoHandler
{
    public class OrderHandler : IOrder
    {
        private CommoXContext db = null;
        public OrderHandler(CommoXContext dbContext)
        {
            db = dbContext;
        }

        public Order submitOrder(Order order)
        {
            db.Orders.Add(order);
            int result = db.SaveChanges();
            if (result > 0)
                return order;
            else
                return null;
        }

        public Order queryOrder(int orderId)
        {
            var result = db.Orders.FirstOrDefault(t => t.OrderId == orderId);
            return result;
        }

        public Order queryOrderByUser(string userId)
        {
            var result = db.Orders.FirstOrDefault(t => t.UserId == userId);
            return result;
        }

        public Order queryOrderByRequirement(int requirementId)
        {
            var result = db.Orders.FirstOrDefault(t => t.RequirementId == requirementId);
            return result;
        }

        public Order queryOrderByEnterprise(int enterpriseId)
        {
            var result = db.Orders.FirstOrDefault(t => t.EnterpriseId == enterpriseId);
            return result;
        }

        public bool updateOrderState(int orderId, string executUserId, int state)
        {
            var order = db.Orders.FirstOrDefault(t => t.OrderId == orderId);
            if (order != null)
            {
                order.ExecuteUserId = executUserId;
                order.OrderStateId = state;
                order.ModifyTime = DateTime.Now;

                if(state == 10)
                {
                    //完成
                    order.CompleteTime = DateTime.Now;
                }

                int result = db.SaveChanges();
                if (result > 0)
                    return true;
                else
                    return false;
            }
            return false;
        }

        public IList<Order> queryTradeOrder(int tradeId)
        {
            var result = db.Orders.Where(t => t.TradeId == tradeId).ToList();
            if (result != null)
                return result;
            else
                return null;
        }

        public IList<Order> queryOrdersByEnterprise(int enterpriseId, int? state)
        {
            var result = db.Orders.Where(t => t.EnterpriseId == enterpriseId);
            if (state.HasValue)
                result = result.Where(f => f.OrderStateId == state);
            return result.ToList();
        }
    }
}
