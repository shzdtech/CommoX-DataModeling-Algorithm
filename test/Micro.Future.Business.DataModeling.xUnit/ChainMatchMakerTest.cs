using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Micro.Future.Business;
using Micro.Future.Business.MatchMaker.Commo.Config;
using Micro.Future.Business.MatchMaker.Commo.Service;
using System.Threading;
using Xunit;
using Micro.Future.Business.MongoDB.Commo.Handler;
using Microsoft.Extensions.Configuration;
using System.Reflection;
using System.IO;
using Micro.Future.Business.MongoDB.Commo.Config;

namespace Micro.Future.Business.DataModeling.xUnit
{
    public class ChainMatchMakerTest
    {
        [Fact]
        public void TestChainMatcher()
        {
            MongoDBConfig.load(TestMongoConfig.conf);

            var config = new MatcherConfig();
            var service = new MatchMakerService(config);
            //service.start();
            //Thread.Sleep(1 * 60 * 1000);
            service.matcher.make();
        }

        [Fact]
        public void TestLock()
        {
            var section = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("MongoConf.json").Build().GetSection("mongoconfig");

            MongoDBConfig.load(section);
            var matcherHandler = new MatcherHandler();
            var chainHander = new ChainDAL();
//            var f = matcherHandler.LockMatcherChain(11303);
            var chain = chainHander.GetChain(11303);
            var chain2 = matcherHandler.GetMatcherChainsByRequirementId(6718, MongoDB.Commo.BizObjects.ChainStatus.LOCKED, false);
            var c = chain;
        }
    }
}
