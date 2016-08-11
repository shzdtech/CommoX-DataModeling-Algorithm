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
        public Order submitOrder(Order order)
        {
            using (var db = new CommoXContext())
            {
                db.Orders.Add(order);
                int result = db.SaveChanges();
                if (result > 0)
                    return order;
                
                else
                    return null;
            }
        }
    
        public Order queryOrder(int orderId)
        {
            using (var db = new CommoXContext())
            {
                var result = db.Orders.SingleOrDefault(t => t.OrderId == orderId);
                return result;

            }
        }

        public Order queryOrderByUser(int userId)
        {
            using (var db = new CommoXContext())
            {
                var result = db.Orders.SingleOrDefault(t => t.UserId == userId);
                return result;

            }
        }

        public Order queryOrderByRequirement(int requirementId)
        {
            using (var db = new CommoXContext())
            {
                var result = db.Orders.SingleOrDefault(t => t.RequirementId == requirementId);
                return result;

            }
        }

        public Order queryOrderByEnterprise(int enterpriseId)
        {
            using (var db = new CommoXContext())
            {
                var result = db.Orders.SingleOrDefault(t => t.EnterpriseId == enterpriseId);
                return result;

            }
        }

        public bool updateOrderState(int orderId, string executUserName, string state)
        {
            using (var db = new CommoXContext())
            {
                var order = db.Orders.SingleOrDefault(t => t.OrderId == orderId);
                if (order != null)
                {
                    order.ExecuteUsername = executUserName;
                    order.OrderState = state;
                    order.ModifyTime = DateTime.Now;
                    int result = db.SaveChanges();
                    if (result > 0)
                        return true;
                    else
                        return false;
                }
                return false;
            }
        }

        public IList<Order> queryTradeOrder(int tradeId)
        {
            using (var db = new CommoXContext())
            {
                var result = db.Orders.Where(t => t.TradeId == tradeId).ToList();
                if (result != null)
                    return result;
                else
                    return null;
            }
        }
    }
}
