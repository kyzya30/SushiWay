CREATE PROC ShowAllCategories 
AS
Select (select count(*) from Category) as TotalCategories,
Category.[NameUkr],
Category.[NameRus],
Category.[CategoryId],
Category.[Priority],
ISNULL(sum(Product.[Count]),0) AS TotalDishes

FROM [SushiTest1].[dbo].[Category]
left join Product on Product.CategoryId = Category.CategoryId 
Group by Category.NameRus,Category.NameUkr,Category.CategoryId,[Priority]
GO
----------------------
CREATE PROC ShowUnprocessedOrders
AS
Select Orders.OrderId, Street, House, Room, sum(dbo.Product.Price * dbo.OrderDetails.[Count]) as TotalPrice, OrdersTimeChanged.Time
From OrderDetails 
join dbo.Product on Product.ProductId = OrderDetails.ProductId 
join dbo.Orders on Orders.OrderId = OrderDetails.OrderDetailsId 
join dbo.OrdersTimeChanged on OrdersTimeChanged.OrderId = Orders.OrderId
Where OrdersTimeChanged.OrderStatus =2 
GROUP BY Street,House,Room,Orders.OrderId, OrdersTimeChanged.Time
GO
-------------------------

CREATE PROCEDURE FindCategory @CategoryName nvarchar(50)
AS

Select (select count(*) from Category) as TotalCategories,
Category.[NameUkr],
Category.[NameRus],
Category.[CategoryId],
Category.[Priority],
ISNULL(sum(Product.[Count]),0) AS TotalDishes

FROM [SushiTest1].[dbo].[Category]
left join Product on Product.CategoryId = Category.CategoryId
Where Category.NameRus  Like  '%'+@CategoryName+'%'
Group by Category.NameRus,Category.NameUkr,Category.CategoryId,[Priority]
GO
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
-----------------

CREATE PROC ShowAllOrders
AS
select o.OrderId, o.Street, o.House, o.Room,q.MaxStatusTime,sum(p.Price * od.[Count]) as TotalPrice,
	os.StatusNameRus
from Orders o
inner join
(
	select otch.OrderId, otch.OrderStatus, q.MaxStatusTime 
	from OrdersTimeChanged otch
	inner join
	(
		select OrderId, max(Time) as MaxStatusTime
		from OrdersTimeChanged
		group by OrderId
	) q on otch.OrderId = q.OrderId and q.MaxStatusTime = otch.Time
) q on q.OrderId = o.OrderId
inner join OrderStatus os on os.OrderStatusId = q.OrderStatus
inner join OrderDetails od on od.OrderDetailsId = o.OrderId
join Product p on p.ProductId = od.ProductId 
group by o.OrderId, o.Street, o.House, o.Room, os.StatusNameRus, q.MaxStatusTime
GO
--------------------

CREATE PROC FindOrders @Order nvarchar(50)
AS
select o.OrderId, o.Street, o.House, o.Room,q.MaxStatusTime,sum(p.Price * od.[Count]) as TotalPrice,
	os.StatusNameRus
from Orders o
inner join
(
	select otch.OrderId, otch.OrderStatus, q.MaxStatusTime 
	from OrdersTimeChanged otch
	inner join
	(
		select max(Time) as MaxStatusTime
		from OrdersTimeChanged
		group by OrderId
	) q on q.MaxStatusTime = otch.Time
) q on q.OrderId = o.OrderId
inner join OrderStatus os on os.OrderStatusId = q.OrderStatus
inner join OrderDetails od on od.OrderDetailsId = o.OrderId
join Product p on p.ProductId = od.ProductId 
where o.OrderId Like @Order+'%'
group by o.OrderId, o.Street, o.House, o.Room, os.StatusNameRus, q.MaxStatusTime
GO
----------------------
CREATE PROC DelOrdersDetailsId @Item nvarchar(50)
AS
DELETE From dbo.OrderDetails
WHERE OrderDetailsId =@Item;
GO
----------------------
CREATE PROC DelOrdersTimeChanged @Item nvarchar(50)
AS
DELETE From dbo.OrdersTimeChanged
WHERE OrderId = @Item;
GO
---------------------------
CREATE PROC InsertValOrdTimeCh @OrdID int, @OrdStatus int
AS
DECLARE @Date datetime
SET @Date= GETDATE()
Insert INTO OrdersTimeChanged 
Values (@OrdID,@OrdStatus, @Date);
GO
----------------------------
CREATE PROC UpdateCategory @CategoryId int, @NewRusName nvarchar(50)
,@NewUkrName nvarchar(50), @NewPriority int 
AS 
Update Category
SET NameRus = @NewRusName , NameUkr = @NewUkrName, [Priority] = @NewPriority
Where CategoryId = @CategoryId
GO


----------------------------

CREATE PROC AddProduct 
@catId int,
@nameRus nvarchar(50),
@nameUkr nvarchar(50),
@numOfOrders int,
@count int,
@energy nvarchar(50),
@balance int,
@price decimal(9,2),
@isSale bit,
@isHided bit
AS
insert into Product
values (@catId, @nameRus, @nameUkr, @numOfOrders, @count, @energy, @balance, @price, @isSale, @isHided,GETDATE());

go