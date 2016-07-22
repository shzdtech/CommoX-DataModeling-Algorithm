using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Micro.Future.Business.DataAccess.Commo.CommoObject
{
    /// <summary>
    /// 产品类型表
    /// </summary>
    public class ProductType
    {
        [Key]
        public int ProductTypeId { get; set; }
        /// <summary>
        /// 产品类型
        /// </summary>
        public string ProductTypeName { get; set; }
        /// <summary>
        /// 产品大类
        /// </summary>
        public int ParentId { get; set; }
        public int StateId { get; set; }
    }
}
