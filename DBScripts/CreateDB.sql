Create DataBase SushiTest1 collate Cyrillic_General_CI_AS
GO

use SushiTest1
GO

Create table Product
(
	[ProductId] int identity(1,1) NOT NULL,
	[CategoryId] int NOT NULL,
	[NameRus] NVARCHAR (50) NOT NULL UNIQUE,
	[NameUkr] NVARCHAR (50) NOT NULL UNIQUE,
	[NumberOfOrders] int NOT NULL,
	[Count] int NULL,
	[Energy] NVARCHAR (50) NOT NULL,
	[Balance] int NOT NULL,
	[Price] decimal(9,2) NOT NULL,
	[Sale] bit NOT NULL,
	[IsHided] bit NOT NULL,
	[AddDate] datetime2 (2) NOT NULL,
	[IngridientsRus] NVARCHAR (max),
	[IngridientsUkr] NVARCHAR (max),
	)
	GO
ALTER TABLE Product
add constraint 
PK_PRODUCT_ProductId PRIMARY KEY(ProductId)
GO

ALTER TABLE Product
ADD CONSTRAINT DF_Product_AddDate DEFAULT(GETDATE()) FOR AddDate
GO


CREATE TABLE ProductWeightDetails
(	
	[ProductId]int NOT NULL,
	[Name]NVARCHAR (50) NOT NULL,
	[Value] decimal(9,3) NOT NULL,
)
GO

ALTER TABLE ProductWeightDetails
add constraint 
FK_ProductWeightDetails_ProductId FOREIGN KEY(ProductId)
REFERENCES Product(ProductId) 
GO

ALTER TABLE ProductWeightDetails
add constraint 
PK_ProductWeightDetails_ProductId PRIMARY KEY(ProductId)
GO


CREATE TABLE Category
(
	[CategoryId] int identity(1,1) NOT NULL,
	[NameRus] NVARCHAR (50) NOT NULL UNIQUE,
	[NameUkr] NVARCHAR (50) NOT NULL UNIQUE,
	[Priority] int NOT NULL
	)
GO
	
ALTER TABLE Category
add constraint 
PK_Category_CategoryId PRIMARY KEY(CategoryId)
GO

CREATE TABLE Orders
(
	[OrderId] int identity(1,1) NOT NULL,
	[Name] NVARCHAR (50)  NULL,
	[PhoneNumber] NVARCHAR (50) NOT NULL,
	[Email] NVARCHAR (250)  NULL,
	[Street] NVARCHAR (50) NOT NULL,
	[House] NVARCHAR (10) NOT NULL,
	[Room] NVARCHAR (10) NOT NULL
	)

GO

ALTER TABLE Orders
add constraint 
PK_Orders_OrderId PRIMARY KEY(OrderId)

GO

CREATE TABLE OrderStatus
(
	[OrderStatusId] int identity(1,1) NOT NULL,
	[StatusNameRus] NVARCHAR (50) NOT NULL,
	[StatusNameUkr] NVARCHAR (50) NOT NULL
)

GO

CREATE TABLE OrderDetails
(
	[OrderDetailsId] int identity(1,1) NOT NULL ,
	[OrderId] int  NOT NULL,
	[ProductId] int NOT NULL,
	[Count] int NOT NULL,
	[Price] decimal(9,2) NOT NULL
	)
GO
ALTER TABLE OrderDetails
add constraint 
PK_OrderDetails_OrderDetailsId PRIMARY KEY(OrderDetailsId)
GO	


ALTER TABLE Product
add constraint 
FK_Product_CategoryId FOREIGN KEY(CategoryId) 
	REFERENCES Category(CategoryId)  
GO

ALTER TABLE OrderStatus
add constraint 
PK_OrderStatus_StatusId PRIMARY KEY(OrderStatusId)
GO

Create table Administrator
(
	[Login] NVARCHAR (30) NOT NULL,
	[Password]NVARCHAR (30) NOT NULL
)
GO

Create table OrdersTimeChanged
(
	[OrdersTimeChangedId] int identity(1,1) NOT NULL,
	[OrderId] int NOT NULL,
	[OrderStatus] int  NOT NULL,
	[Time] datetime2 (2) NOT NULL default(getdate()) 

)
GO
ALTER TABLE OrdersTimeChanged
add constraint 
PK_OrdersTimeChanged_OrdersTimeChangedIdPRIMARY PRIMARY KEY(OrdersTimeChangedId)
GO

Create table Massages
(
	[MessagesId] int identity(1,1) NOT NULL,
	[Name] NVARCHAR (30) NOT NULL,
	[Email]NVARCHAR (250) NOT NULL,
	[Text]NVARCHAR (MAX) NOT NULL
)
GO
ALTER TABLE Massages
add constraint 
PK_Massages_Messagesid PRIMARY KEY(MessagesId)
GO
ALTER TABLE Administrator
add constraint 
PK_Administrator_Login PRIMARY KEY(Login)
GO