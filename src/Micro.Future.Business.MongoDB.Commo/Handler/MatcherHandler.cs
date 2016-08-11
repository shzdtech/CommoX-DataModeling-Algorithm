using Micro.Future.Business.MongoDB.Commo.BizObjects;
using Micro.Future.Business.MongoDB.Commo.Client;
using Micro.Future.Business.MongoDB.Commo.Config;
using Micro.Future.Business.MongoDB.Commo.MongoInterface;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Micro.Future.Business.MongoDB.Commo.Handler
{
    public class MatcherHandler
    {
        private IMongoCollection<ChainObject> COL_CHAIN;
        private IMongoCollection<MongoCounter> COL_COUNTER;

        public MatcherHandler()
        {
            var db = MongoClientSingleton.Instance.GetMongoClient().GetDatabase(MongoDBConfig.DATABASE);
            COL_CHAIN = db.GetCollection<ChainObject>(MongoDBConfig.COLLECTION_CHAIN);
            COL_COUNTER = db.GetCollection<MongoCounter>(MongoDBConfig.COLLECTION_COUNTERS);
        }

        public delegate void OnRequirementChainAddedHandler(IEnumerable<ChainObject> chains);

        public event OnRequirementChainAddedHandler OnChainAdded;

        public void CallOnChainAdded(List<ChainObject> chains)
        {
            OnChainAdded(chains);
        }

        private Int32 getNextSequenceValue(String sequenceName)
        {
            var filter = Builders<MongoCounter>.Filter.Eq("_id", sequenceName);
            var update = Builders<MongoCounter>.Update.Inc(MongoDBConfig.FIELD_COUNTER_VALUE, 1).CurrentDate("ModifyTime"); ;
            var counter = COL_COUNTER.FindOneAndUpdate<MongoCounter>(filter, update);
            //var counter = COL_COUNTER.Find<MongoCounter>(filter).First();
            return counter.sequence_value;
        }

        public void AddRequirementChain(ChainObject chain)
        {
            chain.ChainId = getNextSequenceValue(MongoDBConfig.ID_CHAIN);
            COL_CHAIN.InsertOne(chain);
        }
        
    }
}
