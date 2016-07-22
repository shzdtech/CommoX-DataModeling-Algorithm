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

    }
}
