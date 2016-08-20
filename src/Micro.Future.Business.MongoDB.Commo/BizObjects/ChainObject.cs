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
        public ChainObject (IList<RequirementObject> reqs)
        {
            var reqIdList = new List<int>();
            var userList = new List<string>();
            var isConfirmChain = new List<bool>();
            foreach (RequirementObject r in reqs)
            {
                reqIdList.Add(r.RequirementId);
                userList.Add(r.UserId);
                IsConfirmChain.Add(false);
            }
            RequirementIdChain = reqIdList;
            UserIdChain = userList;
            IsConfirmChain = isConfirmChain;
            isAllConfirmed = false;
            CreateTime = DateTime.Now;
            ModifyTime = DateTime.Now;
        }

        public ChainObject()
        {
        }

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
