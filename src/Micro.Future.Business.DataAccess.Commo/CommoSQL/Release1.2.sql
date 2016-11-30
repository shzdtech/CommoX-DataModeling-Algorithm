CREATE TABLE [dbo].[OrderImage]
(
	[ImageId] INT  IDENTITY (100, 1) NOT NULL PRIMARY KEY, 
    [OrderId] INT NULL, 
    [TradeId] INT NULL, 
    [ImageTypeId] INT NULL, 
    [ImagePath] NVARCHAR(255) NULL, 
    [Position] INT NULL, 
    [CreateTime] DATETIME NULL, 
    [UpdateTime] DATETIME NULL
)
go