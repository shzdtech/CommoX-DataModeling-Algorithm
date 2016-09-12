using Micro.Future.Business.DataAccess.Commo.CommonInterface;
using Micro.Future.Business.DataAccess.Commo.CommoObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Micro.Future.Business.DataAccess.Commo.CommoHandler
{
    class EmailVerifyCodeHandler:IEmailVerifyCode
    {
        private CommoXContext db = null;
        public EmailVerifyCodeHandler(CommoXContext dbContext)
        {
            db = dbContext;
        }

        public bool CheckEmailVerifyCode(string email, string code)
        {
            var r = (from u in db.EmailVerifyCodes
                     where u.Email == email && u.SendTime >= DateTime.UtcNow.AddMinutes(-10)
                     orderby u.SendTime descending
                     select u).FirstOrDefault();
            if (r != null && r.VerifyCode == code)
            {
                r.HasConfirmed = true;
                db.SaveChanges();
                return true;
            }
            return false;
        }

        public bool HasExceedLimitationPerDay(string email)
        {
            return db.EmailVerifyCodes.Count(c => c.Email == email && DateTime.UtcNow.Subtract(c.SendTime).Days == 0) >= 10;
        }

        public bool CanResend(string email)
        {
            return !db.EmailVerifyCodes.Any(c => c.Email == email && c.SendTime.AddSeconds(60 * 2) > DateTime.UtcNow);
        }

        public void SaveEmailVerifyCode(string requestId, string email, string code, DateTime sendTime)
        {
            db.EmailVerifyCodes.Add(new EmailVerifyCode
            {
                RequestId = requestId,
                Email = email,
                VerifyCode = code,
                SendTime = sendTime,
                HasConfirmed = false
            });
            db.SaveChanges();
        }
    }
}
