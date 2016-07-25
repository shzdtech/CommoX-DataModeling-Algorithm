using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Micro.Future.Business.DataAccess.Commo.CommoObject
{
    /// <summary>
    /// 需求对象
    /// </summary>
    public class Requirement
    {
        [Key]
        public int RequirementId { get; set; }
        public int UserId { get; set; }
        public int EnterpriseId { get; set; }
        public int ProductId { get; set; }
        /// <summary>
        /// 需求类型：如出货、出钱、补贴、汇票等
        /// </summary>
        public int RequirementTypeId { get; set; }
        public int ProductPrice { get; set; }
        public decimal ProductQuota { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime ModifyTime { get; set; }
        public int State { get; set; }
    }



}
