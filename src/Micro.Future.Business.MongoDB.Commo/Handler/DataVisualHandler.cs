using Micro.Future.Business.MongoDB.Commo.Client;
using Micro.Future.Business.MongoDB.Commo.Config;
using Micro.Future.Business.MongoDB.Commo.MongoInterface;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Micro.Future.Business.MongoDB.Commo.Handler
{
    public class DataVisualHandler : IDataVisual
    {
        private IMongoDatabase db;

        public DataVisualHandler()
        {
            db = MongoClientSingleton.Instance.GetMongoClient().GetDatabase(MongoDBConfig.DATAVISUAL_DB);
        }

        public string getJsonData(string tablename, string startDateTime, string endDateTime)
        {
            var collection = db.GetCollection<BsonDocument>(tablename);
            var filter = Builders<BsonDocument>.Filter.Gte("DATETIME", startDateTime) &
                Builders<BsonDocument>.Filter.Lt("DATETIME", endDateTime);
            var res = collection.Find(filter).ToList().ToJson();
            
            return res;
        }
    }
}
