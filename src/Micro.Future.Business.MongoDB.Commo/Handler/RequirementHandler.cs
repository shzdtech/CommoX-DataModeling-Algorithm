using Micro.Future.Business.MongoDB.Commo.BizObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Micro.Future.Business.MongoDB.Commo.Handler
{
    public class RequirementHandler
    {
        public delegate void OnRequirementChainChangedHandler(IEnumerable<ChainObject> chains);

        public event OnRequirementChainChangedHandler OnChainChanged;

        public bool AddRequirement(RequirementObject requirement)
        {
            throw new NotImplementedException();
        }

        public bool UpdateRequirement(RequirementObject requirement)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<RequirementObject> QueryRequirements(string userId)
        {
            throw new NotImplementedException();
        }

        public RequirementObject QueryRequirementInfo(int requirementId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ChainObject> QueryRequirementChains(int requirementId)
        {
            throw new NotImplementedException();
        }

        private void TestSampleEvent()
        {
            if (OnChainChanged != null)
            {
                var newChains = new List<ChainObject>();
                OnChainChanged(newChains);
            }
        }
    }
}
