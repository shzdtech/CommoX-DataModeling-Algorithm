using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Micro.Future.Business.MongoDB.Commo;
using Micro.Future.Business.MongoDB.Commo.BizObjects;
using Micro.Future.Business.MongoDB.Commo.Handler;

namespace Micro.Future.Business.MatchMaker.Commo.Models
{
    public static class GreedyMatchMaker
    {
        static void makeChainIncreament()
        {
            var reqHandler = new RequirementHandler();
            var newRequirements = reqHandler.GetNewAddedRequirements();
            var processedRequirements = reqHandler.GetProcessedRequirements();
            var union = newRequirements.Union(processedRequirements);

            var res = new List<ChainObject>();

            // seller and buyer are both in the new added requirements
            foreach (RequirementObject req1 in newRequirements)
            {
                foreach (RequirementObject req2 in newRequirements)
                {
                    //RequirementTypeId: buyer = 1, seller = 2, others = 4
                    if (req1.RequirementTypeId + req2.RequirementTypeId == 3 &&
                        req1.ProductId == req2.ProductId)
                    {
                        foreach (RequirementObject req3 in union)
                        {
                            if (req3.RequirementTypeId == 4 && req3.ProductId == req1.ProductId)
                            {
                                var newChain = new ChainObject();
                                var list = new List<int>();
                                list.Add(req1.RequirementId);
                                list.Add(req2.RequirementId);
                                list.Add(req3.RequirementId);
                                newChain.RequirementIdChain = list;
                                res.Add(newChain);
                            }
                        }

                    }
                }
            }

            // one of seller and buyer is in new added requirements
            foreach (RequirementObject req1 in newRequirements)
            {
                foreach (RequirementObject req2 in processedRequirements)
                {
                    //RequirementTypeId: buyer = 1, seller = 2, others = 4
                    if (req1.RequirementTypeId + req2.RequirementTypeId == 3 &&
                        req1.ProductId == req2.ProductId)
                    {
                        foreach (RequirementObject req3 in union)
                        {
                            if (req3.RequirementTypeId == 4 && req3.ProductId == req1.ProductId)
                            {
                                var newChain = new ChainObject();
                                var list = new List<int>();
                                list.Add(req1.RequirementId);
                                list.Add(req2.RequirementId);
                                list.Add(req3.RequirementId);
                                newChain.RequirementIdChain = list;
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
                reqHandler.UpdateRequirement(req1);
            }

            // add new chain in the chain collection
            foreach (ChainObject chain in res)
            {
                reqHandler.AddRequirementChain(chain);
            }

            // call EventHandler
            reqHandler.CallOnChainChanged(res);
        }   
    }
}
