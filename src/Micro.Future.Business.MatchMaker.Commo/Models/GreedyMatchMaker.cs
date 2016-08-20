using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Micro.Future.Business.MongoDB.Commo;
using Micro.Future.Business.MongoDB.Commo.BizObjects;
using Micro.Future.Business.MongoDB.Commo.Handler;
using Micro.Future.Business.MatchMaker.Abstraction.Models;

namespace Micro.Future.Business.MatchMaker.Commo.Models
{
    public class GreedyMatchMaker: BaseMatchMaker
    {
        private MatcherHandler matcherHandler;
        public GreedyMatchMaker(MatcherHandler mHandler)
        {
            matcherHandler = mHandler;
        }

        public void make()
        {
            var newRequirements = matcherHandler.GetUnprocessedRequirements();
            var processedRequirements = matcherHandler.GetProcessedRequirements();
            var union = newRequirements.Union(processedRequirements);

            var res = new List<ChainObject>();

            // 1. new buyer -> all mid -> all seller
            foreach (RequirementObject req1 in newRequirements)
            {
                if (req1.RequirementTypeId != 1) continue;
                foreach (RequirementObject req2 in union)
                {
                    //RequirementTypeId: buyer = 1, seller = 2, others = 3
                    if (req2.RequirementTypeId == 3 && req1.ProductId == req2.ProductId)
                    {
                        foreach (RequirementObject req3 in union)
                        {
                            if (req3.RequirementTypeId == 2 && req3.ProductId == req1.ProductId)
                            {
                                var newChain = new ChainObject(
                                    new List<RequirementObject> { req1, req2, req3 });
                                res.Add(newChain);
                            }
                        }

                    }
                }
            }

            // 2. old buyer -> all mid -> new seller
            foreach (RequirementObject req1 in processedRequirements)
            {
                if (req1.RequirementTypeId != 1) continue;
                foreach (RequirementObject req2 in union)
                {
                    //RequirementTypeId: buyer = 1, seller = 2, others = 3
                    if (req2.RequirementTypeId == 3 && req1.ProductId == req2.ProductId)
                    {
                        foreach (RequirementObject req3 in newRequirements)
                        {
                            if (req3.RequirementTypeId == 2 && req3.ProductId == req1.ProductId)
                            {
                                var newChain = new ChainObject(
                                    new List<RequirementObject> { req1, req2, req3 });
                                res.Add(newChain);
                            }
                        }

                    }
                }
            }

            // 3. old buyer -> new mid -> old seller
            foreach (RequirementObject req1 in processedRequirements)
            {
                if (req1.RequirementTypeId != 1) continue;
                foreach (RequirementObject req2 in newRequirements)
                {
                    //RequirementTypeId: buyer = 1, seller = 2, others = 3
                    if (req2.RequirementTypeId == 3 && req1.ProductId == req2.ProductId)
                    {
                        foreach (RequirementObject req3 in processedRequirements)
                        {
                            if (req3.RequirementTypeId == 2 && req3.ProductId == req1.ProductId)
                            {
                                var newChain = new ChainObject(
                                    new List<RequirementObject> { req1, req2, req3 });
                                res.Add(newChain);
                            }
                        }

                    }
                }
            }

            // mark the new added requirements as processed
            foreach (RequirementObject req1 in newRequirements)
            {
                req1.RequirementStateId = 1;
                matcherHandler.UpdateRequirement(req1);
            }

            // add new chain in the chain collection
            foreach (ChainObject chain in res)
            {
                matcherHandler.AddRequirementChain(chain);
            }

            // call EventHandler
            matcherHandler.CallOnChainAdded(res);
        }   
    }
}
