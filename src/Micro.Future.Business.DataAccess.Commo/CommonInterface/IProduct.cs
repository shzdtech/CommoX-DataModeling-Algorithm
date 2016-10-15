using Micro.Future.Business.DataAccess.Commo.CommoObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Micro.Future.Business.DataAccess.Commo.CommonInterface
{
    public interface IProduct
    {
        /// <summary>
        /// 产品提交
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        Product saveProduct(Product product);

        /// <summary>
        /// 产品根据productId查询
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        Product queryProduct(int productId);


        /// <summary>
        /// 查询所有的产品列表
        /// </summary>
        /// <returns></returns>
        IList<Product> queryAllProduct();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="productTypeId"></param>
        /// <returns></returns>
        IList<Product> queryProductByType(int productTypeId);

        /// <summary>
        /// 更新产品的内容，根据productId查询到当前产品
        /// 然后更新整个对象
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        Product updateProduct(Product product);

        /// <summary>
        /// 提交产品类型
        /// </summary>
        /// <param name="productType"></param>
        /// <returns></returns>
        ProductType submitProductType(ProductType productType);

        /// <summary>
        /// 查询产品类型
        /// </summary>
        /// <param name="productTypeId"></param>
        /// <returns></returns>
        ProductType queryProductType(int productTypeId);
        /// <summary>
        /// 查询所有产品类型
        /// </summary>
        /// <returns></returns>
        IList<ProductType> queryAllProductType();


    }
}
