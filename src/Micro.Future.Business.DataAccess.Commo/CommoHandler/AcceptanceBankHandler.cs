using Micro.Future.Business.DataAccess.Commo.CommonInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Micro.Future.Business.DataAccess.Commo.CommoObject;

namespace Micro.Future.Business.DataAccess.Commo.CommoHandler
{
    public class AcceptanceBankHandler : IAcceptanceBank
    {
        private CommoXContext db = null;
        public AcceptanceBankHandler(CommoXContext dbContext)
        {
            db = dbContext;
        }

        public AcceptanceBank CreateBank(AcceptanceBank newBank)
        {
            db.AcceptanceBanks.Add(newBank);
            int result = db.SaveChanges();
            if (result > 0)
                return newBank;
            else
                return null;
        }

        public bool DeleteBank(int bankId)
        {
            var bank = QueryBankInfo(bankId);
            db.AcceptanceBanks.Remove(bank);
            int result = db.SaveChanges();
            if (result > 0)
                return true;
            else
                return false;
        }

        public IList<AcceptanceBank> QueryAllBanks()
        {
            return db.AcceptanceBanks.OrderByDescending(f => f.CreateTime).ToList();
        }

        public AcceptanceBank QueryBankInfo(int bankId)
        {
            return db.AcceptanceBanks.FirstOrDefault(p => p.BankId == bankId);
        }

        public bool UpdateBank(AcceptanceBank bank)
        {
            var findBank = QueryBankInfo(bank.BankId);
            if (findBank != null)
            {
                findBank.AcceptanceType = bank.AcceptanceType;
                findBank.BankName = bank.BankName;
                findBank.BankPrice = bank.BankPrice;
                findBank.BankType = bank.BankType;
                findBank.UpdateTime = DateTime.Now;
            }
            int result = db.SaveChanges();
            return result > 0;
        }
    }
}
