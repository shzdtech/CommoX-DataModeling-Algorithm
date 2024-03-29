﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Micro.Future.Business.DataAccess.Commo.CommoObject
{
    /// <summary>
    /// 需求的具体条件内容
    /// </summary>
    public class RequirementRule
    {
        [Key]
        public int RuleId { get; set; }


        /// <summary>
        /// 规则类型：1=企业规则、2=货物规则、3=资金规则、4=支付规则
        /// </summary>
        public int RuleType { get; set; }

        /// <summary>
        /// 规则名称，比如：企业类型，注册资本
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// 规则对应的值，比如：企业类型要求是国企，则value=“国企”
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// 规则名称与值之间的关系，是相等，还是包含
        /// </summary>
        public int OperationId { get; set; }

        /// <summary>
        /// 撮合规则状态
        /// </summary>
        public int State { get; set; }
    }
}
