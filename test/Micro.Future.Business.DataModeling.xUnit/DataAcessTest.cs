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
        User user = new User();

        public DataAcessTest()
        {
            user.UserName = "1000001";
            user.Password = "abc123";
            user.Phone = "13166887987";
            user.LastLoginTime = DateTime.Now;
        }

        [Fact]
        public void TestRegisterConnection()
        {
            var handler = new UserManagerHandler();
            var usertest = handler.userRegister(user);
            Console.WriteLine("userRegister result: " + usertest.UserId);

        }

        [Fact]
        public void TestLoginConnection()
        {
            var handler = new UserManagerHandler();
            var userLogin = handler.userLogin(user);
            Console.WriteLine("loginresult result: " + userLogin);

        }
       


    }


}
