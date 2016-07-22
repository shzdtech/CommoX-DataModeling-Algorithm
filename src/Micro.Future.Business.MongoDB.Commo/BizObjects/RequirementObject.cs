using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Micro.Future.Business.MongoDB.Commo.BizObjects
{
    /// <summary>
    /// 需求对象
    /// </summary>
    public class RequirementObject
    {
        public int RequirementId { get; set; }
        public int UserId { get; set; }
        public int EnterpriseId { get; set; }
        public int ProductId { get; set; }
        public int RequirementTypeId { get; set; }
        public int ProductPrice { get; set; }
        public decimal ProductQuota { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime ModifyTime { get; set; }
        public int RequirementStateId { get; set; }
    }

    /// <summary>
    /// 需求撮合规则
    /// </summary>
    public class RequirementFilter
    {
        public int FilterId { get; set; }
        public int RequirementId { get; set; }
        public string FilterKey { get; set; }
        public int OperationId { get; set; }
        public string FilterValue { get; set; }
        public int StateId { get; set; }
    }
}
