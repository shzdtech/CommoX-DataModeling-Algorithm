using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Micro.Future.Business.MongoDB.Commo.Config
{
    public static class MongoDBConfig
    {
        public const String mongoAddr = "mongodb://root:shzl2701@114.55.54.144:3717";
        public const String COLLECTION_REQUIREMENT = "requirement";
        public const String COLLECTION_CHAIN = "chain";
        // 生产环境数据库
        public const String DATABASE = "Production";
        // 测试环境数据库
        //public const String DATABASE = "testdb";
        //public const String DATABASE = "testdb2";
        public const String COLLECTION_COUNTERS = "counters";
        public const String ID_REQUIREMENT = "RequirementId";
        public const String ID_CHAIN = "ChainId";
        public const String ID_MATCHER = "MatcherVersion";
        public const String FIELD_COUNTER_VALUE = "sequence_value";
    }
}
