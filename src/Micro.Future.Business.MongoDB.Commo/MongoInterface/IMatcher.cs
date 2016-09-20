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

        ChainObject GetChain(int chainId);

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

        void CallOnChainAdded(List<ChainObject> chains);

        IList<RequirementObject> getReqSortedByAmountDesc(RequirementType requirementType);

        /// <summary>
        /// Replace Requirements for chain
        /// </summary>
        /// 给定的Chain必须为锁定状态
        /// <param name="chainId"></param>
        /// <param name="replacedNodeIndexArr"></param>
        /// <param name="replacingRequirementIds"></param>
        /// <returns></returns>
        bool ReplaceRequirementsForChain(int chainId, IList<int> replacedNodeIndexArr, IList<int> replacingRequirementIds);

    }



}
