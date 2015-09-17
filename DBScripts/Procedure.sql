
CREATE PROC ShowAllCategories 
AS
Select
Category.[NameRus],
Category.[CategoryId],
Category.[Priority],
ISNULL(sum(Product.[Count]),0) AS TotalDishes

FROM [SushiTest1].[dbo].[Category]
left join Product on Product.CategoryId = Category.CategoryId 
Group by Category.NameRus,Category.CategoryId,Category.[Priority]
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
-------------------------

CREATE PROCEDURE FindCategory @CategoryName nvarchar(50)
AS

Select
Category.[NameRus],
Category.[CategoryId],
Category.[Priority],
ISNULL(sum(Product.[Count]),0) AS TotalDishes

FROM [SushiTest1].[dbo].[Category]
left join Product on Product.CategoryId = Category.CategoryId
Where Category.NameRus  Like  '%'+@CategoryName+'%'
Group by Category.NameRus,Category.CategoryId,Category.[Priority]