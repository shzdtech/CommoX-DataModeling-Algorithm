using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Micro.Future.Business.MatchMaker.Abstraction.Services
{
    public interface IMatchMakerService
    {
        void start();
        void stop();
    }
}
