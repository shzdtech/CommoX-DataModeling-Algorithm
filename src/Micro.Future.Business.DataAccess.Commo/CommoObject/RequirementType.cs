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
        public string RequirementTypeValue { get; set; }
        public int State { get; set; }

    }
}
