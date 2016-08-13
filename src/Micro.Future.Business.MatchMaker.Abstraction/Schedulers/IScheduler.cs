using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Micro.Future.Business.MatchMaker.Abstraction.Schedulers
{
    public interface IScheduler
    {
        void start();
        void stop();
        bool isStopped();
    }
}
