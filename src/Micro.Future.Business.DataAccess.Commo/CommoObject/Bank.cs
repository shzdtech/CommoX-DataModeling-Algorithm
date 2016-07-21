using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Micro.Future.Business.DataAccess.Commo.CommoObject
{
    /// <summary>
    /// 银行信息表
    /// </summary>
    public class Bank
    {
        public int BankId { get; set; }
        /// <summary>
        /// 银行名称
        /// </summary>
        public string BankName { get; set; }
        /// <summary>
        /// 银行地址
        /// </summary>
        public string BankAddress { get; set; }
    }

}
