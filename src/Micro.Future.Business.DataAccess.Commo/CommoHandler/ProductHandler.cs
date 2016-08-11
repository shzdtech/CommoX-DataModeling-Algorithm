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
        public IList<Product> queryAllProduct()
        {
            using (var db = new CommoXContext())
            {
                var products = db.Products.ToList();
                return products;
            }
        }

        public Product queryProduct(int productId)
        {
            using (var db = new CommoXContext())
            {
                return db.Products.SingleOrDefault(p => p.ProductId == productId);
            }
        }

        public Product saveProduct(Product product)
        {
            using (var db = new CommoXContext())
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
    }
}
