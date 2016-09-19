using Micro.Future.Business.MongoDB.Commo.BizObjects;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;

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

        bool LockMatcherChain(int chainId, string operatorId);

        bool UnLockMatcherChain(int chainId, string operatorId);

        bool ConfirmMatcherChain(int chainId, string operatorId);

        IList<RequirementObject> QueryRequirementsByEnterpriseId(int enterpriseId, RequirementStatus? requirementState);

        IEnumerable<RequirementObject> QueryRequirementsByLinq(Func<RequirementObject, bool> selector);

        IList<IList<RequirementObject>> FindReplacedRequirementsForChain(int chainId, IList<int> replacedNodeIndexArr, int topN);

        bool ReplaceRequirementsForChain(int chainId, IList<int> replacedNodeIndexArr, IList<RequirementObject> replacedRequirements);

    }



}
