using Micro.Future.Business.MongoDB.Commo.BizObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Micro.Future.Business.MongoDB.Commo.MongoInterface
{
    public interface IChainDAL
    {
        ChainObject GetChain(int chainId);

        IList<ChainObject> QueryChains(int userId);

        IList<RequirementObject> GetChainRequirements(int chainId);

        /// <summary>
        /// 确认撮合链中的一个需求，当所有需求都已确认时，isAllConfirmed=true
        /// </summary>
        /// <param name="chainId"></param>
        /// <param name="requirementId"></param>
        /// <param name="isAllConfirmed"></param>
        /// <returns></returns>
        bool ConfirmChainRequirement(int chainId, int requirementId, out bool isAllConfirmed);
    }
}
