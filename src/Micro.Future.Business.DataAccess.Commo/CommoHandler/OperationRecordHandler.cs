using Micro.Future.Business.DataAccess.Commo.CommonInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Micro.Future.Business.DataAccess.Commo.CommoObject;
using System.Linq.Expressions;

namespace Micro.Future.Business.DataAccess.Commo.CommoHandler
{
    public class OperationRecordHandler : IOperationRecord
    {
        private CommoXContext db = null;
        public OperationRecordHandler(CommoXContext dbContext)
        {
            db = dbContext;
        }
        public OperationRecord AddOperationRecord(OperationRecord record)
        {
            {
                record.CreateTime = DateTime.Now;
                db.OperationRecords.Add(record);
                int count = db.SaveChanges();
                if (count > 0)
                    return record;
                else
                    return null;
            }
        }

        public IQueryable<OperationRecord> QueryEnterprises(Expression<Func<OperationRecord, bool>> predicate)
        {
            return db.OperationRecords.Where(predicate);
        }

        public IList<OperationRecord> QueryOperationRecord(string userId)
        {
            throw new NotImplementedException();
        }
    }
}
