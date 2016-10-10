ALTER TABLE [dbo].[Enterprises] ADD [InvoiceMaterial] varchar(1000) NULL 
GO

ALTER TABLE [dbo].[Enterprises] ADD [RegisterWarehouse] varchar(500) NULL 
GO

ALTER TABLE [dbo].[Enterprises] ADD [MaxTradeAmountPerMonth] varchar(50) NULL 
GO

ALTER TABLE [dbo].[Enterprises] ADD [IsAcceptanceBillETicket] int NULL 
GO

TRUNCATE TABLE BusinessTypes;
INSERT INTO BusinessTypes(BusinessTypeName) VALUES('�ط�����');
INSERT INTO BusinessTypes(BusinessTypeName) VALUES('����');
INSERT INTO BusinessTypes(BusinessTypeName) VALUES('���й�˾');
INSERT INTO BusinessTypes(BusinessTypeName) VALUES('�����');
INSERT INTO BusinessTypes(BusinessTypeName) VALUES('������Ӫ');
INSERT INTO BusinessTypes(BusinessTypeName) VALUES('��Ӫ��ҵ');


TRUNCATE TABLE PaymentMethods;
INSERT INTO PaymentMethods(PaymentMethodName)VALUES('��㣨�ֽ�');
INSERT INTO PaymentMethods(PaymentMethodName)VALUES('���У����ӡ�ֽƱ��');
INSERT INTO PaymentMethods(PaymentMethodName)VALUES('����֤�����ڡ����ʣ�');
INSERT INTO PaymentMethods(PaymentMethodName)VALUES('���е��ӻ�Ʊ�����ӡ�ֽƱ��');
INSERT INTO PaymentMethods(PaymentMethodName)VALUES('��ҵ�жһ��Ʊ');


