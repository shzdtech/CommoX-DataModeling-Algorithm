﻿using Micro.Future.Business.DataAccess.Commo.CommoObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Micro.Future.Business.DataAccess.Commo.CommonInterface
{
    interface IOrder
    {
        Order submitOrder(Order order);

        Order queryOrder(int orderId);

        IList<Order> queryTradeOrder(int tradeId);

        Order queryOrderByUser(int userId);

        Order queryOrderByRequirement(int requirementId);

        Order queryOrderByEnterprise(int enterpriseId);

        bool updateOrderState(int orderId, string executUserName, string state);

    }
}
