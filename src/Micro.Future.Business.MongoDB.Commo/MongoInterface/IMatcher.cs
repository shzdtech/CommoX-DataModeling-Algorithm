using Micro.Future.Business.MongoDB.Commo.BizObjects;
using Micro.Future.Business.MongoDB.Commo.QueryObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Micro.Future.Business.MongoDB.Commo.MongoInterface
{
    public interface IMatcher
    {
        int AddRequirement(RequirementObject requirement);

        bool CancelRequirement(int requirementId);

        IEnumerable<RequirementObject> QueryRequirements(string userId);

        IEnumerable<RequirementObject> QueryAllRequirements();

        RequirementObject QueryRequirementInfo(int requirementId);

        void AddMatcherChains(IList<ChainObject> chains);

        IList<ChainObject> GetMatcherChains(ChainStatus stauts, bool isLatestVersion);

        IList<ChainObject> GetMatcherChainsByRequirementId(int requirementId, ChainStatus stauts, bool isLatestVersion);

        IList<ChainObject> GetMatcherChainsByUserId(String userId, ChainStatus status, bool isLatestVersion);

        bool LockMatcherChain(int chainId);

        bool UnLockMatcherChain(int chainId);

        bool ConfirmMatcherChain(int chainId);

        IList<RequirementObject> QueryRequirementsByEnterpriseId(int enterpriseId, RequirementStatus requirementState);

        IList<RequirementObject> QueryRequirementsByRequirementFilter(RequirementQuery reqQuery, string orderBy, out int pageNo, out int pageSize, out int totalSize);





    }



}
