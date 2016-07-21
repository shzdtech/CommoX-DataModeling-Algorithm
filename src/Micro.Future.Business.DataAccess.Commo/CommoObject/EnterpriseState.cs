using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Micro.Future.Business.DataAccess.Commo.CommoObject
{
    /// <summary>
    /// 企业状态表
    /// </summary>
    public class EnterpriseState
    {
        [Key]
        public int StateId { get; set; }
        /// <summary>
        /// 状态情况
        /// </summary>
        public string StateName { get; set; }
    }
}
