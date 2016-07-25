using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Micro.Future.Business.DataAccess.Commo.CommoObject
{
    /// <summary>
    /// 需求的具体条件内容
    /// </summary>
    public class RequirementFilter
    {
        public int FilterId { get; set; }

        public int RequirementId { get; set; }
        /// <summary>
        /// 需求 条件  “主键”
        /// </summary>
        public string FilterKey { get; set; }
        /// <summary>
        /// 需求条件 “操作”类型：如大于；小于等
        /// </summary>
        public String Operation { get; set; }
        /// <summary>
        /// 需求条件的 操作的 “值”
        /// </summary>
        public string FilterValue { get; set; }

        public int StateId { get; set; }
    }
}
