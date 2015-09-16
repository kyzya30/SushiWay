CREATE PROC ShowAllCategories 
AS
Select
Category.[NameRus],
Category.[CategoryId],

ISNULL(sum(Product.[Count]),0) AS TotalDishes

FROM [SushiTest1].[dbo].[Category]
left join Product on Product.CategoryId = Category.CategoryId 
Group by Category.NameRus,Category.CategoryId
GO
