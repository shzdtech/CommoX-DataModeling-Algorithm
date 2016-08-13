using Micro.Future.Business.DataAccess.Commo.CommonInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Micro.Future.Business.DataAccess.Commo.CommoObject;

namespace Micro.Future.Business.DataAccess.Commo.CommoHandler
{
    public class CommonHandler : ICommon
    {
        public AcceptanceBill queryAcceptanceBill(int AcceptanceBillId)
        {
            throw new NotImplementedException();
        }

        public IList<AcceptanceBill> queryAcceptanceBillByUser(int userId)
        {
            throw new NotImplementedException();
        }

        public IList<Bank> queryAllBank()
        {
            throw new NotImplementedException();
        }

        public IList<BusinessType> queryAllBusinessType()
        {
            throw new NotImplementedException();
        }

        public IList<OrderState> queryAllOrderState()
        {
            throw new NotImplementedException();
        }

        public IList<PaymentMethod> queryAllPaymentMethod()
        {
            throw new NotImplementedException();
        }

        public Bank queryBank(int bankId)
        {
            throw new NotImplementedException();
        }

        public BusinessType queryBusinessType(int BusinessTypeId)
        {
            throw new NotImplementedException();
        }

        public OrderState queryOrderState(int stateId)
        {
            throw new NotImplementedException();
        }

        public PaymentMethod queryPaymentMethod(int PaymentMethodId)
        {
            throw new NotImplementedException();
        }

        public AcceptanceBill submictAcceptanceBill(AcceptanceBill bill)
        {
            throw new NotImplementedException();
        }

        public OrderState submictOrderState(OrderState orderstate)
        {
            throw new NotImplementedException();
        }

        public Bank submitBank(Bank bank)
        {
            throw new NotImplementedException();
        }

        public BusinessType submitBusinessType(BusinessType type)
        {
            throw new NotImplementedException();
        }

        public PaymentMethod submitPaymentMethod(PaymentMethod paymethod)
        {
            throw new NotImplementedException();
        }
    }
}
