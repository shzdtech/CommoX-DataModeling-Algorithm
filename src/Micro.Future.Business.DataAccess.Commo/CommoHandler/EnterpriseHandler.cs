using Micro.Future.Business.DataAccess.Commo.CommonInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Micro.Future.Business.DataAccess.Commo.CommoObject;

namespace Micro.Future.Business.DataAccess.Commo.CommoHandler
{
    public class EnterpriseHandler : IEnterprise
    {
        private CommoXContext db = null;
        public EnterpriseHandler(CommoXContext dbContext)
        {
            db = dbContext;
        }
        public Enterprise AddEnterprise(Enterprise enterprise)
        {
            //using (var db = new CommoXContext())
            {
                db.Enterprises.Add(enterprise);
                int count = db.SaveChanges();
                if (count > 0)
                    return enterprise;
                else
                    return null;
            }
        }

        public Enterprise QueryEnterpriseInfo(int enterpriseId)
        {
            //using (var db = new CommoXContext())
            {
                var enterprise = db.Enterprises.Single(e => e.EnterpriseId.Equals(enterpriseId));
                return enterprise;
            }
        }

        public Enterprise QueryEnterpriseInfo(String name)
        {
            //using (var db = new CommoXContext())
            {
                var enterprise = db.Enterprises.Where(e => e.Name.Contains(name))
                                                .First();
                return enterprise;
            }
        }


        public IList<Enterprise> QueryEnterpriseList(String name)
        {
            //using (var db = new CommoXContext())
            {
                var enterprise = db.Enterprises.Where(e => e.Name.Contains(name))
                                                .ToList();
                return enterprise;
            }
        }

        public bool UpdateEnterprise(Enterprise enterprise)
        {
            //var findEnterprise = QueryEnterpriseInfo(enterprise.EnterpriseId);
            //if (findEnterprise != null)
            //{
            //    findEnterprise.Address = enterprise.Address;
            //    findEnterprise.AnnualInspection = enterprise.AnnualInspection;
            //    findEnterprise.BusinessRange = enterprise.BusinessRange;
            //    findEnterprise.BusinessTypeId = enterprise.BusinessTypeId;
            //    findEnterprise.Contacts = enterprise.Contacts;
            //    findEnterprise.InvoicedQuantity = enterprise.InvoicedQuantity;
            //    findEnterprise.LegalRepresentative = enterprise.LegalRepresentative;
            //    findEnterprise.Name = enterprise.Name;
            //    findEnterprise.PaymentMethodId = enterprise.PaymentMethodId;
            //    findEnterprise.PreviousProfit = enterprise.PreviousProfit;
            //    findEnterprise.PreviousSales = enterprise.PreviousSales;
            //    findEnterprise.RegisterAccount = enterprise.RegisterAccount;
            //    findEnterprise.RegisterAddress = enterprise.RegisterAddress;
            //    findEnterprise.RegisterBankId = enterprise.RegisterBankId;
                 
                
            //}
            db.Entry(enterprise).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            int result = db.SaveChanges();
            return result > 0;
        }

        public bool UpdateEnterpriseState(int enterpriseId, int stateId)
        {
            var enterprise = QueryEnterpriseInfo(enterpriseId);
            if(enterprise != null)
            {
                enterprise.StateId = stateId;
            }

            db.Entry(enterprise).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            int result = db.SaveChanges();
            return result > 0;
        }
    }
}
