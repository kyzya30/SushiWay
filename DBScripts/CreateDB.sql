Create DataBase SushiTest1
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
	[AddDate] datetime2 (2) NOT NULL
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

CREATE TABLE Ingridients
(
	[IngridientId] int  NOT NULL,
	[NameRus] NVARCHAR (50) NOT NULL UNIQUE,
	[NameUkr] NVARCHAR (50) NOT NULL UNIQUE
	)
GO
ALTER TABLE Ingridients
add constraint 
PK_Ingridients_IngridientId PRIMARY KEY(IngridientId)
GO

CREATE TABLE ProductIngridients
(
	[ProductId] int  NOT NULL,
	[IngridientsId] int NOT NULL
	)
GO

ALTER TABLE ProductIngridients
add constraint 
FK_ProductIngridients_ProductId FOREIGN KEY(ProductId)
REFERENCES Product(ProductId) 
GO

ALTER TABLE ProductIngridients
add constraint 
FK_ProductIngridients_IngridientsId FOREIGN KEY(IngridientsId)
REFERENCES Ingridients(IngridientId) 
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
	[Room] NVARCHAR (10) NOT NULL,
	[StatusId]int NOT NULL
	)

GO

ALTER TABLE Orders
add constraint 
PK_Orders_OrderId PRIMARY KEY(OrderId)

GO

CREATE TABLE OrderStatus
(
	[StatusId] int identity(1,1) NOT NULL,
	[StatusNameRus] NVARCHAR (50) NOT NULL,
	[StatusNameUkr] NVARCHAR (50) NOT NULL
)

GO

CREATE TABLE OrderDetails
(
	[OrderDetailsId] int  NOT NULL,
	[ProductId] int NOT NULL,
	[Count] int NOT NULL,
	[Price] decimal(9,2) NOT NULL
	)
GO
CREATE TABLE AdditionProductsForProduct
(
	[ProductId] int  NOT NULL,
	[ProductAdditionId] int  NOT NULL

	)
GO

ALTER TABLE AdditionProductsForProduct
add constraint 
FK_AdditionProdutsForProduct_OrderDetailsId FOREIGN KEY(ProductId)
REFERENCES Product(ProductId) 
GO
	
ALTER TABLE OrderDetails
add constraint 
FK_OrderDetails_OrderDetailsId FOREIGN KEY(OrderDetailsId)
REFERENCES Orders(OrderId) 
GO

ALTER TABLE OrderDetails
add constraint 
FK_OrderDetails_ProductId FOREIGN KEY(ProductId)
REFERENCES Product(ProductId) 
GO


ALTER TABLE Product
add constraint 
FK_Product_CategoryId FOREIGN KEY(CategoryId) 
	REFERENCES Category(CategoryId)  
GO

ALTER TABLE OrderStatus
add constraint 
PK_OrderStatus_StatusId PRIMARY KEY(StatusId)
GO

ALTER TABLE Orders
add constraint 
FK_Orders_StatusId FOREIGN KEY(StatusId)
REFERENCES OrderStatus(StatusId)  
GO

Create table Administrator
(
	[Login] NVARCHAR (30) NOT NULL,
	[Password]NVARCHAR (30) NOT NULL
)
GO

Create table OrdersTimeChanged
(
	[OrderId] int NOT NULL,
	[OrderStatus] int  NOT NULL,
	[Time] datetime2 (2) NOT NULL,

)
GO

ALTER TABLE OrdersTimeChanged
add constraint 
FK_OrdersTimeChanged_OrderId FOREIGN KEY(OrderId) 
	REFERENCES Orders(OrderId)  
GO

ALTER TABLE OrdersTimeChanged
add constraint 
FK_OrdersTimeChanged_OrderStatus FOREIGN KEY(OrderStatus) 
	REFERENCES OrderStatus(StatusId)  
GO

Create table Messages
(
	[MessagesId] int identity(1,1) NOT NULL,
	[Name] NVARCHAR (30) NOT NULL,
	[Email]NVARCHAR (250) NOT NULL,
	[Message]NVARCHAR (MAX) NOT NULL
)
GO

ALTER TABLE Administrator
add constraint 
PK_Administrator_Login PRIMARY KEY(Login)
GO
