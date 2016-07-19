using Micro.Future.Business.DataAccess.Commo.CommonInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Micro.Future.Business.DataAccess.Commo.CommoHandler
{
    public class RequirementHandler : IRequirement
    {
        public Requirement queryRequirement(String userId)
        {
            return null;
        }
            

        public Boolean submitRequirement(Requirement require)
        {
            return true;
        }


    }
}
