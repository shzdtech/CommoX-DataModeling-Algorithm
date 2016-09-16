using Micro.Future.Business.DataAccess.Commo.CommoObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Micro.Future.Business.DataAccess.Commo.CommonInterface
{
    public interface IOperationRecord
    {
        /// <summary>
        /// 增加操作记录
        /// </summary>
        /// <param name="record"></param>
        /// <returns></returns>
        OperationRecord AddOperationRecord(OperationRecord record);

        /// <summary>
        /// 根据用户查询操作记录
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        IList<OperationRecord> QueryOperationRecord(String userId);
        /// <summary>
        /// 根据条件查询操作记录
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        IQueryable<OperationRecord> QueryEnterprises(Expression<Func<OperationRecord, bool>> predicate);

    }
}
