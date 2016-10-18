using Micro.Future.Business.DataAccess.Commo.CommoObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Micro.Future.Business.DataAccess.Commo.CommonInterface
{
    public interface IAcceptance
    {

        /// <summary>
        /// 查询所有承兑汇票
        /// </summary>
        /// <returns></returns>
        IList<Acceptance> QueryAllAcceptances();

        /// <summary>
        /// 查询单个承兑汇票
        /// </summary>
        /// <param name="acceptanceId"></param>
        /// <returns></returns>
        Acceptance QueryAcceptance(int acceptanceId);

        /// <summary>
        /// 新增承兑汇票
        /// </summary>
        /// <param name="acceptance"></param>
        /// <returns></returns>
        Acceptance CreateAcceptance(Acceptance acceptance);

        /// <summary>
        /// 更新承兑汇票信息
        /// </summary>
        /// <param name="acceptance"></param>
        /// <returns></returns>
        bool UpdateAcceptance(Acceptance acceptance);

        /// <summary>
        /// 删除承兑汇票
        /// </summary>
        /// <param name="acceptanceId"></param>
        /// <returns></returns>
        bool DeleteAcceptance(int acceptanceId);
    }
}
