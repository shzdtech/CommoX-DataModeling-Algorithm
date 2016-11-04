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
        private const string TABLE_EOD = "eod";

        public DataVisualHandler()
        {
            db = MongoClientSingleton.Instance.GetMongoClient().GetDatabase(MongoDBConfig.DATAVISUAL_DB);
        }

        public string getJsonData(string exchange, string productCode, string startDateTime, string endDateTime = null)
        {
            var collection = db.GetCollection<BsonDocument>(TABLE_EOD);
            var date = System.DateTime.Now;
            var contracts = new List<String>();
            for(int i = 0; i < 12; i++)
            {
                var s = date.ToString("yyMM");
                contracts.Add(productCode + s);
                date = date.AddMonths(1);
            }
            var filter = Builders<BsonDocument>.Filter.Eq("exchange", exchange);
            if(endDateTime == null)
            {
                filter = filter & Builders<BsonDocument>.Filter.Eq("DATETIME", startDateTime);
            }
            else
            {
                filter = filter & Builders<BsonDocument>.Filter.Gte("DATETIME", startDateTime) &
                    Builders<BsonDocument>.Filter.Lt("DATETIME", endDateTime);
            }
            var res = new List<BsonDocument>();
            foreach(var contract in contracts)
            {
                var f = filter & Builders<BsonDocument>.Filter.Eq("contract", contract);
                res.AddRange(collection.Find(f).ToList());
            }
            
            return res.ToJson();

        }

    }
}
