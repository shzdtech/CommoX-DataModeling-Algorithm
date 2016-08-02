using Micro.Future.Business.MongoDB.Commo.BizObjects;
using Micro.Future.Business.MongoDB.Commo.Config;
using Micro.Future.Business.MongoDB.Commo.MongoInterface;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Micro.Future.Business.MongoDB.Commo.Handler
{
    public class RequirementHandler: IRequirement
    {
        private static MongoClient client = new MongoClient(MongoDBConfig.mongoAddr);
        private static IMongoDatabase db = client.GetDatabase(MongoDBConfig.DATABASE);
        private static IMongoCollection<RequirementObject> COL_REQUIREMENT = db.GetCollection<RequirementObject>(MongoDBConfig.COLLECTION_REQUIREMENT);
        private static IMongoCollection<ChainObject> COL_CHAIN = db.GetCollection<ChainObject>(MongoDBConfig.COLLECTION_CHAIN);
        private static IMongoCollection<MongoCounter> COL_COUNTER = db.GetCollection<MongoCounter>(MongoDBConfig.COLLECTION_COUNTERS);

        public delegate void OnRequirementChainChangedHandler(IEnumerable<ChainObject> chains);

        private Int32 getNextSequenceValue(String sequenceName)
        {
            var filter = Builders<MongoCounter>.Filter.Eq("_id", sequenceName);
            var update = Builders<MongoCounter>.Update.Inc(MongoDBConfig.FIELD_COUNTER_VALUE, 1).CurrentDate("ModifyTime"); ;
            var counter = COL_COUNTER.FindOneAndUpdate<MongoCounter>(filter, update);
            //var counter = COL_COUNTER.Find<MongoCounter>(filter).First();
            return counter.sequence_value;
        }

        public event OnRequirementChainChangedHandler OnChainChanged;

        public void CallOnChainChanged(List<ChainObject> chains)
        {
            OnChainChanged(chains);
        }

        public int AddRequirement(RequirementObject requirement)
        {
            requirement.RequirementId = getNextSequenceValue(MongoDBConfig.ID_REQUIREMENT);
            COL_REQUIREMENT.InsertOne(requirement);
            return requirement.RequirementId;
        }

        public bool UpdateRequirement(RequirementObject requirement)
        {
            var filter = Builders<RequirementObject>.Filter.Eq("RequirementId", requirement.RequirementId) &
                    Builders<RequirementObject>.Filter.Eq("Deleted", false);
            var res = COL_REQUIREMENT.ReplaceOne(filter, requirement);
            return res.IsAcknowledged;
        }

        public bool CancelRequirement(int requirementId)
        {
            var filter = Builders<RequirementObject>.Filter.Eq("RequirementId", requirementId) &
                    Builders<RequirementObject>.Filter.Eq("Deleted", false);
            var update = Builders<RequirementObject>.Update
                .Set("Deleted", true)
                .CurrentDate("ModifyTime");
            var res = COL_REQUIREMENT.UpdateOne(filter, update);
            if (res.IsAcknowledged && res.ModifiedCount.Equals(1)) {
                //TODO should be in a transaction
                //remove corresponding RequirementObjChain in the chain
                var filterChain = Builders<ChainObject>.Filter.AnyEq("RequirementIdChain", requirementId) &
                    Builders<ChainObject>.Filter.Eq("Deleted", false);
                var updateChain = Builders<ChainObject>.Update
                    .Set("Deleted", true)
                    .CurrentDate("ModifyTime");
                var chains = COL_CHAIN.Find<ChainObject>(filterChain).ToList();
                if(chains.Count > 0)
                {
                    foreach (var chain in chains)
                    {
                        chain.Deleted = true;
                    }
                    COL_CHAIN.UpdateMany(filterChain, updateChain);
                    OnChainChanged(chains);
                }
                return true;
            } else{
                return false;
            }
        }

/*        public bool UpdateRequirement(RequirementObject requirement)
        {
            throw new NotImplementedException();
        }
        */

        public IEnumerable<RequirementObject> QueryRequirements(string userId)
        {
            var filter = Builders<RequirementObject>.Filter.Eq("UserId", userId) &
                    Builders<RequirementObject>.Filter.Eq("Deleted", false);
            var res = COL_REQUIREMENT.Find<RequirementObject>(filter).ToList();
            return res;

        }

        public RequirementObject QueryRequirementInfo(int requirementId)
        {
            var filter = Builders<RequirementObject>.Filter.Eq("RequirementId", requirementId) &
                    Builders<RequirementObject>.Filter.Eq("Deleted", false);
            var res = COL_REQUIREMENT.Find<RequirementObject>(filter).First();
            return res;
        }

        public IEnumerable<RequirementObject> GetNewAddedRequirements()
        {
            var filter = Builders<RequirementObject>.Filter.Eq("RequirementStateId", 0) &
                    Builders<RequirementObject>.Filter.Eq("Deleted", false);
            var res = COL_REQUIREMENT.Find<RequirementObject>(filter).ToList();
            return res;
        }

        public IEnumerable<RequirementObject> GetProcessedRequirements()
        {
            var filter = Builders<RequirementObject>.Filter.Ne("RequirementStateId", 0) & 
                Builders<RequirementObject>.Filter.Eq("Deleted", false);
            var res = COL_REQUIREMENT.Find<RequirementObject>(filter).ToList();
            return res;
        }

        public void AddRequirementChain(ChainObject chain)
        {
            chain.ChainId = getNextSequenceValue(MongoDBConfig.ID_CHAIN);
            COL_CHAIN.InsertOne(chain);
        }

        public IEnumerable<ChainObject> QueryRequirementChains(int requirementId)
        {
            var filterChain = Builders<ChainObject>.Filter.AnyEq("RequirementIdChain", requirementId) &
                    Builders<ChainObject>.Filter.Eq("Deleted", false);
            var chains = COL_CHAIN.Find<ChainObject>(filterChain).ToList();
            return chains;
        }

        private void TestSampleEvent()
        {
            if (OnChainChanged != null)
            {
                var newChains = new List<ChainObject>();
                OnChainChanged(newChains);
            }
        }
    }
}
