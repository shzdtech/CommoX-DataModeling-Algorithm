using Micro.Future.Business.DataAccess.Commo.CommoObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Micro.Future.Business.DataAccess.Commo.CommonInterface
{
    public interface IFinancialProduct
    {
        /// <summary>
        /// 查询所有理财产品
        /// </summary>
        /// <returns></returns>
        IList<FinancialProduct> QueryAllFinancialProducts();

        /// <summary>
        /// 查询单个理财产品
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        FinancialProduct QueryFinancialProduct(int productId);

        /// <summary>
        /// 新增理财产品
        /// </summary>
        /// <param name="productInfo"></param>
        /// <returns></returns>
        FinancialProduct CreateFinancialProduct(FinancialProduct productInfo);

        /// <summary>
        /// 更新理财产品信息
        /// </summary>
        /// <param name="productInfo"></param>
        /// <returns></returns>
        bool UpdateFinancialProduct(FinancialProduct productInfo);

        /// <summary>
        /// 删除理财产品
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        bool DeleteFinancialProduct(int productId);
    }
}
