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

        /// <summary>
        /// 指定几个需求，撮合一条需求列表
        /// </summary>
        /// <param name="requirementIds">
        /// 指定的一些需求id列表，不能为空，或者全是0。
        /// 固定位置时，留空的需求Id填0.
        /// </param>
        /// <param name="fixedLength">传值表示固定长度</param>
        /// <param name="isPositionFixed">已有的需求是否固定位置。 true表示固定位置；false表示不固定 </param>
        /// <returns></returns>
        ChainObject AutoMatchRequirements(string opUserId, IList<int> requirementIds, int fixedLength, bool isPositionFixed = false);

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
