using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Micro.Future.Business.MongoDB.Commo.MongoInterface
{
    public interface IDataVisual
    {
        string getJsonData(string exchange, string productCode, string startDateTime, string endDateTime = null);

        string getJsonData(string exchange, string productCode, DateTime startDateTime, DateTime? endDateTime = null)
    }
}
