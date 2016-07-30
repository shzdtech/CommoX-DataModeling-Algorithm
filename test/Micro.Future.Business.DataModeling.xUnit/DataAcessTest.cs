using Micro.Future.Business.DataAccess.Commo;
using Micro.Future.Business.DataAccess.Commo.CommoHandler;
using Micro.Future.Business.DataAccess.Commo.CommoObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Micro.Future.Business.DataModeling.xUnit
{
    public class DataAcessTest
    {

        public DataAcessTest()
        {

        }

        [Fact]
        public void TestConnection()
        {
            var handler = new UserManagerHandler();
            var user = new User();
            user.UserId = 1000001;
            user.UserName = "1000001";
            user.Password = "abc123";
            user.Phone = "13166887987";
            bool result = handler.userRegister(user);

        }


    }


}
