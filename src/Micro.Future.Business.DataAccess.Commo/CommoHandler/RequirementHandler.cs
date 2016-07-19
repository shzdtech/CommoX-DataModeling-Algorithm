using Micro.Future.Business.DataAccess.Commo.CommonInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Micro.Future.Business.DataAccess.Commo.CommoHandler
{
    public class RequirementHandler : IRequirement
    {
        public bool saveRequirement(Requirement require)
        {
            using (var db = new CommoXContext())
            {
                db.Add(require);
                int result = db.SaveChanges();
                return result > 0;
            }
        }
       
        public Requirement queryRequirementInfo(int requirementId)
        {
            using (var db = new CommoXContext())
            {
                return db.Requirements.SingleOrDefault(f => f.RequirementId == requirementId);
            }
        }

        public IEnumerable<Requirement> queryRequirements(int userId)
        {
            using (var db = new CommoXContext())
            {
                return db.Requirements.Where(f => f.UserId == userId);
            }
        }

        public IEnumerable<Filter> queryRequirementFilters(int requirementId)
        {
            using (var db = new CommoXContext())
            {
                var query = from r in db.RequirementDetails
                            join f in db.Filters on r.FilterId equals f.FilterId
                            where r.RequirementId == requirementId && f.StateId == 1
                            select f;

                return query.ToList();
            }
        }

        public bool saveRequirementFilters(IEnumerable<Filter> filters)
        {
            throw new NotImplementedException();
        }
    }
}
