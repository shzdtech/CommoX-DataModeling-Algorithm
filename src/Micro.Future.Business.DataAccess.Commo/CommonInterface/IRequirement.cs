using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Micro.Future.Business.DataAccess.Commo.CommonInterface
{
    public interface IRequirement
    {
        Requirement queryRequirement(String userId);

        Boolean submitRequirement(Requirement require);



    }
}
