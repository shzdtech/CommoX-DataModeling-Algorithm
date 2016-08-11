using Micro.Future.Business.MongoDB.Commo.MongoInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Micro.Future.Business.MongoDB.Commo.BizObjects;

namespace Micro.Future.Business.MongoDB.Commo.Handler
{
    public class ChainDAL : IChainDAL
    {
        public bool ConfirmChainRequirement(int chainId, int requirementId, out bool isAllConfirmed)
        {
            throw new NotImplementedException();
        }

        public ChainObject GetChain(int chainId)
        {
            throw new NotImplementedException();
        }

        public IList<RequirementObject> GetChainRequirements(int chainId)
        {
            throw new NotImplementedException();
        }

        public IList<ChainObject> QueryChains(int userId)
        {
            throw new NotImplementedException();
        }
    }
}
