using Micro.Future.Business.DataAccess.Commo.CommoObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Micro.Future.Business.DataAccess.Commo.CommonInterface
{
    public interface IEnterprise
    {
        /// <summary>
        /// 查询企业信息
        /// </summary>
        /// <param name="enterpriseId"></param>
        /// <returns></returns>
        Enterprise QueryEnterpriseInfo(int enterpriseId);

        /// <summary>
        /// 查询企业信息
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        Enterprise QueryEnterpriseInfo(String name);

        /// <summary>
        /// 查询企业信息
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        IList<Enterprise> QueryEnterpriseList(String name);


        /// <summary>
        /// 添加企业信息
        /// </summary>
        /// <param name="enterprise"></param>
        /// <returns>返回新的EnterpriseId</returns>
        Enterprise AddEnterprise(Enterprise enterprise);

        /// <summary>
        /// 企业信息更新
        /// </summary>
        /// <param name="enterprise"></param>
        /// <returns></returns>
        bool UpdateEnterprise(Enterprise enterprise);

        bool UpdateEnterpriseState(int enterpriseId, int stateId);
    }
}
