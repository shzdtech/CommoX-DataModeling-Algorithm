using Micro.Future.Business.MatchMaker.Abstraction.Models;
using Micro.Future.Business.MongoDB.Commo.Handler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Micro.Future.Business.MatchMaker.Commo.Models
{
    public class RankingMatchMaker : BaseMatchMaker
    {
        private MatcherHandler matcherHandler;
        public RankingMatchMaker(MatcherHandler mHandler)
        {
            matcherHandler = mHandler;
        }

        public void make()
        {
            var reqs = matcherHandler.GetUnprocessedRequirements();

            

        }
    }
}
