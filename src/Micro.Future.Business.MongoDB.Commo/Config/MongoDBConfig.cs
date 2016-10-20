using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;


namespace Micro.Future.Business.MongoDB.Commo.Config
{
    public static class MongoDBConfig
    {
        public static String mongoAddr;
            //"mongodb://root:shzl2701@114.55.54.144:3717";
        public static String COLLECTION_REQUIREMENT = "requirement";
        public static String COLLECTION_CHAIN = "chain";
        // 生产环境数据库
        public static String DATABASE;
        // 测试环境数据库
        //public const String DATABASE = "testdb";
        //public const String DATABASE = "testdb2";
        public static String COLLECTION_COUNTERS = "counters";
        public static String ID_REQUIREMENT = "RequirementId";
        public static String ID_CHAIN = "ChainId";
        public static String ID_MATCHER = "MatcherVersion";
        public static String FIELD_COUNTER_VALUE = "sequence_value";
        public static void load(IConfigurationSection conf)
        {
            if (!String.IsNullOrEmpty(conf["mongoAddr"])) mongoAddr = conf["mongoAddr"];
            if (!String.IsNullOrEmpty(conf["mongodb"])) DATABASE = conf["mongodb"];
        }
    }
}
