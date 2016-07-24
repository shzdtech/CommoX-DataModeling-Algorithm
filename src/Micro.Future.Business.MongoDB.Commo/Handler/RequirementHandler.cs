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

        public delegate void OnRequirementChainChangedHandler(IEnumerable<ChainObject> chains);

        public event OnRequirementChainChangedHandler OnChainChanged;

        public void AddRequirement(RequirementObject requirement)
        {
            COL_REQUIREMENT.InsertOne(requirement);
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
