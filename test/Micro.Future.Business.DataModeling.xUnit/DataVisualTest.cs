using Micro.Future.Business.MongoDB.Commo.Config;
using Micro.Future.Business.MongoDB.Commo.Handler;
using Micro.Future.Business.MongoDB.Commo.MongoInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Micro.Future.Business.DataModeling.xUnit
{

    public class DataVisualTest
    {
        [Fact]
        public void TestDataVisual()
        {
            MongoDBConfig.load(TestMongoConfig.conf);
            var handler = new DataVisualHandler();
            var data = handler.getJsonData("DCE", "JM", "2015-11-16 00:00:00", "2015-11-20 00:00:00");
            var n = data.Count();
        }
    }
}
