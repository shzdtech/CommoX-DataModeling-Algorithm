﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Micro.Future.Business.DataAccess.Commo.CommoObject
{
    /// <summary>
    /// 订单状态表
    /// </summary>
    public class OrderState
    {
        public int OrderStateId { get; set; }
        public string OrderStateName { get; set; }
        public int StateId { get; set; }
    }
}
