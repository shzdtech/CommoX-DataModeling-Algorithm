ALTER TABLE [dbo].[Enterprises] ADD [InvoiceMaterial] varchar(1000) NULL 
GO

ALTER TABLE [dbo].[Enterprises] ADD [RegisterWarehouse] varchar(500) NULL 
GO

ALTER TABLE [dbo].[Enterprises] ADD [MaxTradeAmountPerMonth] varchar(50) NULL 
GO

ALTER TABLE [dbo].[Enterprises] ADD [IsAcceptanceBillETicket] int NULL 
GO

ALTER TABLE [dbo].[BusinessTypes] ADD DEFAULT 0 FOR [ParentId]
GO

ALTER TABLE [dbo].[BusinessTypes] ADD DEFAULT 0 FOR [StateId]
GO

TRUNCATE TABLE BusinessTypes;
INSERT INTO BusinessTypes(BusinessTypeName) VALUES('地方国企');
INSERT INTO BusinessTypes(BusinessTypeName) VALUES('央企');
INSERT INTO BusinessTypes(BusinessTypeName) VALUES('上市公司');
INSERT INTO BusinessTypes(BusinessTypeName) VALUES('混合制');
INSERT INTO BusinessTypes(BusinessTypeName) VALUES('大型民营');
INSERT INTO BusinessTypes(BusinessTypeName) VALUES('民营企业');

ALTER TABLE [dbo].[PaymentMethods] ADD DEFAULT 1 FOR [StateId]
GO

TRUNCATE TABLE PaymentMethods;
INSERT INTO PaymentMethods(PaymentMethodName)VALUES('电汇（现金）');
INSERT INTO PaymentMethods(PaymentMethodName)VALUES('银承（电子、纸票）');
INSERT INTO PaymentMethods(PaymentMethodName)VALUES('信用证（国内、国际）');
INSERT INTO PaymentMethods(PaymentMethodName)VALUES('银行电子汇票（电子、纸票）');
INSERT INTO PaymentMethods(PaymentMethodName)VALUES('商业承兑会汇票');

ALTER TABLE [dbo].[Products] ALTER COLUMN [ProductTypeId] int 
GO
ALTER TABLE [dbo].[Products] ADD DEFAULT 1 FOR [StateId]
GO
ALTER TABLE [dbo].[ProductTypes] ADD DEFAULT 0 FOR [ParentId]
GO

ALTER TABLE [dbo].[ProductTypes] ADD DEFAULT 1 FOR [StateId]
GO

ALTER TABLE [dbo].[Enterprises] ALTER COLUMN [IsAcceptanceBillETicket] bit 
GO

CREATE TABLE [dbo].[FinancialProduct] (
[ProductId] int NOT NULL IDENTITY(1,1) ,
[BankAddress] varchar(500) NULL ,
[ProductTerm] int NULL ,
[ProductYield] float(53) NULL ,
[IsDeleted] bit NULL ,
[CreatedTime] datetime2 NULL ,
[UpdatedTime] datetime2 NULL ,
PRIMARY KEY ([ProductId])
)


CREATE TABLE [dbo].[Acceptances] (
[AcceptanceId] int NOT NULL IDENTITY(1,1) ,
[Amount] float(53) NULL ,
[DueDate] date NULL ,
[BankName] varchar(255) NULL ,
[AcceptanceType] varchar(50) NULL ,
[DrawTime] datetime2 NULL ,
[CreateTime] datetime2 NULL ,
[UpdateTime] datetime2 NULL ,
[Subsidies]	 FLOAT (53)    NULL,
	[YearSubsidies]	 FLOAT (53)    NULL,
PRIMARY KEY ([AcceptanceId])
)


