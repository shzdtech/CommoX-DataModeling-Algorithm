using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Micro.Future.Business.DataAccess.Commo.CommoObject
{

    /// <summary>
    /// 企业类型、性质表
    /// </summary>
    public class BusinessType
    {
        [Key]
        public int BusinessTypeId { get; set; }
        /// <summary>
        /// 企业类型
        /// </summary>
        public string BusinessTypeName { get; set; }
        /// <summary>
        /// 大类ID
        /// </summary>
        public int ParentId { get; set; }
        public int StateId { get; set; }

    }
}
