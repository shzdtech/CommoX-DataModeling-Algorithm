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
        public User userRegister(User user)
        {
            using (var db = new CommoXContext())
            {
                db.Users.Add(user);
                int count = db.SaveChanges();
                if (count > 0)
                    return user;
                else
                    return null;
            }
        }

        /// <summary>
        /// 判断user.UserName与Password验证登录，PASSWORD要加密
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public User userLogin(User user)
        {
            using (var db = new CommoXContext())
            {
                var uses = db.Users.Where(u => u.UserName.Equals(user.UserName))
                                    .Where(u => u.Password.Equals(user.Password))
                                    .First();
                if (uses != null)
                {
                    user.LastLoginTime = DateTime.Now;
                    db.SaveChanges();
                    return user;
                }
                    
            }
            return null;
        }

        public Boolean userLogout(User user)
        {
            return true;
        }

        public User userUpdate(User user)
        {
            return user;
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
