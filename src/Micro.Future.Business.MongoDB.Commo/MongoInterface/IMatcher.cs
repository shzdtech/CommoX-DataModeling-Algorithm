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

        int AddChain(ChainObject chain);
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

        IList<RequirementObject> getBuyerSellerReqSortedByAmountAsc(RequirementType mID, string productName, decimal minAmount);

        IList<RequirementObject> getMidReqSortedByAmountAsc(RequirementType mID, decimal minAmount);

        /// <summary>
        /// Replace Requirements for chain
        /// </summary>
        /// 给定的Chain必须为锁定状态
        /// <param name="chainId"></param>
        /// <param name="replacedNodeIndexArr"></param>
        /// <param name="replacingRequirementIds"></param>
        /// <returns></returns>
        bool ReplaceRequirementsForChain(int chainId, IList<int> replacedNodeIndexArr, IList<int> replacingRequirementIds);


        /// <summary>
        /// 指定需求列表，直接生成一条链
        /// </summary>
        /// <param name="requirementids">需求列表，长度必须大于等于3，所有Id不能为0</param>
        /// <param name="opUserId">操作员</param>
        /// <returns>
        /// ChainObj or NULL if the chain is not valid or it cannot be confirmed
        /// </returns>
        ChainObject CreateChain(IList<int> requirementids, string opUserId);
    }



}
