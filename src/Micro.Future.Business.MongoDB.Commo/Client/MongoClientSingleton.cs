﻿using Micro.Future.Business.MongoDB.Commo.Config;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Micro.Future.Business.MongoDB.Commo.Client
{

    public sealed class MongoClientSingleton
    {
        private MongoClientSingleton()
        {
        }
        private static readonly Lazy<MongoClientSingleton> lazy = new Lazy<MongoClientSingleton>(() => new MongoClientSingleton());
        private static MongoClient client = new MongoClient(MongoDBConfig.mongoAddr);

        public static MongoClientSingleton Instance
        {
            get
            {
                return lazy.Value;
            }
        }

        public MongoClient GetMongoClient()
        {
            return client;
        }
    }
}
