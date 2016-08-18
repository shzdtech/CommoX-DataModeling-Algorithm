using Micro.Future.Business.DataAccess.Commo;
using Micro.Future.Business.DataAccess.Commo.CommoHandler;
using Micro.Future.Business.DataAccess.Commo.CommoObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Micro.Future.Business.DataModeling.xUnit
{
    public class DataAcessTest
    {
        User user = new User();


        public DataAcessTest()
        {
            user.UserName = "1000001";
            user.Password = "abc123";
            user.Phone = "13166887987";
            user.LastLoginTime = DateTime.Now;


        }

        [Fact]
        public void TestRegisterConnection()
        {
            var handler = new UserManagerHandler();
            var usertest = handler.userRegister(user);
            Console.WriteLine("userRegister result: " + usertest.UserId);

        }

        [Fact]
        public void TestLoginConnection()
        {
            var handler = new UserManagerHandler();
            var userLogin = handler.userLogin(user);
            Console.WriteLine("loginresult result: " + userLogin);

        }
       
        [Fact]
        public void TestTradeHandler()
        {
            var tradehandler = new TradeHandler();
            Trade trade = new Trade();
            trade.TradeAmount = 1000000;
            trade.TradeFee = 100;
            trade.TradeSubsidy = 10;
            trade.TradeTime = DateTime.Now;
            trade.TradeTitle = "铜交易情况";
            trade.ParticipatorCount = 3;
            trade = tradehandler.submitTrade(trade);


            var orderhandler = new OrderHandler();
            if(trade.TradeId > 0)
            {
                //for each requirement ...

                Order order = new Order();
                order.TradeId = trade.TradeId;
                order.TradeSequence = 1;
                order.UserId = "100001";
                order.RequirementFilters = "product=au;price=1000";
                order.RequirementId = 100001;
                order.RequirementRemarks = "备注！！";
                order.RequirementType = "出货";
                order.EnterpriseId = 10001;
                order.EnterpriseName = "A COMPANY";
                order.OrderState = "完成";
                order.CreateTime = DateTime.Now;
                order.ModifyTime = DateTime.Now;
                order.CompleteTime = DateTime.Now;
                order = orderhandler.submitOrder(order);

                order = new Order();
                order.TradeId = trade.TradeId;
                order.TradeSequence = 2;
                order.UserId = "100002";
                order.RequirementFilters = "paymethod=cash";
                order.RequirementId = 100002;
                order.RequirementRemarks = "备注！！";
                order.RequirementType = "贸易量";
                order.EnterpriseId = 10002;
                order.EnterpriseName = "B COMPANY";
                order.OrderState = "进行中";
                order.CreateTime = DateTime.Now;
                order.ModifyTime = DateTime.Now;
                order = orderhandler.submitOrder(order);

                order = new Order();
                order.TradeId = trade.TradeId;
                order.TradeSequence = 3;
                order.UserId = "100003";
                order.RequirementFilters = "paymethod=cash";
                order.RequirementId = 100003;
                order.RequirementRemarks = "备注！！";
                order.RequirementType = "出资";
                order.EnterpriseId = 10003;
                order.EnterpriseName = "C COMPANY";
                order.OrderState = "等待";
                order.CreateTime = DateTime.Now;
                order.ModifyTime = DateTime.Now;
                order = orderhandler.submitOrder(order);


            }

        }

        [Fact]
        public void TestTradeUpdate()
        {
            int orderId = 10005;
            string exeUserName = "test";
            string state = "完成";
            var orderhandler = new OrderHandler();

            orderhandler.updateOrderState(orderId, exeUserName, state);

            orderId = 10006;
            exeUserName = "test";
            state = "进行中";
            orderhandler.updateOrderState(orderId, exeUserName, state);

        }
        [Fact]
        public void TestEnterprise()
        {
            Enterprise enterprise = new Enterprise();
            enterprise.Address = "上海市";
            enterprise.AnnualInspection = "";
            enterprise.BusinessRange = "";
            enterprise.BusinessTypeId = 1;
            enterprise.Contacts = "";
            enterprise.CreateTime = DateTime.Now;
            enterprise.InvoicedQuantity = 10000000;
            enterprise.LegalRepresentative = "test";
            enterprise.Name = "上海栈道";
            enterprise.PaymentMethodId = 2;
            enterprise.PreviousProfit = 11100000;
            enterprise.PreviousSales = 5534354;
            enterprise.RegisterAccount = "14353442343";
            enterprise.RegisterAddress = "上海";
            enterprise.RegisterBankId = 1;
            enterprise.RegisterCapital = 1000000;
            enterprise.RegisterNumber = "2334234" ;
            enterprise.RegisterTime = DateTime.Parse("2012-02-14");
            enterprise.ReputationGrade = 5;
            var handler = new EnterpriseHandler();
            handler.AddEnterprise(enterprise);

        }


    }


}
