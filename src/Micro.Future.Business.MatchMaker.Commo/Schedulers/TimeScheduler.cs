using Micro.Future.Business.MatchMaker.Abstraction.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Micro.Future.Business.MatchMaker.Abstraction.Schedulers;

namespace Micro.Future.Business.MatchMaker.Commo.Scheduler
{
    public class TimeScheduler: IScheduler
    {
        int timeIntervalSeconds;
        volatile Boolean stopped;
        BaseMatchMaker matcher;

        public TimeScheduler(BaseMatchMaker matcherMaker, int timeInterval)
        {
            timeIntervalSeconds = timeInterval;
            stopped = true;
            matcher = matcherMaker;
        }

        // set the status to stop
        // NOTE: It won't stop the thread immediately, if currently chain task is running 
        public void stop()
        {
            stopped = true;
        }

        public void start()
        {
            stopped = false;
            while (!stopped)
            {
                matcher.makeChainIncreament();
                Thread.Sleep(timeIntervalSeconds * 1000);
            }
        }

        public bool isStopped()
        {
            return stopped;
        }
    }
}