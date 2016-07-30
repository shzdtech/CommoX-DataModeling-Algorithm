using Micro.Future.Business.DataAccess.Commo.CommonInterface;
using Micro.Future.Business.DataAccess.Commo.CommoObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Micro.Future.Business.DataAccess.Commo.CommoHandler
{
    public class UserManagerHandler : IUserManager
    {
        public Boolean userRegister(User user)
        {
            using (var db = new CommoXContext())
            {
                db.Users.Add(user);
                int count = db.SaveChanges();
                if (count > 0)
                    return true;
                else
                    return false;
            }
        }

        /// <summary>
        /// 判断user.UserName与Password验证登录，PASSWORD要加密
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public Boolean userLogin(User user)
        {
            using (var db = new CommoXContext())
            {
                int login = db.Users.Where(u => u.UserName.Equals(user.UserName))
                                    .Where(u => u.Password.Equals(user.Password))
                                    .Count();
                if (login > 0) return true;
            }
            return false;
        }

        public Boolean userLogout(User user)
        {
            return true;
        }

        public Boolean userUpdate(User user)
        {
            return true;
        }

        public User GetUserById(int userId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 通过User.UserName, User.Phone 获取用户信息
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public User GetUser(User user)
        {
            throw new NotImplementedException();
        }
    }
}
