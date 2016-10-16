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
    public class AcceptanceTest
    {
        private CommoXContext _context = null;
        public AcceptanceTest()
        {
            var optionsBuilder = new DbContextOptionsBuilder<CommoXContext>();
            optionsBuilder.UseInMemoryDatabase();

            _context = new CommoXContext(optionsBuilder.Options);
        }



        [Fact]
        public Acceptance TestAddAcceptance()
        {
            IAcceptance service = new AcceptanceHandler(_context);

            Acceptance accpet = new Acceptance()
            {
                Amount = 100,
                DueDate = DateTime.Parse("2018-02-14"),
                BankName ="中国银行",
                AcceptanceType="电票",
                DrawTime= DateTime.Now,
                CreateTime = DateTime.Now,
                UpdateTime = DateTime.Now

            };

            var newAccpet = service.CreateAcceptance(accpet);


            Assert.NotEqual<int>(0, newAccpet.AcceptanceId);

            return newAccpet;
        }

        [Fact]
        public void TestAllFunctions()
        {
            var newProduct = TestAddAcceptance();

            IAcceptance service = new AcceptanceHandler(_context);
            var old = service.QueryAcceptance(newProduct.AcceptanceId);
            newProduct.AcceptanceType = "纸票";
            var updateProduct = service.UpdateAcceptance(newProduct);

            newProduct = TestAddAcceptance();

            IList<Acceptance> list = service.QueryAllAcceptances();

            var result = service.DeleteAcceptance(newProduct.AcceptanceId);



        }
    }
}
