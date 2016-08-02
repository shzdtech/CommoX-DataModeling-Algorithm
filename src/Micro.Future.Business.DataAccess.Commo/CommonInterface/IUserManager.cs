using Micro.Future.Business.DataAccess.Commo.CommoObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Micro.Future.Business.DataAccess.Commo.CommonInterface
{
    public interface IUserManager
    {
        /// <summary>
        ///用户注册
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        User userRegister(User user);

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        User userLogin(User user);

        /// <summary>
        /// 用户注销登录
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Boolean userLogout(User user);

        /// <summary>
        /// 用户更新信息
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        User userUpdate(User user);

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
