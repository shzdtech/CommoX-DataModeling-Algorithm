using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Micro.Future.Business.MatchMaker.Commo.Config
{
    public class MatcherConfig
    {
        public int TIME_INTERVAL_SECONDS = 120;
        public string MATCHER_TYPE = "greedy";
        public string SCHEDULER_TYPE = "time";
        public bool START_BYDEFAULT = true;
    }
}
