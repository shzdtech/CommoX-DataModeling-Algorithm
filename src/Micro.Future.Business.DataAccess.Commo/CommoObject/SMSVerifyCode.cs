using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Micro.Future.Business.DataAccess.Commo.CommoObject
{
    public class SMSVerifyCode
    {
        [Key]
        public ulong Id { get; set; }
        public string RequestId { get; set; }
        public string PhoneNumber { get; set; }
        public string VerifyCode { get; set; }
        public bool HasConfirmed { get; set; }
        public DateTime SendTime { get; set; }
    }
}
