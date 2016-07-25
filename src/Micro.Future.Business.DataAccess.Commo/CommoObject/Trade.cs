using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Micro.Future.Business.DataAccess.Commo.CommoObject
{
    /// <summary>
    /// 交易信息表
    /// </summary>
    public class Trade
    {
        [Key]
        public int TradeId { get; set; }
        /// <summary>
        /// 交易标题
        /// </summary>
        public string TradeTitle { get; set; }
        /// <summary>
        /// 交易费
        /// </summary>
        public double TradeFee { get; set; }
        /// <summary>
        /// 交易时间
        /// </summary>
        public DateTime TradeTime { get; set; }
        /// <summary>
        /// 交易资金量
        /// </summary>
        public double TradeAmount { get; set; }
        /// <summary>
        /// 交易货物量
        /// </summary>
        public double TradeQuota { get; set; }
        /// <summary>
        /// 交易补贴量
        /// </summary>
        public double TradeSubsidy { get; set; }
        /// <summary>
        /// 参与企业数
        /// </summary>
        public int ParticipatorCount { get; set; }

    }

}
