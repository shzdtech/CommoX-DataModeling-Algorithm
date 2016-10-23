using Micro.Future.Business.MatchMaker.Commo.Models;
using Micro.Future.Business.MongoDB.Commo.BizObjects;
using Micro.Future.Business.MongoDB.Commo.Config;
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
            MongoDBConfig.load(TestMongoConfig.conf);

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
            req2.TradeAmount = 80000;
            req2.RequirementStateId = RequirementStatus.OPEN;
            req2.RequirementTypeId = RequirementType.SELLER;
            req2.CreateTime = DateTime.Now;
            req2.ModifyTime = DateTime.Now;
            req2.ProductType = "T1";
            req2.EnterpriseType = "NNN";
            req2.Filters = new List<RequirementFilter>();
            //req2.Filters.Add(filter);

            var req3 = new RequirementObject();
            req3.Deleted = false;
            req3.EnterpriseId = 3;
            req3.UserId = "1103";
            req3.TradeAmount = 80000;
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
            req4.TradeAmount = 90000;
            req4.RequirementStateId = RequirementStatus.OPEN;
            req4.RequirementTypeId = RequirementType.MID;
            req4.CreateTime = DateTime.Now;
            req4.ModifyTime = DateTime.Now;
            req4.ProductType = "T1";
            req4.EnterpriseType = "NNN";
            req4.Filters = new List<RequirementFilter>();
            //req4.Filters.Add(filter);

            var matcherHandler = new MatcherHandler();
            var chainDal = new ChainDAL();
            var req1Id = matcherHandler.AddRequirement(req1);
            var req2Id = matcherHandler.AddRequirement(req2);
            var req3Id = matcherHandler.AddRequirement(req3);
            var req4Id = matcherHandler.AddRequirement(req4);
            var matcherMaker = new RankingMatchMaker(matcherHandler);
            matcherMaker.make();
            
            // Get the open chains with the latest match version of the given userid 
            var chains = matcherHandler.GetMatcherChainsByUserId("1101", ChainStatus.OPEN);

            // Test AutoMatchRequirements
            
            var autoReqsNoFix = new List<int>();
            var autoReqsFix = new List<int>();
            autoReqsFix.Add(-1);
            autoReqsFix.Add(0);
            autoReqsFix.Add(req4Id);
            autoReqsFix.Add(req2Id);
            var opUserId = "abc";
            /*
            var c1 = matcherMaker.AutoMatchRequirements(opUserId, autoReqsFix, 4, true);
            if(c1 != null) matcherHandler.UnLockMatcherChain(c1.ChainId, opUserId);
            var c2 = matcherMaker.AutoMatchRequirements(opUserId, autoReqsFix, 0, true, 3);
            if(c2 != null) matcherHandler.UnLockMatcherChain(c2.ChainId, opUserId);
            */
            autoReqsNoFix.Add(-1);
            autoReqsNoFix.Add(req3Id);
            autoReqsNoFix.Add(req2Id);
            var c13 = matcherMaker.AutoMatchRequirements(opUserId, autoReqsNoFix, 4, false);
            if (c13 != null) matcherHandler.UnLockMatcherChain(c13.ChainId, opUserId);
            var c14 = matcherMaker.AutoMatchRequirements(opUserId, autoReqsNoFix, 0, false, 3);
            if (c14 != null) matcherHandler.UnLockMatcherChain(c14.ChainId, opUserId);

            var autoReqsNoFix2 = new List<int>();
            var autoReqsFix2 = new List<int>();
            autoReqsFix2.Add(req1Id);
            autoReqsFix2.Add(req3Id);
            autoReqsFix2.Add(0);
            autoReqsFix2.Add(-1);
            var opUserId2 = "abc2";
            /*
            var c21 = matcherMaker.AutoMatchRequirements(opUserId2, autoReqsFix2, 4, true);
            if (c21 != null) matcherHandler.UnLockMatcherChain(c21.ChainId, opUserId2);
            var c22 = matcherMaker.AutoMatchRequirements(opUserId2, autoReqsFix2, 0, true, 3);
            if (c22 != null) matcherHandler.UnLockMatcherChain(c22.ChainId, opUserId2);
            */
            autoReqsNoFix2.Add(req1Id);
            autoReqsNoFix2.Add(req3Id);
            autoReqsNoFix2.Add(-1);
            var c23 = matcherMaker.AutoMatchRequirements(opUserId2, autoReqsNoFix2, 4, false);
            if (c23 != null) matcherHandler.UnLockMatcherChain(c23.ChainId, opUserId2);
            var c24 = matcherMaker.AutoMatchRequirements(opUserId2, autoReqsNoFix2, 0, false, 3);
            if (c24 != null) matcherHandler.UnLockMatcherChain(c24.ChainId, opUserId2);

            // Get the open chains with the latest match version of the given requirementid 
            //var chains2 = matcherHandler.GetMatcherChainsByRequirementId(req1Id, ChainStatus.OPEN);

            // LOCK the chain
            //var b = matcherHandler.LockMatcherChain(chains[0].ChainId, "aaa");

            // Set isLatestVersion to false to get all locked chains, because some locked chains are with old versions
            //var lockedChains = matcherHandler.GetMatcherChainsByUserId("1101", ChainStatus.LOCKED, false);

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
