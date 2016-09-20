using Micro.Future.Business.MongoDB.Commo.BizObjects;
using Micro.Future.Business.MongoDB.Commo.Client;
using Micro.Future.Business.MongoDB.Commo.Config;
using Micro.Future.Business.MongoDB.Commo.MongoInterface;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using MongoDB.Driver.Linq;

namespace Micro.Future.Business.MongoDB.Commo.Handler
{
    public class MatcherHandler: IMatcher
    {
        private IMongoCollection<ChainObject> COL_CHAIN;
        private IMongoCollection<RequirementObject> COL_REQUIREMENT;
        private IMongoCollection<MongoCounter> COL_COUNTER;

        public MatcherHandler()
        {
            var db = MongoClientSingleton.Instance.GetMongoClient().GetDatabase(MongoDBConfig.DATABASE);
            COL_CHAIN = db.GetCollection<ChainObject>(MongoDBConfig.COLLECTION_CHAIN);
            COL_COUNTER = db.GetCollection<MongoCounter>(MongoDBConfig.COLLECTION_COUNTERS);
            COL_REQUIREMENT = db.GetCollection<RequirementObject>(MongoDBConfig.COLLECTION_REQUIREMENT);
        }

        public enum ChainUpdateStatus
        {
            ADD = 1,
            DELETE = 2,
            UPDATE = 3
        }

        public delegate void OnRequirementChainChangedEvent(IEnumerable<ChainObject> chains, ChainUpdateStatus status);

        public event OnRequirementChainChangedEvent OnChainChanged;

        public void CallOnChainAdded(List<ChainObject> chains)
        {
            OnChainChanged?.Invoke(chains, ChainUpdateStatus.ADD);
        }

        private Int32 getNextSequenceValue(String sequenceName)
        {
            var filter = Builders<MongoCounter>.Filter.Eq("_id", sequenceName);
            var update = Builders<MongoCounter>.Update.Inc(MongoDBConfig.FIELD_COUNTER_VALUE, 1).CurrentDate("ModifyTime"); ;
            var counter = COL_COUNTER.FindOneAndUpdate<MongoCounter>(filter, update);
            //var counter = COL_COUNTER.Find<MongoCounter>(filter).First();
            return counter.sequence_value;
        }

        private Int32 getCurrentMatcherVersion()
        {
            var filter = Builders<MongoCounter>.Filter.Eq("_id", MongoDBConfig.ID_MATCHER);
            var counter = COL_COUNTER.Find<MongoCounter>(filter).First();
            return counter.sequence_value - 1; 
        }

        public void AddMatcherChains(IList<ChainObject> chains)
        {
            if (chains.Count == 0) return;
            var version = getNextSequenceValue(MongoDBConfig.ID_MATCHER);
            foreach (var chain in chains)
            {
                var chainId = getNextSequenceValue(MongoDBConfig.ID_CHAIN);
                chain.ChainId = chainId;
                chain.Version = version;
            }
            COL_CHAIN.InsertMany(chains);
        }

        public ChainObject GetChain(int chainId)
        {
            var filterChain = Builders<ChainObject>.Filter.Eq("ChainId", chainId) &
                    Builders<ChainObject>.Filter.Eq("Deleted", false);
            var chains = COL_CHAIN.Find<ChainObject>(filterChain);
            if (chains.Count() == 0) throw new Exception("Chain doesn't exist");
            return chains.First();
        }

        public IList<ChainObject> GetMatcherChains(ChainStatus stauts, bool isLatestVersion = true)
        {
            var version = 0;
            if (isLatestVersion) version = getCurrentMatcherVersion();
            var filterChain = Builders<ChainObject>.Filter.Eq("Deleted", false) &
                    Builders<ChainObject>.Filter.Eq("ChainStateId", (int)stauts) &
                    Builders<ChainObject>.Filter.Gte("Version", version);

            //var filterChain = Builders<ChainObject>.Filter.Gt("ChainId", 0);
            var chains = COL_CHAIN.Find<ChainObject>(filterChain).ToList();
            return chains;
        }

        public IList<ChainObject> GetMatcherChainsByRequirementId(int requirementId, ChainStatus stauts, bool isLatestVersion = true)
        {
            var version = 0;
            if (isLatestVersion) version = getCurrentMatcherVersion();
            var filterChain = Builders<ChainObject>.Filter.AnyEq("RequirementIdChain", requirementId) &
                    Builders<ChainObject>.Filter.Eq("Deleted", false) &
                    Builders<ChainObject>.Filter.Eq("ChainStateId", (int)stauts) &
                    Builders<ChainObject>.Filter.Gte("Version", version);

            //var filterChain = Builders<ChainObject>.Filter.Gt("ChainId", 0);
            var chains = COL_CHAIN.Find<ChainObject>(filterChain).ToList();
            return chains;
        }

        public IList<ChainObject> GetMatcherChainsByUserId(String userId, ChainStatus status, bool isLatestVersion = true)
        {
            var version = 0;
            if(isLatestVersion) version = getCurrentMatcherVersion();
            var filterChain = Builders<ChainObject>.Filter.AnyEq("UserIdChain", userId) &
                    Builders<ChainObject>.Filter.Eq("Deleted", false) &
                    Builders<ChainObject>.Filter.Eq("ChainStateId", (int)status) &
                    Builders<ChainObject>.Filter.Gte("Version", version);

            //var filterChain = Builders<ChainObject>.Filter.Gt("ChainId", 0);
            var chains = COL_CHAIN.Find<ChainObject>(filterChain).ToList();
            return chains;
        }

        public bool LockMatcherChain(int chainId, string operatorId)
        {
            var filterChain = Builders<ChainObject>.Filter.Eq("ChainId", chainId) &
                   Builders<ChainObject>.Filter.Eq("Deleted", false) &
                   Builders<ChainObject>.Filter.Eq("ChainStateId", (int)ChainStatus.OPEN);
            var chains = COL_CHAIN.Find<ChainObject>(filterChain);
            if (chains.Count() == 0) return false;
            var chain = chains.First();

            if(!LockRequirementIds(chain.RequirementIdChain)) return false;

            var update = Builders<ChainObject>.Update
                .Set("ChainStateId", (int)ChainStatus.LOCKED)
                .Set("OperatorUserId", operatorId)
                .CurrentDate("ModifyTime");
            var res1 = COL_CHAIN.UpdateOne(filterChain, update);

            if (res1.IsAcknowledged) return true;
            else return false;
        }

        public bool UnLockMatcherChain(int chainId, string operatorId)
        {
            var filterChain = Builders<ChainObject>.Filter.Eq("ChainId", chainId) &
                   Builders<ChainObject>.Filter.Eq("Deleted", false) &
                   Builders<ChainObject>.Filter.Eq("ChainStateId", (int)ChainStatus.LOCKED);
            var chains = COL_CHAIN.Find<ChainObject>(filterChain);
            if (chains.Count() == 0) return false;
            var chain = chains.First();

            if (!UnlockRequirementIds(chain.RequirementIdChain)) return false;

            var update = Builders<ChainObject>.Update
                .Set("ChainStateId", (int)ChainStatus.OPEN)
                .Set("OperatorUserId", operatorId)
                .CurrentDate("ModifyTime");
            var res1 = COL_CHAIN.UpdateOne(filterChain, update);

            if (res1.IsAcknowledged) return true;
            else return false;
        }
        
        public bool ConfirmMatcherChain(int chainId, string operatorId)
        {
            return updateChainStatus(chainId, ChainStatus.CONFIRMED, RequirementStatus.CONFIRMED, operatorId);
        }

        private bool updateChainStatus(int chainId, ChainStatus chainStatus, RequirementStatus reqStatus, string operatorId)
        {
            var filter = Builders<ChainObject>.Filter.Eq("ChainId", chainId) &
                    Builders<ChainObject>.Filter.Eq("Deleted", false);
            var chain = COL_CHAIN.Find<ChainObject>(filter).First();
            var update = Builders<ChainObject>.Update
                .Set("ChainStateId", (int)chainStatus)
                .Set("OperatorUserId", operatorId)
                .CurrentDate("ModifyTime");
            var res1 = COL_CHAIN.UpdateOne(filter, update);
            var reqfilter = Builders<RequirementObject>.Filter.In("RequirementId", chain.RequirementIdChain) &
                Builders<RequirementObject>.Filter.Eq("Deleted", false);
            var requpdate = Builders<RequirementObject>.Update
                .Set("RequirementStateId", (int)reqStatus)
                .CurrentDate("ModifyTime");
            var res2 = COL_REQUIREMENT.UpdateMany(reqfilter, requpdate);
            if (res1.IsAcknowledged && res2.IsAcknowledged) return true;
            else return false;
        }

        private bool updateRequirementStatus(int reqId, RequirementStatus reqUpdateStatus, RequirementStatus? reqFilterStatus = null)
        {
            var filter = Builders<RequirementObject>.Filter.Eq("RequirementId", reqId) &
                    Builders<RequirementObject>.Filter.Eq("Deleted", false);
            if (reqFilterStatus.HasValue)
            {
                filter = filter & Builders<RequirementObject>.Filter.Eq("RequirementStateId", (int)reqFilterStatus); 
            }
            var update = Builders<RequirementObject>.Update
                .Set("RequirementStateId", (int)reqUpdateStatus)
                .CurrentDate("ModifyTime");
            var res1 = COL_REQUIREMENT.UpdateOne(filter, update);
            if (res1.IsAcknowledged && res1.ModifiedCount == 1) return true;
            else return false;
        }

        public void CallOnChainRemoved(List<ChainObject> chains)
        {
            OnChainChanged?.Invoke(chains, ChainUpdateStatus.DELETE);
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
            if (res.IsAcknowledged && res.ModifiedCount.Equals(1))
            {
                //TODO should be in a transaction
                //remove corresponding RequirementObjChain in the chain
                var filterChain = Builders<ChainObject>.Filter.AnyEq("RequirementIdChain", requirementId) &
                    Builders<ChainObject>.Filter.Eq("Deleted", false);
                var updateChain = Builders<ChainObject>.Update
                    .Set("Deleted", true)
                    .CurrentDate("ModifyTime");
                var chains = COL_CHAIN.Find<ChainObject>(filterChain).ToList();
                if (chains.Count > 0)
                {
                    foreach (var chain in chains)
                    {
                        chain.Deleted = true;
                    }
                    COL_CHAIN.UpdateMany(filterChain, updateChain);
                    //OnChainChanged(chains);
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        public IEnumerable<RequirementObject> QueryAllRequirements()
        {
            var filter = Builders<RequirementObject>.Filter.Gt("RequirementId", 0) &
                    Builders<RequirementObject>.Filter.Eq("Deleted", false);
            var res = COL_REQUIREMENT.Find<RequirementObject>(filter).ToList();
            return res;
        }

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

        public IList<RequirementObject> getReqSortedByAmountDesc(RequirementType requirementType)
        {
            var requirementTypeId = (int)requirementType;
            var filter = Builders<RequirementObject>.Filter.Eq("RequirementStateId", (int)RequirementStatus.OPEN) &
                Builders<RequirementObject>.Filter.Eq("Deleted", false) & 
                Builders<RequirementObject>.Filter.Eq("RequirementTypeId", requirementTypeId);
            var builder = Builders<RequirementObject>.Sort;
            var sort = builder.Descending("TradeAmount");

            var res = COL_REQUIREMENT.Find<RequirementObject>(filter).Sort(sort).ToList();
            return res;
        }

        public IList<RequirementObject> QueryRequirementsByEnterpriseId(int enterpriseId, RequirementStatus? requirementState = null)
        {
            var filter = Builders<RequirementObject>.Filter.Eq("EnterpriseId", enterpriseId) &
                     Builders<RequirementObject>.Filter.Eq("Deleted", false);
            if(requirementState.HasValue)
                filter = filter & Builders<RequirementObject>.Filter.Eq("RequirementStateId", requirementState);
            var res = COL_REQUIREMENT.Find<RequirementObject>(filter).ToList();
            return res;
        }

        public IEnumerable<RequirementObject> QueryRequirementsByLinq(Func<RequirementObject, bool> selector)
        {
            return COL_REQUIREMENT.AsQueryable().Where(selector);
    
        }

        private bool LockRequirementIds(IList<int> reqIds)
        {
            var rollbackReqIds = new List<int>();
            var rollbackFlag = false;
            foreach (var r in reqIds)
            {
                if (!updateRequirementStatus(r, RequirementStatus.LOCKED, RequirementStatus.OPEN))
                {
                    rollbackFlag = true;
                    break;
                }
                else
                {
                    rollbackReqIds.Add(r);
                }
            }
            if (rollbackFlag)
            {
                foreach (var r in rollbackReqIds)
                {
                    updateRequirementStatus(r, RequirementStatus.OPEN, RequirementStatus.LOCKED);
                }
                return false;
            }
            else return true;
        }
        private bool UnlockRequirementIds(IList<int> reqIds)
        {
            var flag = true;
            foreach(var r in reqIds)
            {
                if(!updateRequirementStatus(r, RequirementStatus.OPEN, RequirementStatus.LOCKED)) flag = false;
            }
            return flag;
        }


        public bool ReplaceRequirementsForChain(int chainId, IList<int> replacedNodeIndexArr, IList<int> replacingRequirementIds)
        {
            
            // the replaced chain status should be LOCKED
            var filterChain = Builders<ChainObject>.Filter.Eq("ChainId", chainId) &
                    Builders<ChainObject>.Filter.Eq("Deleted", false) &
                    Builders<ChainObject>.Filter.Eq("ChainStateId", (int)ChainStatus.LOCKED);
            var chains = COL_CHAIN.Find<ChainObject>(filterChain);
            if (chains.Count() == 0) return false;
            var chain = chains.First();

            if (replacedNodeIndexArr.Count != replacingRequirementIds.Count || replacedNodeIndexArr.Count > chain.ChainLength) return false;

            if (!LockRequirementIds(replacingRequirementIds)) return false;

            var replacedRequirementIds = new List<int>();
            var index = 0;
            foreach(var i in replacedNodeIndexArr)
            {
                replacedRequirementIds.Add(chain.RequirementIdChain[i]);

                var r = QueryRequirementInfo(replacingRequirementIds[index]);

                chain.RequirementIdChain[i] = r.RequirementId;
                chain.UserIdChain[i] = r.UserId;
                chain.EnterpriseIdChain[i] = r.EnterpriseId;
                
                index += 1;
            }

            UnlockRequirementIds(replacedRequirementIds);

            return true;
        }
    }
}
