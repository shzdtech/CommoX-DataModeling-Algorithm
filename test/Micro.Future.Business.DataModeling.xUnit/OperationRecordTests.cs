using Micro.Future.Business.DataAccess.Commo;
using Micro.Future.Business.DataAccess.Commo.CommoHandler;
using Micro.Future.Business.DataAccess.Commo.CommonInterface;
using Micro.Future.Business.DataAccess.Commo.CommoObject;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;


namespace Micro.Future.Business.DataModeling.xUnit
{
    public class OperationRecordTests
    {
        private CommoXContext _context = null;

        public OperationRecordTests()
        {
            var optionsBuilder = new DbContextOptionsBuilder<CommoXContext>();
            optionsBuilder.UseInMemoryDatabase();

            _context = new CommoXContext(optionsBuilder.Options);
        }

        [Fact]
        public void Test_AddOperationRecord()
        {
            IOperationRecord service = new OperationRecordHandler(_context);

            OperationRecord record = new OperationRecord()
            {
                UserId = "100022",
                ObjectType = "Chain",
                ObjectValue = "100",
                Operation = "1",
                CreateTime = DateTime.Now

            };

            var newrecord = service.AddOperationRecord(record);
            Assert.NotEqual<int>(0, newrecord.Id);
            Assert.Equal<string>(record.UserId, record.UserId);

        }



    }
}
