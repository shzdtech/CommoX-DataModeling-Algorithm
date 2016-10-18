﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Micro.Future.Business.DataAccess.Commo.CommoObject
{
    public class Acceptance
    {
        /// <summary>
        /// 承兑汇票ID
        /// </summary>
        [Key]
        public int AcceptanceId { get; set; }
        /// <summary>
        /// 承兑汇票金额
        /// 单位：万元
        /// </summary>
        public double Amount { get; set; }
        /// <summary>
        /// 到期日
        /// </summary>
        public DateTime DueDate { get; set; }
        /// <summary>
        /// 开票银行
        /// </summary>
        public string BankName { get; set; }
        /// <summary>
        /// 票据类型
        /// 分为：电票/纸票
        /// </summary>
        public string AcceptanceType { get; set; }
        /// <summary>
        /// 出票时间
        /// </summary>
        public DateTime DrawTime { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateTime { get; set; }

    }
}
