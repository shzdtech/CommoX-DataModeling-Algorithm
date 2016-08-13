using Micro.Future.Business.DataAccess.Commo.CommoObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Micro.Future.Business.DataAccess.Commo.CommonInterface
{
    public interface ICommon
    {
        /// <summary>
        /// 新增承兑汇票
        /// </summary>
        /// <param name="bill"></param>
        /// <returns></returns>
        AcceptanceBill submictAcceptanceBill(AcceptanceBill bill);
        /// <summary>
        /// 查询承兑汇票
        /// </summary>
        /// <param name="AcceptanceBillId"></param>
        /// <returns></returns>
        AcceptanceBill queryAcceptanceBill(int AcceptanceBillId);
        /// <summary>
        /// 查询企业下承兑汇票
        /// </summary>
        /// <param name="enterpriseId"></param>
        /// <returns></returns>
        IList<AcceptanceBill> queryAcceptanceBillByEnterprise(int enterpriseId);
        /// <summary>
        /// 提交银行信息
        /// </summary>
        /// <param name="bank"></param>
        /// <returns></returns>
        Bank submitBank(Bank bank);
        /// <summary>
        /// 查询某个银行
        /// </summary>
        /// <param name="bankId"></param>
        /// <returns></returns>
        Bank queryBank(int bankId);
        /// <summary>
        /// 查询所有银行信息
        /// </summary>
        /// <returns></returns>
        IList<Bank> queryAllBank();
        /// <summary>
        /// 提交企业性质类型
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        BusinessType submitBusinessType(BusinessType type);
        /// <summary>
        /// 查询企业性质类型
        /// </summary>
        /// <returns></returns>
        IList<BusinessType> queryAllBusinessType();
        /// <summary>
        /// 查询某个企业性质
        /// </summary>
        /// <param name="BusinessTypeId"></param>
        /// <returns></returns>
        BusinessType queryBusinessType(int BusinessTypeId);
        /// <summary>
        /// 提交订单状态
        /// </summary>
        /// <param name="orderstate"></param>
        /// <returns></returns>
        OrderState submictOrderState(OrderState orderstate);
        /// <summary>
        /// 查询所有订单状态值
        /// </summary>
        /// <returns></returns>
        IList<OrderState> queryAllOrderState();
        /// <summary>
        /// 查询订单状态值
        /// </summary>
        /// <param name="stateId"></param>
        /// <returns></returns>
        OrderState queryOrderState(int stateId);
        /// <summary>
        /// 支付方式
        /// </summary>
        /// <param name="paymethod"></param>
        /// <returns></returns>
        PaymentMethod submitPaymentMethod(PaymentMethod paymethod);
        /// <summary>
        /// 查询所有支付方式
        /// </summary>
        /// <returns></returns>
        IList<PaymentMethod> queryAllPaymentMethod();
        /// <summary>
        /// 查询一种支付方式
        /// </summary>
        /// <param name="PaymentMethodId"></param>
        /// <returns></returns>
        PaymentMethod queryPaymentMethod(int PaymentMethodId);



    }
}
