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
    [BsonIgnoreExtraElements]
    public class ChainObject
    {
        public ChainObject (IList<RequirementObject> reqs)
        {
            var reqIdList = new List<int>();
            var userList = new List<string>();
            var enterpriseIdList = new List<int>();
            var minTradeAmount = reqs[0].TradeAmount;
            foreach (RequirementObject r in reqs)
            {
                reqIdList.Add(r.RequirementId);
                userList.Add(r.UserId);
                enterpriseIdList.Add(r.EnterpriseId);
                if (minTradeAmount > r.TradeAmount) minTradeAmount = r.TradeAmount;
            }
            RequirementIdChain = reqIdList;
            UserIdChain = userList;
            EnterpriseIdChain = enterpriseIdList;
            CreateTime = DateTime.Now;
            ModifyTime = DateTime.Now;
            ProductName = reqs[0].ProductName;
            ProductType = reqs[0].ProductType;
            TradeAmount = minTradeAmount;
            ChainLength = reqs.Count;
        }

        public ChainObject()
        {
        }

        [BsonId]
        [BsonRepresentation(BsonType.Int32)]
        public int ChainId { get; set; }
        public List<int> RequirementIdChain { get; set; }
        //public List<bool> IsConfirmChain { get; set; }
        public List<string> UserIdChain { get; set; }
        public List<int> EnterpriseIdChain { get; set; }
        public int Version { get; set; }
        //public bool isAllConfirmed { get; set; }
        public ChainStatus ChainStateId { get; set; }
        public string ProductName { get; set; }
        public string ProductType { get; set; }
        public decimal TradeAmount { get; set; }
        public int ChainLength { get; set; }
        public string OperatorUserId { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime CreateTime { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime ModifyTime { get; set; }
        public bool Deleted { get; set; }

        public string OperatorId { get; set; }
    }

    public enum ChainStatus
    {
        OPEN = 0,
        LOCKED = 1,
        CONFIRMED = 2
    }
}
