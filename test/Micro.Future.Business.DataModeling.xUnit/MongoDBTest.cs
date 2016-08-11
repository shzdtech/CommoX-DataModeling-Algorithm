using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Micro.Future.Business.MongoDB.Commo.Handler;
using Micro.Future.Business.MongoDB.Commo.BizObjects;

namespace Micro.Future.Business.DataModeling.xUnit
{
    public class MongoDBTest
    {
        public MongoDBTest()
        {
        }

        [Fact]
        public void TestConnection()
        {
            var handler = new RequirementHandler();
            var req = new RequirementObject();
            req.Deleted = false;
            req.EnterpriseId = 1;
            req.ProductId = 10;
            req.ProductPrice = 100;
            req.RequirementStateId = 0;
            req.RequirementTypeId = 1;
            var id = handler.AddRequirement(req);
            var queryRes = handler.QueryRequirementInfo(req.RequirementId);

            var chain = new ChainObject();
            chain.Deleted = false;
            var list = new List<int>();
            list.Add(id);
            list.Add(10);
            list.Add(11);
            chain.RequirementIdChain = list;
            handler.AddRequirementChain(chain);

            var chains = handler.QueryRequirementChains(180);
            var size = chains.Count();

            var res = handler.CancelRequirement(req.RequirementId);

        }
    }
}
