using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Micro.Future.Business.DataAccess.Commo.CommoObject
{
    /// <summary>
    /// 订单信息表
    /// </summary>
    public class Order
    {
        public int OrderId { get; set; }
        /// <summary>
        /// 关联交易
        /// </summary>
        public int TradeId { get; set; }
        /// <summary>
        /// 在本次交易链中所在的次序，如TradeChain：A-B-C-D， B的sequence为2
        /// </summary>
        public int TradeSequence { get; set; }
        /// <summary>
        /// 关联需求
        /// </summary>
        public int RequirementId { get; set; }
        /// <summary>
        /// 关联企业
        /// </summary>
        public int EnterpriseId { get; set; }
        /// <summary>
        /// 关联货物
        /// </summary>
        public int ProductId { get; set; }
        /// <summary>
        /// 支付方式
        /// </summary>
        public int PaymentMethodId { get; set; }
        /// <summary>
        /// 补贴
        /// </summary>
        public double Subsidy { get; set; }
        /// <summary>
        /// 金额
        /// </summary>
        public double Amount { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        public int Quota { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime ModifyTime { get; set; }
        /// <summary>
        /// 完成时间
        /// </summary>
        public DateTime CompleteTime { get; set; }
        /// <summary>
        /// 订单状态
        /// </summary>
        public int OrderStateId { get; set; }

    }
}
