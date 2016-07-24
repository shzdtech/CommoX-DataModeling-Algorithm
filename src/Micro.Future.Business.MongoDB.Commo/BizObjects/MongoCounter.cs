using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Micro.Future.Business.MongoDB.Commo.BizObjects
{
    public class MongoCounter
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public String CounterId { get; set; }
        public int sequence_value { get; set; }
        public DateTime ModifyTime { get; set; }
    }
}
