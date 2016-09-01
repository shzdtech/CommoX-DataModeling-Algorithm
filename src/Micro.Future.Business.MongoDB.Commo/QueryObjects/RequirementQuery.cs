using Micro.Future.Business.MongoDB.Commo.BizObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Micro.Future.Business.MongoDB.Commo.QueryObjects
{
    public class RequirementQuery
    {
        //RequirementState、RequirementType、ProductName、ProductType、StartTradeAmount、EndTradeAmount
        RequirementStatus reqStatus;
        String ProductType;
        String ProductName;
        int StartTradeAmount;
        int endTradeAmount;
    }
}
