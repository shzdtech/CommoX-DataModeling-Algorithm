using Micro.Future.Business.DataAccess.Commo.CommonInterface;
using Micro.Future.Business.DataAccess.Commo.CommoObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Micro.Future.Business.DataAccess.Commo.CommoHandler
{
    public class ProductHandler :IProduct
    {
        private CommoXContext db = null;
        public ProductHandler(CommoXContext dbContext)
        {
            db = dbContext;
        }
        public IList<Product> queryAllProduct()
        {
            //using (var db = new CommoXContext())
            {
                var products = db.Products.ToList();
                return products;
            }
        }

        public Product queryProduct(int productId)
        {
            //using (var db = new CommoXContext())
            {
                return db.Products.FirstOrDefault(p => p.ProductId == productId);
            }
        }

        public Product saveProduct(Product product)
        {
            //using (var db = new CommoXContext())
            {
                db.Products.Add(product);
                int count = db.SaveChanges();
                if (count > 0)
                    return product;
                else
                    return null;
            }
        }

        public Product updateProduct(Product product)
        {
            throw new NotImplementedException();
        }

        public ProductType submitProductType(ProductType productType)
        {
            //using (var db = new CommoXContext())
            {
                db.ProductTypes.Add(productType);
                int count = db.SaveChanges();
                if (count > 0)
                    return productType;
                else
                    return null;
            }
        }
        public ProductType queryProductType(int productTypeId)
        {
            //using (var db = new CommoXContext())
            {
                return db.ProductTypes.FirstOrDefault(p => p.ProductTypeId == productTypeId);
            }
        }
        public IList<ProductType> queryAllProductType()
        {
            //using (var db = new CommoXContext())
            {
                return db.ProductTypes.ToList();
            }
        }
    }
}
