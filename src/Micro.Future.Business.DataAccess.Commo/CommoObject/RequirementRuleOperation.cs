using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Micro.Future.Business.DataAccess.Commo.CommoObject
{
    public class RequirementRuleOperation
    {
        [Key]
        public int OperationId { get; set; }
        public string OperationName { get; set; }
        public int State { get; set; }
    }
}
