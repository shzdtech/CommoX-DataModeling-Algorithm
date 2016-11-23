using Micro.Future.Business.MongoDB.Commo.Client;
using Micro.Future.Business.MongoDB.Commo.Config;
using Micro.Future.Business.MongoDB.Commo.MongoInterface;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization.Attributes;
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

        public string getJsonData(string exchange, string productCode, DateTime startDateTime, DateTime? endDateTime = null)
        {
            var collection = db.GetCollection<WindData>(TABLE_EOD);
            var date = System.DateTime.Now;
            var contracts = new List<String>();
            for (int i = 0; i < 12; i++)
            {
                var s = date.ToString("yyMM");
                contracts.Add(productCode + s);
                date = date.AddMonths(1);
            }
            var filter = Builders<WindData>.Filter.Eq("EXCHANGE", exchange);
            if (endDateTime == null)
            {
                filter = filter & Builders<WindData>.Filter.Eq("DATETIME", startDateTime);
            }
            else
            {
                var endDT = endDateTime.GetValueOrDefault();
                filter = filter & Builders<WindData>.Filter.Gte("DATETIME", startDateTime) &
                    Builders<WindData>.Filter.Lt("DATETIME", endDateTime.GetValueOrDefault());
            }
            var res = new List<WindData>();
            foreach (var contract in contracts)
            {
                var f = filter & Builders<WindData>.Filter.Eq("CONTRACT", contract);
                res.AddRange(collection.Find(f).ToList());
            }

            foreach (var r in res)
            {
                r.HIGH = convertNanToNull(r.HIGH);
                r.LOW = convertNanToNull(r.LOW);
                r.OPEN = convertNanToNull(r.OPEN);
                r.SETTLE = convertNanToNull(r.SETTLE);
                r.VOLUME = convertNanToNull(r.VOLUME);
                r.CLOSE = convertNanToNull(r.CLOSE);
            }

            var jsonWriterSettings = new JsonWriterSettings { OutputMode = JsonOutputMode.Strict };

            return res.ToJson(jsonWriterSettings);
        }

        public string getJsonData(string exchange, string productCode, string startDateTime, string endDateTime = null)
        {
            if(endDateTime == null)
            {
                return getJsonData(exchange, productCode, DateTime.Parse(startDateTime));
            }
            else
            {
                return getJsonData(exchange, productCode, DateTime.Parse(startDateTime), DateTime.Parse(endDateTime));
            }

        }

        private double? convertNanToNull(double? val)
        {
            if (val != null && Double.IsNaN(val.GetValueOrDefault(0)))
                return null;
            else return val;
        }
    }

    [BsonIgnoreExtraElements]
    public class WindData
    {
        [BsonId]
        public string Id { get; set; }
       
        public double? CLOSE { get; set; }

        public double? HIGH { get; set; }

        public double? SETTLE { get; set; }

        public double? OPEN { get; set; }

        public DateTime DATETIME { get; set; }

        public string CONTRACT { get; set; }

        public double? VOLUME { get; set; }

        public double? LOW { get; set; }

        public string EXCHANGE { get; set; }
    }
}
