using Micro.Future.Business.DataAccess.Commo.CommonInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Micro.Future.Business.DataAccess.Commo.CommoObject;

namespace Micro.Future.Business.DataAccess.Commo.CommoHandler
{
    public class AcceptanceHandler : IAcceptance
    {
        private CommoXContext db = null;
        public AcceptanceHandler(CommoXContext dbContext)
        {
            db = dbContext;
        }
        public Acceptance CreateAcceptance(Acceptance acceptance)
        {
            db.Acceptances.Add(acceptance);
            int result = db.SaveChanges();
            if (result > 0)
                return acceptance;
            else
                return null;
        }

        public bool DeleteAcceptance(int acceptanceId)
        {
            var acceptance = QueryAcceptance(acceptanceId);
            db.Acceptances.Remove(acceptance);
            int result = db.SaveChanges();
            if (result > 0)
                return true;
            else
                return false;
        }

        public Acceptance QueryAcceptance(int acceptanceId)
        {
            return db.Acceptances.FirstOrDefault(p => p.AcceptanceId == acceptanceId);
        }

        public IList<Acceptance> QueryAllAcceptances()
        {
            var acceptance = db.Acceptances.ToList();
            return acceptance;
        }

        public bool UpdateAcceptance(Acceptance acceptance)
        {
            var findAcceptance = QueryAcceptance(acceptance.AcceptanceId);
            if (findAcceptance != null)
            {
                findAcceptance.Amount = acceptance.Amount;
                findAcceptance.DueDate = acceptance.DueDate;
                findAcceptance.BankName = acceptance.BankName;
                findAcceptance.AcceptanceType = acceptance.AcceptanceType;
                findAcceptance.DrawTime = acceptance.DrawTime;
                findAcceptance.CreateTime = acceptance.CreateTime;
                findAcceptance.UpdateTime = DateTime.Now;
                

            }
            int result = db.SaveChanges();
            return result > 0;
        }
    }
}
