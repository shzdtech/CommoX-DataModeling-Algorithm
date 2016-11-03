using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Micro.Future.Business.MongoDB.Commo.MongoInterface
{
    public interface IDataVisual
    {
        string getJsonData(string tablename, string startDateTime, string endDateTime);
    }
}
