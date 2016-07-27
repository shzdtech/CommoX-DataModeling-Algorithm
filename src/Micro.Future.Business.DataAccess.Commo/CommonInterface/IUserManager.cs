using Micro.Future.Business.DataAccess.Commo.CommoObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Micro.Future.Business.DataAccess.Commo.CommonInterface
{
    public interface IUserManager
    {
        Boolean userRegister(User user);

        Boolean userLogin(User user);

        Boolean userLogout(User user);

        Boolean userUpdate(User user);

        /// <summary>
        /// 根据userId获取用户信息
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        User GetUserById(int userId);

        /// <summary>
        /// 根据userName，phone，email获取用户信息
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        User GetUser(User user);
    }
}
