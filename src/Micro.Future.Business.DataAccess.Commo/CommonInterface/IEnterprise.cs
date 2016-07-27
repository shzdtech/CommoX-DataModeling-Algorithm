using Micro.Future.Business.DataAccess.Commo.CommoObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Micro.Future.Business.DataAccess.Commo.CommonInterface
{
    public interface IEnterprise
    {
        Enterprise QueryEnterpriseInfo(int enterpriseId);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="enterprise"></param>
        /// <returns>返回新的EnterpriseId</returns>
        int AddEnterprise(Enterprise enterprise);

        bool UpdateEnterprise(Enterprise enterprise);
    }
}
