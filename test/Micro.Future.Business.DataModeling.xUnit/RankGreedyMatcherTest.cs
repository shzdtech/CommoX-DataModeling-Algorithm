using Micro.Future.Business.MatchMaker.Commo.Models;
using Micro.Future.Business.MongoDB.Commo.BizObjects;
using Micro.Future.Business.MongoDB.Commo.Handler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;


namespace Micro.Future.Business.DataModeling.xUnit
{
    public class RankGreedyMatcherTest
    {
        [Fact]
        public void TestRunChainMaker()
        {
            var matcherHandler = new MatcherHandler();
            var matcherMaker = new RankingMatchMaker(matcherHandler);
            matcherMaker.make();
        }
        [Fact]
        public void TestChainMatcher()
        {

            var req1 = new RequirementObject();
            req1.Deleted = false;
            req1.EnterpriseId = 1;
            req1.UserId = "1101";
            req1.ProductName = "A";
            req1.ProductPrice = 100;
            req1.ProductQuantity = 1000;
            req1.TradeAmount = 110000;
            req1.RequirementStateId = RequirementStatus.OPEN;
            req1.RequirementTypeId = RequirementType.BUYER;
            req1.CreateTime = DateTime.Now;
            req1.ModifyTime = DateTime.Now;
            req1.ProductType = "T1";
            req1.EnterpriseType = "AAA";

            var filter = new RequirementFilter();
            filter.FilterValueTypeId = FilterValueType.STRING;
            filter.FilterKey = "EnterpriseType";
            filter.FilterValue = "NNN";
            filter.OperationTypeId = FilterOperationType.EQ;

            var req2 = new RequirementObject();
            req2.Deleted = false;
            req2.EnterpriseId = 2;
            req2.UserId = "1102";
            req2.ProductName = "A";
            req2.ProductPrice = 100;
            req2.ProductQuantity = 900;
            req2.TradeAmount = 90000;
            req2.RequirementStateId = RequirementStatus.OPEN;
            req2.RequirementTypeId = RequirementType.SELLER;
            req2.CreateTime = DateTime.Now;
            req2.ModifyTime = DateTime.Now;
            req2.ProductType = "T1";
            req2.EnterpriseType = "NNN";
            req2.Filters = new List<RequirementFilter>();
            req2.Filters.Add(filter);

            var req3 = new RequirementObject();
            req3.Deleted = false;
            req3.EnterpriseId = 3;
            req3.UserId = "1103";
            req3.TradeAmount = 210000;
            req3.RequirementStateId = RequirementStatus.OPEN;
            req3.RequirementTypeId = RequirementType.MID;
            req3.CreateTime = DateTime.Now;
            req3.ModifyTime = DateTime.Now;
            req3.ProductType = "T1";
            req3.EnterpriseType = "NNN";

            var req4 = new RequirementObject();
            req4.Deleted = false;
            req4.EnterpriseId = 4;
            req4.UserId = "1104";
            req4.TradeAmount = 220000;
            req4.RequirementStateId = RequirementStatus.OPEN;
            req4.RequirementTypeId = RequirementType.MID;
            req4.CreateTime = DateTime.Now;
            req4.ModifyTime = DateTime.Now;
            req4.ProductType = "T1";
            req4.EnterpriseType = "NNN";
            req4.Filters = new List<RequirementFilter>();
            req4.Filters.Add(filter);

            var matcherHandler = new MatcherHandler();
            var chainDal = new ChainDAL();
            var req1Id = matcherHandler.AddRequirement(req1);
            matcherHandler.AddRequirement(req2);
            matcherHandler.AddRequirement(req3);
            var req4Id = matcherHandler.AddRequirement(req4);
            var matcherMaker = new RankingMatchMaker(matcherHandler);
            matcherMaker.make();
            
            // Get the open chains with the latest match version of the given userid 
            var chains = matcherHandler.GetMatcherChainsByUserId("1101", ChainStatus.OPEN);

            // Get the open chains with the latest match version of the given requirementid 
            var chains2 = matcherHandler.GetMatcherChainsByRequirementId(req1Id, ChainStatus.OPEN);

            // LOCK the chain
            var b = matcherHandler.LockMatcherChain(chains[0].ChainId, "aaa");

            // Set isLatestVersion to false to get all locked chains, because some locked chains are with old versions
            var lockedChains = matcherHandler.GetMatcherChainsByUserId("1101", ChainStatus.LOCKED, false);

            // Get the confirmed chains of the given requirementid with the latest match version
            // NOTE: some confirmed  chains are with old versions, Set isLatestVersion to false to get them
            //var c = matcherHandler.ConfirmMatcherChain(chains[0].ChainId, "aaa");
            //var confirmedChains = matcherHandler.GetMatcherChainsByRequirementId(chains[0].RequirementIdChain[0], ChainStatus.CONFIRMED);

            // Get ALl confirmed Chains
            // Set isLatestVersion to false to get all confirmed chains, because some confirmed chains may be with the old version
            //var allConfirmedChains = matcherHandler.GetMatcherChains(ChainStatus.CONFIRMED, false);

            //var enterpriseReqs = matcherHandler.QueryRequirementsByEnterpriseId(1, RequirementStatus.CONFIRMED);
            //var enterpriseChains = chainDal.QueryChainsByEnterpriseId(2);
            /*
            var replacedReqs = matcherMaker.FindReplacedRequirementsForChain(chains[0].ChainId, 1, 5);
            var replacedArr = new List<int>();
            replacedArr.Add(1);
            var replacingIdArr = new List<int>();
            replacingIdArr.Add(req4Id);
            matcherHandler.ReplaceRequirementsForChain(chains[0].ChainId, replacedArr, replacingIdArr);
            */
            return;
        }

        private void construct()
        {
            var req1 = new RequirementObject();
            req1.ProductName = "A";
            req1.TradeAmount = 1000;
            req1.RequirementStateId = RequirementStatus.OPEN;

            var req2 = new RequirementObject();
            req1.ProductName = "A";
            req1.TradeAmount = 1000;
            req1.RequirementStateId = (int)RequirementStatus.OPEN;

        }
    }
}
