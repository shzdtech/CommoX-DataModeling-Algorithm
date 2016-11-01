using Micro.Future.Business.DataAccess.Commo.CommonInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Micro.Future.Business.DataAccess.Commo.CommoObject;

namespace Micro.Future.Business.DataAccess.Commo.CommoHandler
{
    public class AcceptanceBillHandler : IAcceptanceBill
    {
        private CommoXContext db = null;
        public AcceptanceBillHandler(CommoXContext dbContext)
        {
            db = dbContext;
        }
        public AcceptanceBill CreateAcceptance(AcceptanceBill acceptance)
        {
            db.AcceptanceBills.Add(acceptance);
            int result = db.SaveChanges();
            if (result > 0)
                return acceptance;
            else
                return null;
        }

        public bool DeleteAcceptance(int acceptanceId)
        {
            var acceptance = QueryAcceptance(acceptanceId);
            db.AcceptanceBills.Remove(acceptance);
            int result = db.SaveChanges();
            if (result > 0)
                return true;
            else
                return false;
        }

        public AcceptanceBill QueryAcceptance(int acceptanceId)
        {
            return db.AcceptanceBills.FirstOrDefault(p => p.AcceptanceBillId == acceptanceId);
        }

        public IList<AcceptanceBill> QueryAllAcceptances(int enterpriseId)
        {
            var acceptance = db.AcceptanceBills.Where(f=>f.EnterpriseId == enterpriseId).ToList();
            return acceptance;
        }

        public bool UpdateAcceptance(AcceptanceBill acceptance)
        {
            var findAcceptance = QueryAcceptance(acceptance.AcceptanceBillId);
            if (findAcceptance != null)
            {
                findAcceptance.Amount = acceptance.Amount;
                findAcceptance.DueDate = acceptance.DueDate;
                findAcceptance.CreateTime = acceptance.CreateTime;

                findAcceptance.DrawerName = acceptance.DrawerName;
                findAcceptance.DrawerAccount = acceptance.DrawerAccount;
                findAcceptance.DrawerBankId = acceptance.DrawerBankId;
                findAcceptance.PayeeName = acceptance.PayeeName;
                findAcceptance.PayeeAccount = acceptance.PayeeAccount;
                findAcceptance.PayeeBankId = acceptance.PayeeBankId;
                findAcceptance.Amount = acceptance.Amount;
                findAcceptance.DueDate = acceptance.DueDate;
                findAcceptance.AgreementNumber = acceptance.AgreementNumber;
            }
            int result = db.SaveChanges();
            return result > 0;
        }
    }
}
