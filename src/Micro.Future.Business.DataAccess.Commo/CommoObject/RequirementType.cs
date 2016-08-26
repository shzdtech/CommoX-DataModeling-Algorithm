using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Micro.Future.Business.DataAccess.Commo.CommoObject
{
    /// <summary>
    /// 需求的类型：出货、出钱等
    /// </summary>
    public class RequirementType
    {
        [Key]
        public int RequirementTypeId { get; set; }
        public string RequirementTypeName { get; set; }
        public int StateId { get; set; }

    }

    public class RequirementState
    {
        [Key]
        public int RequirementStateId { get; set; }
        public string RequirementStateName { get; set; }
    }


}
