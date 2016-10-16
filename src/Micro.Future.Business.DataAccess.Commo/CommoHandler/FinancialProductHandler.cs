using Micro.Future.Business.DataAccess.Commo.CommonInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Micro.Future.Business.DataAccess.Commo.CommoObject;

namespace Micro.Future.Business.DataAccess.Commo.CommoHandler
{
    public class FinancialProductHandler : IFinancialProduct
    {
        private CommoXContext db = null;
        public FinancialProductHandler(CommoXContext dbContext)
        {
            db = dbContext;
        }
        public FinancialProduct CreateFinancialProduct(FinancialProduct productInfo)
        {
            db.FinancialProducts.Add(productInfo);
            int result = db.SaveChanges();
            if (result > 0)
                return productInfo;
            else
                return null;
        }

        public bool DeleteFinancialProduct(int productId)
        {
            var product = QueryFinancialProduct(productId);
            db.FinancialProducts.Remove(product);
            int result = db.SaveChanges();
            if (result > 0)
                return true;
            else
                return false;
        }

        public FinancialProduct QueryFinancialProduct(int productId)
        {
            return db.FinancialProducts.FirstOrDefault(p => p.ProductId == productId);

        }

        public IList<FinancialProduct> QueryAllFinancialProducts()
        {
            var products = db.FinancialProducts.ToList();
            return products;
        }

        public bool UpdateFinancialProduct(FinancialProduct productInfo)
        {
            var findProduct = QueryFinancialProduct(productInfo.ProductId);
            if (findProduct != null)
            {
                findProduct.BankAddress = productInfo.BankAddress;
                findProduct.ProductTerm = productInfo.ProductTerm;
                findProduct.ProductYield = productInfo.ProductYield;
                findProduct.IsDeleted = productInfo.IsDeleted;
                findProduct.CreatedTime = productInfo.CreatedTime;
                findProduct.UpdatedTime = DateTime.Now;
               
            }
            int result = db.SaveChanges();
            return result > 0;
        }
    }
}
