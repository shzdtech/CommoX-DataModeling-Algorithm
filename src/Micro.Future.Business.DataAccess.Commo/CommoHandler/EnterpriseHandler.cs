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
        public Enterprise AddEnterprise(Enterprise enterprise)
        {
            using (var db = new CommoXContext())
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
            using (var db = new CommoXContext())
            {
                var enterprise = db.Enterprises.Single(e => e.EnterpriseId.Equals(enterpriseId));
                return enterprise;
            }
        }

        public Enterprise QueryEnterpriseInfo(String name)
        {
            using (var db = new CommoXContext())
            {
                var enterprise = db.Enterprises.Where(e => e.Name.Contains(name))
                                                .First();
                return enterprise;
            }
        }


        public IEnumerable<Enterprise> QueryEnterpriseList(String name)
        {
            using (var db = new CommoXContext())
            {
                var enterprise = db.Enterprises.Where(e => e.Name.Contains(name))
                                                .ToList();
                return enterprise;
            }
        }

        public Enterprise UpdateEnterprise(Enterprise enterprise)
        {
            throw new NotImplementedException();
        }
    }
}
