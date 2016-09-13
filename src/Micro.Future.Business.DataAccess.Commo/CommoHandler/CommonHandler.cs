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
        private CommoXContext db = null;

        public CommonHandler(CommoXContext dbContext)
        {
            db = dbContext;
        }
        public AcceptanceBill queryAcceptanceBill(int AcceptanceBillId)
        {
            //using (var db = new CommoXContext())
            {
                return db.AcceptanceBills.FirstOrDefault(p => p.AcceptanceBillId == AcceptanceBillId);
            }
        }

        public IList<AcceptanceBill> queryAcceptanceBillByEnterprise(int enterpriseId)
        {
            //using (var db = new CommoXContext())
            {
                return db.AcceptanceBills.Where(p => p.EnterpriseId == enterpriseId).ToList();
            }
        }

        public IList<Bank> queryAllBank()
        {
            //using (var db = new CommoXContext())
            {
                return db.Banks.ToList();
            }
        }

        public IList<BusinessType> queryAllBusinessType()
        {
            //using (var db = new CommoXContext())
            {
                return db.BusinessTypes.ToList();
            }
        }

        public IList<OrderState> queryAllOrderState()
        {
            //using (var db = new CommoXContext())
            {
                return db.OrderStates.ToList();
            }
        }

        public IList<PaymentMethod> queryAllPaymentMethod()
        {
            //using (var db = new CommoXContext())
            {
                return db.PaymentMethods.ToList();
            }
        }

        public Bank queryBank(int bankId)
        {
            //using (var db = new CommoXContext())
            {
                return db.Banks.FirstOrDefault(p => p.BankId == bankId);
            }
        }

        public BusinessType queryBusinessType(int BusinessTypeId)
        {
            //using (var db = new CommoXContext())
            {
                return db.BusinessTypes.FirstOrDefault(p => p.BusinessTypeId == BusinessTypeId);
            }
        }

        public OrderState queryOrderState(int stateId)
        {
            //using (var db = new CommoXContext())
            {
                return db.OrderStates.FirstOrDefault(p => p.OrderStateId == stateId);
            }
        }

        public PaymentMethod queryPaymentMethod(int PaymentMethodId)
        {
            //using (var db = new CommoXContext())
            {
                return db.PaymentMethods.FirstOrDefault(p => p.PaymentMethodId == PaymentMethodId);
            }
        }

        public AcceptanceBill submictAcceptanceBill(AcceptanceBill bill)
        {
            //using (var db = new CommoXContext())
            {
                db.AcceptanceBills.Add(bill);
                int count = db.SaveChanges();
                if (count > 0)
                    return bill;
                else
                    return null;
            }
        }

        public OrderState submictOrderState(OrderState orderstate)
        {
            //using (var db = new CommoXContext())
            {
                db.OrderStates.Add(orderstate);
                int count = db.SaveChanges();
                if (count > 0)
                    return orderstate;
                else
                    return null;
            }
        }

        public Bank submitBank(Bank bank)
        {
            //using (var db = new CommoXContext())
            {
                db.Banks.Add(bank);
                int count = db.SaveChanges();
                if (count > 0)
                    return bank;
                else
                    return null;
            }
        }

        public BusinessType submitBusinessType(BusinessType type)
        {
            //using (var db = new CommoXContext())
            {
                db.BusinessTypes.Add(type);
                int count = db.SaveChanges();
                if (count > 0)
                    return type;
                else
                    return null;
            }
        }

        public PaymentMethod submitPaymentMethod(PaymentMethod paymethod)
        {
            //using (var db = new CommoXContext())
            {
                db.PaymentMethods.Add(paymethod);
                int count = db.SaveChanges();
                if (count > 0)
                    return paymethod;
                else
                    return null;
            }
        }
    }
}
