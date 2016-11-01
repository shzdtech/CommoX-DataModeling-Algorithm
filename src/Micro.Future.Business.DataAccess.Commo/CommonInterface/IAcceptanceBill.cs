using Micro.Future.Business.DataAccess.Commo.CommoObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Micro.Future.Business.DataAccess.Commo.CommonInterface
{
    public interface IAcceptanceBill
    {
        /// <summary>
        /// 查询所有承兑汇票
        /// </summary>
        /// <returns></returns>
        IList<AcceptanceBill> QueryAllAcceptances(int enterpriseId);

        /// <summary>
        /// 查询单个承兑汇票
        /// </summary>
        /// <param name="acceptanceId"></param>
        /// <returns></returns>
        AcceptanceBill QueryAcceptance(int acceptanceId);

        /// <summary>
        /// 新增承兑汇票
        /// </summary>
        /// <param name="acceptance"></param>
        /// <returns></returns>
        AcceptanceBill CreateAcceptance(AcceptanceBill acceptance);

        /// <summary>
        /// 更新承兑汇票信息
        /// </summary>
        /// <param name="acceptance"></param>
        /// <returns></returns>
        bool UpdateAcceptance(AcceptanceBill acceptance);

        /// <summary>
        /// 删除承兑汇票
        /// </summary>
        /// <param name="acceptanceId"></param>
        /// <returns></returns>
        bool DeleteAcceptance(int acceptanceId);
    }
}
