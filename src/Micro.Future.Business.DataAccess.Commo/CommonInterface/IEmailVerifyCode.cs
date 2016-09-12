using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Micro.Future.Business.DataAccess.Commo.CommonInterface
{
    public interface IEmailVerifyCode
    {
        bool CheckEmailVerifyCode(string email, string code);

        bool HasExceedLimitationPerDay(string email);
        
        bool CanResend(string email);

        void SaveEmailVerifyCode(string requestId, string email, string code, DateTime sendTime);
    }
 
}
