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
                db.SaveChanges();
                return true;
            }
        }

        public Boolean userLogin(User user)
        {
            return true;
        }

        public Boolean userLogout(User user)
        {
            return true;
        }

        public Boolean userUpdate(User user)
        {
            return true;
        }
    }
}
