using Micro.Future.Business.MongoDB.Commo.BizObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Micro.Future.Business.MongoDB.Commo.MongoInterface
{
    public interface IRequirement
    {
        int AddRequirement(RequirementObject requirement);

        bool CancelRequirement(int requirementId);

        IEnumerable<RequirementObject> QueryRequirements(string userId);

        RequirementObject QueryRequirementInfo(int requirementId);

        IEnumerable<ChainObject> QueryRequirementChains(int requirementId);
    }



}
