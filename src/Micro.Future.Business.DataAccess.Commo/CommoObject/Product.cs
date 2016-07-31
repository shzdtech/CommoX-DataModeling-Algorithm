using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Micro.Future.Business.DataAccess.Commo.CommoObject
{
    /// <summary>
    /// 产品信息表
    /// </summary>
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        /// <summary>
        /// 产品名称
        /// </summary>
        public string ProductName { get; set; }
        /// <summary>
        /// 产品类型
        /// </summary>
        public string ProductTypeId { get; set; }
        /// <summary>
        /// 产品价格
        /// </summary>
        public double Price { get; set; }
        /// <summary>
        /// 产品限额量
        /// </summary>
        public double LimitedQuota { get; set; }
        public int StateId { get; set; }
    }
}
