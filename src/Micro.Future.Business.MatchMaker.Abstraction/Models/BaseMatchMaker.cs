using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Micro.Future.Business.MongoDB.Commo.BizObjects;

namespace Micro.Future.Business.MatchMaker.Abstraction.Models
{
    public interface BaseMatchMaker
    {
        void make();
        IList<RequirementObject> FindReplacedRequirementsForChain(int chainId, int replacedNodeIndex, int topN);
    }
}
