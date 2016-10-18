using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Micro.Future.Business.DataModeling.xUnit
{
    public static class TestMongoConfig
    {
        public static IConfigurationSection conf = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("MongoConf.json").Build().GetSection("mongoconfig");
    }
}
