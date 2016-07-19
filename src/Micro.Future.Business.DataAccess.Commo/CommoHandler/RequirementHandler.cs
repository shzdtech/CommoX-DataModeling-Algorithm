using Micro.Future.Business.DataAccess.Commo.CommonInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Micro.Future.Business.DataAccess.Commo.CommoHandler
{
    public class RequirementHandler : IRequirement
    {
        public Requirement queryRequirement(int userId)
        {
            using (var db = new CommoXContext())
            {
                return db.Requirements.FirstOrDefault(f => f.UserId == userId);
            }
        }

        public Boolean submitRequirement(Requirement require)
        {
            using(var db = new CommoXContext())
            {
                db.Add(require);
                int result = db.SaveChanges();
                return result > 0;
            }
        }
    }
}
