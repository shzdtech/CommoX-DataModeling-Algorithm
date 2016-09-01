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
        public void TestInsert()
        {
            var reqHandler = new MatcherHandler();
            var req1 = new RequirementObject();
            var userId1 = "101";
            req1.Deleted = false;
            req1.EnterpriseId = 1;
            req1.UserId = userId1;
            req1.ProductName = "10";
            req1.ProductPrice = 100;
            req1.ProductQuantity = 1000;
            req1.RequirementStateId = 0;
            req1.RequirementTypeId = RequirementType.BUYER;
            req1.CreateTime = DateTime.Now;
            req1.ModifyTime = DateTime.Now;
            var id1 = reqHandler.AddRequirement(req1);

            var req2 = new RequirementObject();
            var userId2 = "102";
            req2.Deleted = false;
            req2.EnterpriseId = 1;
            req2.UserId = userId2;
            req2.ProductName = "10";
            req2.ProductPrice = 100;
            req2.ProductQuantity = 1000;
            req2.RequirementStateId = 0;
            req2.RequirementTypeId = RequirementType.SELLER;
            req2.CreateTime = DateTime.Now;
            req2.ModifyTime = DateTime.Now;
            var id2 = reqHandler.AddRequirement(req2);

            var req = new RequirementObject();
            var userId = "103";
            req.Deleted = false;
            req.EnterpriseId = 1;
            req.UserId = userId;
            req.ProductName = "10";
            req.ProductPrice = 100;
            req.ProductQuantity = 1000;
            req.RequirementStateId = 0;
            req.RequirementTypeId = RequirementType.MID;
            req.CreateTime = DateTime.Now;
            req.ModifyTime = DateTime.Now;
            var id = reqHandler.AddRequirement(req);
        }

        [Fact]
        public void TestChainDAL()
        {
            var chainDAL = new ChainDAL();
            var chainsByUid = chainDAL.QueryChains("101");

            var chainId = chainsByUid[0].ChainId;

            var getchain = chainDAL.GetChain(chainId);

            bool isAllConfirm = false;
            //bool isConfirm = false;
            //isConfirm = chainDAL.ConfirmChainRequirement(chainId, getchain.RequirementIdChain[0], out isAllConfirm);
            //isConfirm = chainDAL.ConfirmChainRequirement(chainId, getchain.RequirementIdChain[1], out isAllConfirm);
            //isConfirm = chainDAL.ConfirmChainRequirement(chainId, getchain.RequirementIdChain[2], out isAllConfirm);
            var foo = isAllConfirm;

        }



        [Fact]
        public void TestConnection()
        {
            var reqHandler = new MatcherHandler();
            var matcherHandler = new MatcherHandler();
            
            var req = new RequirementObject();
            var userId = "101";
            req.Deleted = false;
            req.EnterpriseId = 1;
            req.UserId = userId;
            req.ProductName = "10";
            req.ProductPrice = 100;
            req.ProductQuantity = 1000;
            req.RequirementStateId = 0;
            req.RequirementTypeId = RequirementType.BUYER;
            var id = reqHandler.AddRequirement(req);
            var queryRes = reqHandler.QueryRequirementInfo(req.RequirementId);

            //var chains = reqHandler.QueryRequirementChains(180);
            //var size = chains.Count();

            var res = reqHandler.CancelRequirement(req.RequirementId);

        }
    }
}
