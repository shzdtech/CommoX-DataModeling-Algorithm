using Micro.Future.Business.MongoDB.Commo.BizObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Micro.Future.Business.MongoDB.Commo.QueryObjects
{
    public class ChainQuery
    {
        String ProductType;
        String ProductName;
        int TradeAmount;
        ChainStatus chainStatus;
    }
}
