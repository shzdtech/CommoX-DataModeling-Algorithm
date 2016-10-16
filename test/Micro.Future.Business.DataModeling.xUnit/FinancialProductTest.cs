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
    public class FinancialProductTest
    {
        private CommoXContext _context = null;
        public FinancialProductTest()
        {
            var optionsBuilder = new DbContextOptionsBuilder<CommoXContext>();
            optionsBuilder.UseInMemoryDatabase();

            _context = new CommoXContext(optionsBuilder.Options);
        }


        [Fact]
        public FinancialProduct TestAddFinancialProduct()
        {
            IFinancialProduct service = new FinancialProductHandler(_context);

            FinancialProduct product = new FinancialProduct()
            {

                BankAddress = "上海市陆家嘴中国银行",
                ProductTerm=180,
                ProductYield=0.06,
                IsDeleted=true,
                CreatedTime = DateTime.Now,
                UpdatedTime= DateTime.Now
            
            };

            var newProduct = service.CreateFinancialProduct(product);


            Assert.NotEqual<int>(0, newProduct.ProductId);

            return newProduct;
        }

        [Fact]
        public void TestAllFunctions()
        {
            var newProduct = TestAddFinancialProduct();

            IFinancialProduct service = new FinancialProductHandler(_context);
            var oldProduct = service.QueryFinancialProduct(newProduct.ProductId);
            newProduct.ProductYield = 0.08;
            var updateProduct = service.UpdateFinancialProduct(newProduct);

            newProduct = TestAddFinancialProduct();

            IList<FinancialProduct> list = service.QueryAllFinancialProducts();

            var result = service.DeleteFinancialProduct(newProduct.ProductId);



        }
    }
}
