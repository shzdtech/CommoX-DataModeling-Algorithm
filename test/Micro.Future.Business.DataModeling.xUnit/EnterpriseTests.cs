using Micro.Future.Business.DataAccess.Commo;
using Micro.Future.Business.DataAccess.Commo.CommoHandler;
using Micro.Future.Business.DataAccess.Commo.CommonInterface;
using Micro.Future.Business.DataAccess.Commo.CommoObject;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Micro.Future.Business.DataModeling.xUnit
{
    public class EnterpriseTests
    {
        private CommoXContext _context = null;
        public EnterpriseTests()
        {
            var optionsBuilder = new DbContextOptionsBuilder<CommoXContext>();
            optionsBuilder.UseInMemoryDatabase();

            _context = new CommoXContext(optionsBuilder.Options);
        }


        [Fact]
        public Enterprise TestAddEnterprise()
        {
            IEnterprise service = new EnterpriseHandler(_context);

            Enterprise enterprise = new Enterprise()
            {
                Name = "test company",
                Address = "test address",
                EmailAddress = "test@test.com",
                Contacts = "test",
                MobilePhone = "12345678901",
                Fax = "021123456"
            };

            var newEnterprise = service.AddEnterprise(enterprise);
            

            Assert.NotEqual<int>(0, newEnterprise.EnterpriseId);
            Assert.Equal<string>(enterprise.Name, newEnterprise.Name);

            return newEnterprise;
        }

        [Fact]
        public void TestDeleteEnterprise()
        {
            var newEnterprise = TestAddEnterprise();

            IEnterprise service = new EnterpriseHandler(_context);
            var result = service.DeleteEnterprise(newEnterprise.EnterpriseId);

            

        }
    }
}
