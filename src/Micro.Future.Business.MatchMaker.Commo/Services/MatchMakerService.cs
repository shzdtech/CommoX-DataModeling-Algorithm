﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Micro.Future.Business.MongoDB.Commo.Handler;
using Micro.Future.Business.MatchMaker.Abstraction.Services;
using Micro.Future.Business.MatchMaker.Commo.Scheduler;
using Micro.Future.Business.MatchMaker.Abstraction.Models;
using Micro.Future.Business.MatchMaker.Abstraction.Schedulers;
using Micro.Future.Business.MatchMaker.Commo.Models;
using Micro.Future.Business.MatchMaker.Commo.Config;

namespace Micro.Future.Business.MatchMaker.Commo.Service
{
    public class MatchMakerService: IMatchMakerService
    {
        public MatcherHandler matcherHandler = new MatcherHandler();
        public IScheduler scheduler;
        public BaseMatchMaker matcher;

        public MatchMakerService(MatcherConfig config)
        {
            if(config.MATCHER_TYPE == "greedy")
                matcher = new GreedyMatchMaker(matcherHandler);
            else if (config.MATCHER_TYPE == "rank")
                matcher = new RankingMatchMaker(matcherHandler);
            else // default
                matcher = new RankingMatchMaker(matcherHandler);
            scheduler = new TimeScheduler(matcher, config.TIME_INTERVAL_SECONDS);
        }

        public void start()
        {
            if (!scheduler.isStopped())
            {
                throw new Exception("Scheduler is already running");
            }
            Task t1 = new Task(scheduler.start);
            t1.Start();
        }

        public void stop()
        {
            scheduler.stop();
        }
    }
}
