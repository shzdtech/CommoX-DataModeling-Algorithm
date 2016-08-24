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
        private CommoXContext db = null;
        public UserManagerHandler(CommoXContext dbContext)
        {
            db = dbContext;
        }

        public User userRegister(User user)
        {
            //using (var db = new CommoXContext())
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
            //using (var db = new CommoXContext())
            {
                var users = db.Users.Where(u => u.UserName.Equals(user.UserName))
                                    .Where(u => u.Password.Equals(user.Password))
                                    .First();
                if (users != null)
                {
                    users.LastLoginTime = DateTime.Now;
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

        public User GetUserById(string userId)
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
