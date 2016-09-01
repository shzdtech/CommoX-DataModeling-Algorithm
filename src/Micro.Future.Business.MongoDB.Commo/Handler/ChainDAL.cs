using Micro.Future.Business.MongoDB.Commo.MongoInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Micro.Future.Business.MongoDB.Commo.BizObjects;
using MongoDB.Driver;
using Micro.Future.Business.MongoDB.Commo.Client;
using Micro.Future.Business.MongoDB.Commo.Config;
using Micro.Future.Business.MongoDB.Commo.QueryObjects;

namespace Micro.Future.Business.MongoDB.Commo.Handler
{
    public class ChainDAL : IChainDAL
    {
        private IMongoCollection<RequirementObject> COL_REQUIREMENT;
        private IMongoCollection<ChainObject> COL_CHAIN;
        private IMongoCollection<MongoCounter> COL_COUNTER;

        public ChainDAL()
        {
            var db = MongoClientSingleton.Instance.GetMongoClient().GetDatabase(MongoDBConfig.DATABASE);
            COL_REQUIREMENT = db.GetCollection<RequirementObject>(MongoDBConfig.COLLECTION_REQUIREMENT);
            COL_CHAIN = db.GetCollection<ChainObject>(MongoDBConfig.COLLECTION_CHAIN);
            COL_COUNTER = db.GetCollection<MongoCounter>(MongoDBConfig.COLLECTION_COUNTERS);
        }

        /*
        public bool ConfirmChainRequirement(int chainId, int requirementId, out bool isAllConfirmed)
        {
            var filterChain = Builders<ChainObject>.Filter.Eq("ChainId", chainId) & 
                Builders<ChainObject>.Filter.AnyEq("RequirementIdChain", requirementId) &
                    Builders<ChainObject>.Filter.Eq("Deleted", false);
            var chains = COL_CHAIN.Find<ChainObject>(filterChain);
            var flag = true;
            if (chains.Count() == 0)
                throw new Exception("The Chain with the given chainId and requirementId doesn't exist");
            var chain = chains.First(); 
            for (int i = 0; i < chain.IsConfirmChain.Count(); i++)
            {
                if(chain.RequirementIdChain[i] == requirementId)
                {
                    chain.IsConfirmChain[i] = true;
                }
                if (!chain.IsConfirmChain[i]) flag = false;
            }
            isAllConfirmed = flag;
            chain.isAllConfirmed = isAllConfirmed;
            var res = COL_CHAIN.ReplaceOne(filterChain, chain);

            return res.IsAcknowledged;
        }
        */

        public ChainObject GetChain(int chainId)
        {
            var filterChain = Builders<ChainObject>.Filter.Eq("ChainId", chainId) &
                    Builders<ChainObject>.Filter.Eq("Deleted", false);
            var chains = COL_CHAIN.Find<ChainObject>(filterChain);
            if (chains.Count() == 0) throw new Exception("Chain doesn't exist");
            return chains.First();
        }

        /*
        public IList<RequirementObject> GetChainRequirements(int chainId)
        {
            // two steps:
            // 1. Query ChainObject by ChainId
            // 2. QUery Requirement by requirementId 
            throw new NotImplementedException();
        }
        */

        public IList<ChainObject> QueryChains(string userId)
        {
            var filterChain = Builders<ChainObject>.Filter.AnyEq("UserIdChain", userId) &
                    Builders<ChainObject>.Filter.Eq("Deleted", false);
            var chains = COL_CHAIN.Find<ChainObject>(filterChain).ToList();
            return chains;
        }

        public IList<ChainObject> QueryChainsByEnterpriseId(int enterpriseId, ChainStatus state)
        {
            throw new NotImplementedException();
        }

        public IList<ChainObject> QUeryChainsByChainQuery(ChainQuery chainQuery, string orderBy, out int pageNo, out int pageSize, out int totalCount)
        {
            throw new NotImplementedException();
        }
    }
}
