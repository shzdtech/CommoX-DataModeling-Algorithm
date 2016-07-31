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
            user.UserId = 1000001;
            user.UserName = "1000001";
            user.Password = "abc123";
            user.Phone = "13166887987";
            user.LastLoginTime = DateTime.Now;
        }

        [Fact]
        public void TestRegisterConnection()
        {
            var handler = new UserManagerHandler();
            bool result = handler.userRegister(user);
            Console.WriteLine("userRegister result: " + result);

        }

        [Fact]
        public void TestLoginConnection()
        {
            var handler = new UserManagerHandler();
            bool loginresult = handler.userLogin(user);
            Console.WriteLine("loginresult result: " + loginresult);

        }
       


    }


}
