using Micro.Future.Business.DataAccess.Commo.CommoObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Micro.Future.Business.DataAccess.Commo.CommonInterface
{
    public interface IRequirement
    {
        IList<Requirement> queryRequirements(int userId);

        Requirement queryRequirementInfo(int requirementId);

        Requirement saveRequirement(Requirement require);

        IList<RequirementFilter> queryRequirementFilters(int requirementId);

        bool saveRequirementFilters(IEnumerable<RequirementFilter> filters);

        /// <summary>
        /// 提交需求类型
        /// </summary>
        /// <param name="requirementType"></param>
        /// <returns></returns>
        RequirementType submitRequirementTypeId(RequirementType requirementType);
        /// <summary>
        /// 查询需求类型
        /// </summary>
        /// <param name="RequirementTypeId"></param>
        /// <returns></returns>
        RequirementType queryRequirementType(int RequirementTypeId);
        /// <summary>
        /// 查询所有需求类型
        /// </summary>
        /// <returns></returns>
        IList<RequirementType> queryAllRequirementType();
    }
}
