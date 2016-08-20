using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Micro.Future.Business;
using Micro.Future.Business.MatchMaker.Commo.Config;
using Micro.Future.Business.MatchMaker.Commo.Service;
using System.Threading;
using Xunit;

namespace Micro.Future.Business.DataModeling.xUnit
{
    public class ChainMatchMakerTest
    {
        [Fact]
        public void TestChainMatcher()
        {
            var config = new MatcherConfig();
            var service = new MatchMakerService(config);
            //service.start();
            //Thread.Sleep(1 * 60 * 1000);
            service.matcher.make();
        }
    }
}
