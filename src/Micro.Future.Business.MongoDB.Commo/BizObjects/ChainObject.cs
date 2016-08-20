using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Micro.Future.Business.MongoDB.Commo.BizObjects
{
    /// <summary>
    /// 撮合连 对象
    /// </summary>
    public class ChainObject
    {
        [BsonId]
        [BsonRepresentation(BsonType.Int32)]
        public int ChainId { get; set; }
        public List<int> RequirementIdChain { get; set; }
        public List<bool> IsConfirmChain { get; set; }
        public List<string> UserIdChain { get; set; }
        public bool isAllConfirmed { get; set; }

        public DateTime CreateTime { get; set; }
        public DateTime ModifyTime { get; set; }
        public bool Deleted { get; set; }
    }
}
