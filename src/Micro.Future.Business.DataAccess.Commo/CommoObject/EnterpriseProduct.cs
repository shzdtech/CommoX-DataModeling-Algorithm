using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Micro.Future.Business.DataAccess.Commo.CommoObject
{
    /// <summary>
    /// 企业产品表
    /// </summary>
    public class EnterpriseProduct
    {
        /// <summary>
        /// 企业Id
        /// </summary>
        public int EnterpriseId { get; set; }
        /// <summary>
        /// 产品Id
        /// </summary>
        public int ProductId { get; set; }
        /// <summary>
        /// 仓库地址
        /// </summary>
        public string StorageAddress { get; set; }
        /// <summary>
        /// 仓单类型
        /// </summary>
        public string WarehouseReceipt { get; set; }
        /// <summary>
        /// 库存量
        /// </summary>
        public double Quota { get; set; }
    

        public string CreateTime { get; set; }
    }
}
