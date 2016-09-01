using Micro.Future.Business.DataAccess.Commo;
using Micro.Future.Business.DataAccess.Commo.CommoHandler;
using Micro.Future.Business.DataAccess.Commo.CommoObject;
using Microsoft.EntityFrameworkCore;
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
        string connect = @"Server=114.55.54.144; User Id=sa;; Password=shzdtech!123; Database=Commo;";
        CommoXContext db;
        public DataAcessTest()
        {
            user.UserName = "1000001";
            user.Password = "abc123";
            user.Phone = "13166887987";
            user.LastLoginTime = DateTime.Now;
            var optionsBuilder = new DbContextOptionsBuilder<CommoXContext>();
            optionsBuilder.UseSqlServer(connect);
            db = new CommoXContext(optionsBuilder.Options);

        }

        [Fact]
        public void TestRegisterConnection()
        {
            var handler = new UserManagerHandler(db);
            var usertest = handler.userRegister(user);
            Console.WriteLine("userRegister result: " + usertest.UserId);

        }

        [Fact]
        public void TestLoginConnection()
        {
            var handler = new UserManagerHandler(db);
            var userLogin = handler.userLogin(user);
            Console.WriteLine("loginresult result: " + userLogin);

        }
       
        [Fact]
        public void TestTradeHandler()
        {
            var tradehandler = new TradeHandler(db);
            Trade trade = new Trade();
            trade.TradeAmount = 1000000;
            trade.TradeFee = 100;
            trade.TradeSubsidy = 10;
            trade.TradeTime = DateTime.Now;
            trade.TradeTitle = "铜交易情况";
            trade.ParticipatorCount = 3;
            trade = tradehandler.submitTrade(trade);


            var orderhandler = new OrderHandler(db);
            if(trade.TradeId > 0)
            {
                //for each requirement ...

                Order order = new Order();
                order.TradeId = trade.TradeId;
                order.TradeSequence = 1;
                order.UserId = "100001";
                order.RequirementId = 100001;
                order.EnterpriseId = 10001;

                order.RequirementStateId = 1;
                order.RequirementTypeId = 2;
                order.RequirementRemarks = "I am 备注！！";

                order.CreateTime = DateTime.Now;
                order.ModifyTime = DateTime.Now;
                order.CompleteTime = DateTime.Now;
                order.OrderStateId = 101;
                order.ExecuteUserId = 100011;

                order.ProductName = "铜";
                order.ProductType = "有色";
                order.ProductSpecification = "货物规格：Cu_Ag>=99.95%";
                order.ProductPrice = 100;
                order.ProductQuantity = 10000;
                order.ProductUnit = "吨";
                order.WarehouseState = "上海";

                order.WarehouseAccount = "开户";
                order.InvoiceValue = "10000";
                order.InvoiceIssueDateTime = "2017-01-01";
                order.InvoiceTransferMode = "面给";
                //RequirementRule rule = new RequirementRule();
                //rule.RuleId = 1;
                //rule.RuleType = 1;
                //rule.Key = "企业类型";
                //rule.Value = "国企";
                //rule.OperationId = 1;

                //order.Rules.Add(rule);

                order = orderhandler.submitOrder(order);

                order = new Order();
                order.TradeId = trade.TradeId;
                order.TradeSequence = 2;
                order.UserId = "100002";
                order.RequirementId = 100002;
                order.EnterpriseId = 10002;

                order.RequirementStateId = 1;
                order.RequirementTypeId = 3;
                order.RequirementRemarks = "I am 购销！！";

                order.CreateTime = DateTime.Now;
                order.ModifyTime = DateTime.Now;
                order.OrderStateId = 100;
                order.ExecuteUserId = 100022;

                order.TradeAmount = 88888888;
                order.TradeProfit = 8;
                order.EnterpriseType = "国企";
                order.BusinessRange = "有色金属";

                order.WarehouseAccount = "开户";
                order.InvoiceValue = "20000";
                order.InvoiceIssueDateTime = "2017-01-01";
                order.InvoiceTransferMode = "面给";

                order = orderhandler.submitOrder(order);



                order = new Order();
                order.TradeId = trade.TradeId;
                order.TradeSequence = 3;
                order.UserId = "100003";
                order.RequirementId = 100003;
                order.EnterpriseId = 10003;

                order.RequirementStateId = 1;
                order.RequirementTypeId = 3;
                order.RequirementRemarks = "I am 备注！！";

                order.CreateTime = DateTime.Now;
                order.ModifyTime = DateTime.Now;

                order.OrderStateId = 102;
                order.ExecuteUserId = 100033;

                order.PaymentAmount = 9999999;
                order.PaymentDateTime = "2016-08-30";
                order.PaymentType = "现金";

                order.WarehouseAccount = "开户";
                order.InvoiceValue = "5000";
                order.InvoiceIssueDateTime = "2017-01-01";
                order.InvoiceTransferMode = "面给";

                order = orderhandler.submitOrder(order);


            }

        }

        [Fact]
        public void TestTradeUpdate()
        {
            int orderId = 10010;
            int exeUseruserId = 100077;
            int state = 101;
            var orderhandler = new OrderHandler(db);

            orderhandler.updateOrderState(orderId, exeUseruserId, state);

            orderId = 10011;
            exeUseruserId = 100088;
            state = 102;
            orderhandler.updateOrderState(orderId, exeUseruserId, state);

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
            var handler = new EnterpriseHandler(db);
            handler.AddEnterprise(enterprise);

        }


        [Fact]
        public void TestEnterpriseQuery()
        {
            Enterprise enterprise = new Enterprise();
           
            var handler = new EnterpriseHandler(db);
            enterprise = handler.QueryEnterpriseInfo(123);

        }

    }


}
