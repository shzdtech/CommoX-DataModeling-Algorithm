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
        public int RequirementId { get; set; }
        public int RequirementType { get; set; }
        public int ProductId { get; set; }
        public decimal Price { get; set; }
        public decimal Quota { get; set; }
        public decimal TotalPrice { get; set; }
        public int FromRequirementId { get; set; }
        public DateTime CreateTime { get; set; }

    }

}
