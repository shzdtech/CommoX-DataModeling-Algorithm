using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Micro.Future.Business.DataAccess.Commo.CommoObject
{
    /// <summary>
    /// 支付方式表
    /// </summary>
    public class PaymentMethod
    {
        [Key]
        public int PaymentMethodId { get; set; }
        /// <summary>
        /// 支付方式
        /// </summary>
        public string PaymentMethodName { get; set; }
        public int StateId { get; set; }
    }
}
