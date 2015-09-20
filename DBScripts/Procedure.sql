

CREATE PROC ShowAllCategories 
AS
Select (select count(*) from Category) as TotalCategories,
Category.[NameRus],
Category.[CategoryId],
Category.[Priority],
ISNULL(sum(Product.[Count]),0) AS TotalDishes

FROM [SushiTest1].[dbo].[Category]
left join Product on Product.CategoryId = Category.CategoryId 
Group by Category.NameRus,Category.CategoryId,[Priority]
GO
----------------------
CREATE PROC ShowUnprocessedOrders
AS
Select Orders.OrderId, Street, House, Room, sum(dbo.Product.Price * dbo.OrderDetails.[Count]) as TotalPrice, OrdersTimeChanged.Time
From OrderDetails 
join dbo.Product on Product.ProductId = OrderDetails.ProductId 
join dbo.Orders on Orders.OrderId = OrderDetails.OrderDetailsId 
join dbo.OrdersTimeChanged on OrdersTimeChanged.OrderId = Orders.OrderId
Where StatusId =2 
GROUP BY Street,House,Room,Orders.OrderId, OrdersTimeChanged.Time
GO
-------------------------

CREATE PROCEDURE FindCategory @CategoryName nvarchar(50)
AS

Select (select count(*) from Category) as TotalCategories,
Category.[NameRus],
Category.[CategoryId],
Category.[Priority],
ISNULL(sum(Product.[Count]),0) AS TotalDishes

FROM [SushiTest1].[dbo].[Category]
left join Product on Product.CategoryId = Category.CategoryId
Where Category.NameRus  Like  '%'+@CategoryName+'%'
Group by Category.NameRus,Category.CategoryId,Category.[Priority]
-----------------------
CREATE PROC AllDishes
AS
SELECT Product.ProductId,Product.NameRus, (Category.[Priority]) AS [Priority],Category.NameRus AS Category, (ProductWeightDetails.Value) AS [Weight],(ProductWeightDetails.Name) AS NameOfWeight  ,Product.Price, (select count(*) from Product) as TotalDishes,
Product.Sale,Product.IsHided, Product.AddDate
From Product
join Category on Category.CategoryId = Product.CategoryId 
join ProductWeightDetails on ProductWeightDetails.ProductId = Product.ProductId
GO
------------

CREATE PROC FindDishes @DishName nvarchar(50)
AS
SELECT Product.ProductId,Product.NameRus, (Category.[Priority]) AS [Priority],Category.NameRus AS Category, (ProductWeightDetails.Value) AS [Weight],(ProductWeightDetails.Name) AS NameOfWeight  ,Product.Price, (select count(*) from Product) as TotalDishes,
Product.Sale,Product.IsHided, Product.AddDate
From Product
join Category on Category.CategoryId = Product.CategoryId 
join ProductWeightDetails on ProductWeightDetails.ProductId = Product.ProductId
Where Product.NameRus Like '%'+@DishName+'%'
GO