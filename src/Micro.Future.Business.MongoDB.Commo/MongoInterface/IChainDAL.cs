using Micro.Future.Business.MongoDB.Commo.BizObjects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Micro.Future.Business.MongoDB.Commo.MongoInterface
{
    public interface IChainDAL
    {
        ChainObject GetChain(int chainId);

        IList<ChainObject> QueryChains(string userId);

        IList<ChainObject> QueryChainsByEnterpriseId(int enterpriseId, ChainStatus? state);

        IEnumerable<ChainObject> QueryChainsByLinq(Func<ChainObject, bool> selector);


        //IList<RequirementObject> GetChainRequirements(int chainId);

        /// <summary>
        /// 无用了
        /// 确认撮合链中的一个需求，当所有需求都已确认时，isAllConfirmed=true
        /// </summary>
        /// <param name="chainId"></param>
        /// <param name="requirementId"></param>
        /// <param name="isAllConfirmed"></param>
        /// <returns></returns>
        //bool ConfirmChainRequirement(int chainId, int requirementId, out bool isAllConfirmed);
    }
}
