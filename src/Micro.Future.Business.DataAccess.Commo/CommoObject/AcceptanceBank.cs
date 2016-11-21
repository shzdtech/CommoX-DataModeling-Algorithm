using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Micro.Future.Business.DataAccess.Commo.CommoObject
{
    /// <summary>
    /// 承兑汇票贴票行
    /// </summary>
    public class AcceptanceBank
    {
        [Key]
        public int BankId { get; set; }

        /// <summary>
        /// 贴票行
        /// </summary>
        public string BankName { get; set; }

        /// <summary>
        /// 1=大票/2=小票
        /// </summary>
        public int AcceptanceType { get; set; }

        /// <summary>
        /// 银行报价（贴现利率）
        /// </summary>
        public double BankPrice { get; set; }

        /// <summary>
        /// 开票行类别（1=国股/2=城商/3=农商）
        /// </summary>
        public int BankType { get; set; }

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
