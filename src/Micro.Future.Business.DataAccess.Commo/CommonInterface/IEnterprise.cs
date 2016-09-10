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

        /// <summary>
        /// 企业 更新 认证状态
        /// </summary>
        /// <param name="enterpriseId"></param>
        /// <param name="stateId"></param>
        /// <returns></returns>
        bool UpdateEnterpriseState(int enterpriseId, int stateId);
        /// <summary>
        /// 企业注册 有效性认证：企业名，企业注册邮箱
        /// </summary>
        /// <param name="enterpriseName"></param>
        /// <param name="adminEmail"></param>
        /// <returns></returns>
        bool ValidationEnterpriceRegister(string enterpriseName, string adminEmail);

    }
}
