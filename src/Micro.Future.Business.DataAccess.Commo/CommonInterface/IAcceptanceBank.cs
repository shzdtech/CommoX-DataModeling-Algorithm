using Micro.Future.Business.DataAccess.Commo.CommoObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Micro.Future.Business.DataAccess.Commo.CommonInterface
{
    public interface IAcceptanceBank
    {
        IList<AcceptanceBank> QueryAllBanks();

        AcceptanceBank QueryBankInfo(int bankId);

        AcceptanceBank CreateBank(AcceptanceBank newBank);

        bool UpdateBank(AcceptanceBank bank);

        bool DeleteBank(int bankId);
    }
}
