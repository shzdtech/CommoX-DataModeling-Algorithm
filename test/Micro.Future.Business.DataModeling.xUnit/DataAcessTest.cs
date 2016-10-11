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
        string connect = @"Server=114.55.54.144; User Id=sa;; Password=shzdtech!123; Database=Commo-test;";
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
        public void TestAcceptanceBill()
        {
            var commonhandler = new CommonHandler(db);
            AcceptanceBill bill = new AcceptanceBill();
            bill.EnterpriseId = 10002;
            bill.DrawerName = "billTest";
            bill.DrawerBankId = 1;
            bill.DrawerAccount = "337748823423";
            bill.CreateTime = DateTime.Now;
            bill.DueDate = DateTime.Today;

            commonhandler.submictAcceptanceBill(bill);

            var queryBill = commonhandler.queryAcceptanceBill(10000);

            var billList = commonhandler.queryAcceptanceBillByEnterprise(10002);


        }

       
        [Fact]
        public void TestBank()
        {
            var commonhandler = new CommonHandler(db);
            Bank bank = new Bank();
            bank.BankName = "中国银行Test";
            bank.BankAddress = "上海浦东新区Test";
            //commonhandler.submitBank(bank);

            var queryBank = commonhandler.queryBank(1001);
            var bankList = commonhandler.queryAllBank();
        }

        [Fact]
        public void TestBusinessType()
        {
            var commonhandler = new CommonHandler(db);
            BusinessType buss = new BusinessType();
            buss.BusinessTypeName = "国企Test";
            //commonhandler.submitBusinessType(buss);
            var queryBussiness = commonhandler.queryBusinessType(100);
            var queryBussinessList = commonhandler.queryAllBusinessType();

        }

        [Fact]
        public void TestPaymentMethod()
        {
            var commonhandler = new CommonHandler(db);
            PaymentMethod payment = new PaymentMethod();
            payment.PaymentMethodName = "现金Test";
            //commonhandler.submitPaymentMethod(payment);
            var queryPaymentMethod = commonhandler.queryPaymentMethod(10);
            var queryPaymentMethodList = commonhandler.queryAllPaymentMethod();

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
                order.ExecuteUserId = "100011";

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
                order.ExecuteUserId = "100022";

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
                order.ExecuteUserId = "100033";

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
            String exeUseruserId = "100077";
            int state = 101;
            var orderhandler = new OrderHandler(db);

            orderhandler.updateOrderState(orderId, exeUseruserId, state);

            orderId = 10011;
            exeUseruserId = "100088";
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
            enterprise.Name = "上海栈道1";
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
            enterprise.EmailAddress = "1112223@email.com";
            enterprise.MobilePhone = "13122332222";
            enterprise.LicenseImagePath = "c:/pic/aa.img";

            var handler = new EnterpriseHandler(db);
            if(handler.ValidationEnterpriceRegister(enterprise.Name , enterprise.EmailAddress))
                handler.AddEnterprise(enterprise);

        }

        [Fact]
        public void TestEnterpriseUpdate()
        {
            Enterprise enterprise = new Enterprise();

            var handler = new EnterpriseHandler(db);
            enterprise = handler.QueryEnterpriseInfo(123);
            enterprise.EmailAddress = "test111@test.com";
            enterprise.Address = "test address";
            handler.UpdateEnterprise(enterprise);

        }

        [Fact]
        public void TestEnterpriseUpdateState()
        {
            int enterpriseId = 123;
            int state = 5;
            Enterprise enterprise = new Enterprise();

            var handler = new EnterpriseHandler(db);
            handler.UpdateEnterpriseState(enterpriseId, state);

        }

        [Fact]
        public void TestEnterpriseQuery()
        {
            Enterprise enterprise = new Enterprise();
           
            var handler = new EnterpriseHandler(db);
            enterprise = handler.QueryEnterpriseInfo(123);

        }

        [Fact]
        public void TestProductQuery()
        {
            Product product = new Product();

            var handler = new ProductHandler(db);
            product = handler.queryProduct(2);
            var productList = handler.queryAllProduct();

        }

        [Fact]
        public void TestProductTypeQuery()
        {
            ProductType producttype = new ProductType();

            var handler = new ProductHandler(db);
            producttype = handler.queryProductType(2);
            var producttypeList = handler.queryAllProductType();

        }



    }





}
