using Micro.Future.Business.DataAccess.Commo.CommonInterface;
using Micro.Future.Business.DataAccess.Commo.CommoObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Micro.Future.Business.DataAccess.Commo.CommoHandler
{
    public class OrderHandler : IOrder
    {
        public bool submitOrder(Order order)
        {
            using (var db = new CommoXContext())
            {
                db.Orders.Add(order);
                int result = db.SaveChanges();
                return result > 0;
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


    }
}
