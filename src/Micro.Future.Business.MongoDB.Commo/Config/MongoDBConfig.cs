using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace Micro.Future.Business.MongoDB.Commo.Config
{
    public static class MongoDBConfig
    {
        public static String mongoAddr { get; set; }
            //=  "mongodb://root:Xgmz372701@114.55.54.144:3717";
        public static String COLLECTION_REQUIREMENT { get; set; } = "requirement";
        public static String COLLECTION_CHAIN { get; set; } = "chain";
        // 生产环境数据库
        //public const String DATABASE = "Production";
        // 测试环境数据库
        public static String DATABASE { get; set; }

        // 数据可视化DB
        public static String DATAVISUAL_DB { get; set; }

            //= "testdb2";
        //public const String DATABASE = "testdb2";
        public static String COLLECTION_COUNTERS { get; set; } = "counters";
        public static String ID_REQUIREMENT { get; set; } = "RequirementId";
        public static String ID_CHAIN { get; set; } = "ChainId";
        public static String ID_MATCHER { get; set; } = "MatcherVersion";
        public static String FIELD_COUNTER_VALUE { get; set; } = "sequence_value";


        public static void load(IConfigurationSection conf)
        {
            if (!String.IsNullOrEmpty(conf["mongoAddr"])) mongoAddr = conf["mongoAddr"];
            if (!String.IsNullOrEmpty(conf["mongodb"])) DATABASE = conf["mongodb"];
            if (!String.IsNullOrEmpty(conf["DataVisualDB"])) DATAVISUAL_DB = conf["DataVisualDB"];
        }
    }
}
