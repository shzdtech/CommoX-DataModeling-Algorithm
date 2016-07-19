using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Micro.Future.Business.DataAccess.Commo.CommonInterface
{
    public interface IRequirement
    {
        IEnumerable<Requirement> queryRequirements(int userId);

        Requirement queryRequirementInfo(int requirementId);

        Boolean saveRequirement(Requirement require);
    }
}
