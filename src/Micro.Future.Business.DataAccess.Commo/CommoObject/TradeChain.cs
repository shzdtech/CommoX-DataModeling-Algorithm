using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Micro.Future.Business.DataAccess.Commo.CommoObject
{
    /// <summary>
    /// 交易链信息表
    /// </summary>
    public class TradeChain
    {
        public int TradeChainId { get; set; }
        /// <summary>
        /// 交易ID
        /// </summary>
        public int TradeId { get; set; }
        /// <summary>
        /// 本交易链中的序列号
        /// </summary>
        public int TradeSequence { get; set; }
        /// <summary>
        /// 需求Id
        /// </summary>
        public int RequirementId { get; set; }

        public int CreateTime { get; set; }



    }
}
