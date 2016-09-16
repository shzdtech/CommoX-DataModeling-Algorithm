using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Micro.Future.Business.DataAccess.Commo.CommoObject
{
    public class OperationRecord
    {
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; }
        /// <summary>
        /// 操作对象的类型：如Chain；
        /// </summary>
        public string ObjectType { get; set; }
        /// <summary>
        /// 操作对象的值：如 100；  即操作100的chain
        /// </summary>
        public string ObjectValue { get; set; }
        /// <summary>
        /// 操作类型如为chain：1：锁定；2：解除锁定；3：确认
        /// </summary>
        public string Operation { get; set; }
        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime CreateTime { get; set; }



    }
}
