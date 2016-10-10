ALTER TABLE [dbo].[Enterprises] ADD [InvoiceMaterial] varchar(1000) NULL 
GO

ALTER TABLE [dbo].[Enterprises] ADD [RegisterWarehouse] varchar(500) NULL 
GO

ALTER TABLE [dbo].[Enterprises] ADD [MaxTradeAmountPerMonth] varchar(50) NULL 
GO

ALTER TABLE [dbo].[Enterprises] ADD [IsAcceptanceBillETicket] int NULL 
GO

TRUNCATE TABLE BusinessTypes;
INSERT INTO BusinessTypes(BusinessTypeName) VALUES('地方国企');
INSERT INTO BusinessTypes(BusinessTypeName) VALUES('央企');
INSERT INTO BusinessTypes(BusinessTypeName) VALUES('上市公司');
INSERT INTO BusinessTypes(BusinessTypeName) VALUES('混合制');
INSERT INTO BusinessTypes(BusinessTypeName) VALUES('大型民营');
INSERT INTO BusinessTypes(BusinessTypeName) VALUES('民营企业');


TRUNCATE TABLE PaymentMethods;
INSERT INTO PaymentMethods(PaymentMethodName)VALUES('电汇（现金）');
INSERT INTO PaymentMethods(PaymentMethodName)VALUES('银承（电子、纸票）');
INSERT INTO PaymentMethods(PaymentMethodName)VALUES('信用证（国内、国际）');
INSERT INTO PaymentMethods(PaymentMethodName)VALUES('银行电子汇票（电子、纸票）');
INSERT INTO PaymentMethods(PaymentMethodName)VALUES('商业承兑会汇票');


