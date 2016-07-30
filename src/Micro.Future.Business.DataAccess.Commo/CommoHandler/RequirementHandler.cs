﻿using Micro.Future.Business.DataAccess.Commo.CommonInterface;
using Micro.Future.Business.DataAccess.Commo.CommoObject;
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
                db.Requirements.Add(require);
                int result = db.SaveChanges();
                return result > 0;
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

        public IEnumerable<Requirement> queryRequirements(int userId)
        {
            using (var db = new CommoXContext())
            {
                return db.Requirements.Where(f => f.UserId == userId);
            }
        }

        public IEnumerable<RequirementFilter> queryRequirementFilters(int requirementId)
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

    }
}
