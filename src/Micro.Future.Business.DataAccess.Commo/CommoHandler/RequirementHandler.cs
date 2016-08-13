using Micro.Future.Business.DataAccess.Commo.CommonInterface;
using Micro.Future.Business.DataAccess.Commo.CommoObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Micro.Future.Business.DataAccess.Commo.CommoHandler
{
    public class RequirementHandler : IRequirement
    {
        public Requirement saveRequirement(Requirement require)
        {
            using (var db = new CommoXContext())
            {
                db.Requirements.Add(require);
                int result = db.SaveChanges();
                if (result > 0)
                    return require;
                else
                    return null;
            }
        }


        public bool saveRequirementFilters(IEnumerable<RequirementFilter> filters)
        {
            using (var db = new CommoXContext())
            {
                foreach (RequirementFilter filter in filters)
                {
                    db.RequirementFilters.Add(filter);
                }
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

        public IList<Requirement> queryRequirements(int userId)
        {
            using (var db = new CommoXContext())
            {
                return db.Requirements.Where(f => f.UserId == userId).ToList();
            }
        }

        public IList<RequirementFilter> queryRequirementFilters(int requirementId)
        {
            using (var db = new CommoXContext())
            {
                //var query = from r in db.Requirements
                //            join f in db.RequirementFilters on r.RequirementId equals f.RequirementId
                //            where r.RequirementId == requirementId && f.StateId == 1
                //            select f;
                var result = db.RequirementFilters.Where(f => f.RequirementId == requirementId);
                return result.ToList();
            }
        }

        public RequirementType submitRequirementTypeId(RequirementType requirementType)
        {
            throw new NotImplementedException();
        }

        public RequirementType queryRequirementType(int RequirementTypeId)
        {
            throw new NotImplementedException();
        }

        public IList<RequirementType> queryAllRequirementType()
        {
            throw new NotImplementedException();
        }
    }
}
