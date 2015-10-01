﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebApplication1
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
    public partial class SushiTest1Entities1 : DbContext
    {
        public SushiTest1Entities1()
            : base("name=SushiTest1Entities1")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Massage> Massages { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderStatu> OrderStatus { get; set; }
        public virtual DbSet<OrdersTimeChanged> OrdersTimeChangeds { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductWeightDetail> ProductWeightDetails { get; set; }
        public virtual DbSet<Administrator> Administrators { get; set; }
    
        public virtual int AddProduct(Nullable<int> catId, string nameRus, string nameUkr, Nullable<int> numOfOrders, Nullable<int> count, string energy, Nullable<int> balance, Nullable<decimal> price, Nullable<bool> isSale, Nullable<bool> isHided, Nullable<int> priority, string rusIngr, string ukrIngr)
        {
            var catIdParameter = catId.HasValue ?
                new ObjectParameter("catId", catId) :
                new ObjectParameter("catId", typeof(int));
    
            var nameRusParameter = nameRus != null ?
                new ObjectParameter("nameRus", nameRus) :
                new ObjectParameter("nameRus", typeof(string));
    
            var nameUkrParameter = nameUkr != null ?
                new ObjectParameter("nameUkr", nameUkr) :
                new ObjectParameter("nameUkr", typeof(string));
    
            var numOfOrdersParameter = numOfOrders.HasValue ?
                new ObjectParameter("numOfOrders", numOfOrders) :
                new ObjectParameter("numOfOrders", typeof(int));
    
            var countParameter = count.HasValue ?
                new ObjectParameter("count", count) :
                new ObjectParameter("count", typeof(int));
    
            var energyParameter = energy != null ?
                new ObjectParameter("energy", energy) :
                new ObjectParameter("energy", typeof(string));
    
            var balanceParameter = balance.HasValue ?
                new ObjectParameter("balance", balance) :
                new ObjectParameter("balance", typeof(int));
    
            var priceParameter = price.HasValue ?
                new ObjectParameter("price", price) :
                new ObjectParameter("price", typeof(decimal));
    
            var isSaleParameter = isSale.HasValue ?
                new ObjectParameter("isSale", isSale) :
                new ObjectParameter("isSale", typeof(bool));
    
            var isHidedParameter = isHided.HasValue ?
                new ObjectParameter("isHided", isHided) :
                new ObjectParameter("isHided", typeof(bool));
    
            var priorityParameter = priority.HasValue ?
                new ObjectParameter("Priority", priority) :
                new ObjectParameter("Priority", typeof(int));
    
            var rusIngrParameter = rusIngr != null ?
                new ObjectParameter("RusIngr", rusIngr) :
                new ObjectParameter("RusIngr", typeof(string));
    
            var ukrIngrParameter = ukrIngr != null ?
                new ObjectParameter("UkrIngr", ukrIngr) :
                new ObjectParameter("UkrIngr", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("AddProduct", catIdParameter, nameRusParameter, nameUkrParameter, numOfOrdersParameter, countParameter, energyParameter, balanceParameter, priceParameter, isSaleParameter, isHidedParameter, priorityParameter, rusIngrParameter, ukrIngrParameter);
        }
    
        public virtual ObjectResult<AllDishes_Result> AllDishes()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<AllDishes_Result>("AllDishes");
        }
    
        public virtual int DeleteCategory(Nullable<int> item)
        {
            var itemParameter = item.HasValue ?
                new ObjectParameter("item", item) :
                new ObjectParameter("item", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("DeleteCategory", itemParameter);
        }
    
        public virtual int DeleteDish(Nullable<int> item)
        {
            var itemParameter = item.HasValue ?
                new ObjectParameter("item", item) :
                new ObjectParameter("item", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("DeleteDish", itemParameter);
        }
    
        public virtual int DeleteOrder(Nullable<int> item)
        {
            var itemParameter = item.HasValue ?
                new ObjectParameter("item", item) :
                new ObjectParameter("item", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("DeleteOrder", itemParameter);
        }
    
        public virtual int DeleteProductWeightDetails(Nullable<int> item)
        {
            var itemParameter = item.HasValue ?
                new ObjectParameter("item", item) :
                new ObjectParameter("item", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("DeleteProductWeightDetails", itemParameter);
        }
    
        public virtual int DelOrdersDetailsId(string item)
        {
            var itemParameter = item != null ?
                new ObjectParameter("Item", item) :
                new ObjectParameter("Item", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("DelOrdersDetailsId", itemParameter);
        }
    
        public virtual int DelOrdersTimeChanged(string item)
        {
            var itemParameter = item != null ?
                new ObjectParameter("Item", item) :
                new ObjectParameter("Item", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("DelOrdersTimeChanged", itemParameter);
        }
    
        public virtual ObjectResult<FindCategory_Result> FindCategory(string categoryName)
        {
            var categoryNameParameter = categoryName != null ?
                new ObjectParameter("CategoryName", categoryName) :
                new ObjectParameter("CategoryName", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<FindCategory_Result>("FindCategory", categoryNameParameter);
        }
    
        public virtual ObjectResult<FindDishById_Result> FindDishById(Nullable<int> id)
        {
            var idParameter = id.HasValue ?
                new ObjectParameter("id", id) :
                new ObjectParameter("id", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<FindDishById_Result>("FindDishById", idParameter);
        }
    
        public virtual ObjectResult<FindDishes_Result> FindDishes(string dishName)
        {
            var dishNameParameter = dishName != null ?
                new ObjectParameter("DishName", dishName) :
                new ObjectParameter("DishName", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<FindDishes_Result>("FindDishes", dishNameParameter);
        }
    
        public virtual ObjectResult<FindOrders_Result> FindOrders(string order)
        {
            var orderParameter = order != null ?
                new ObjectParameter("Order", order) :
                new ObjectParameter("Order", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<FindOrders_Result>("FindOrders", orderParameter);
        }
    
        public virtual int HideDish(Nullable<int> item)
        {
            var itemParameter = item.HasValue ?
                new ObjectParameter("item", item) :
                new ObjectParameter("item", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("HideDish", itemParameter);
        }
    
        public virtual int InsertValOrdTimeCh(Nullable<int> ordID, Nullable<int> ordStatus)
        {
            var ordIDParameter = ordID.HasValue ?
                new ObjectParameter("OrdID", ordID) :
                new ObjectParameter("OrdID", typeof(int));
    
            var ordStatusParameter = ordStatus.HasValue ?
                new ObjectParameter("OrdStatus", ordStatus) :
                new ObjectParameter("OrdStatus", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("InsertValOrdTimeCh", ordIDParameter, ordStatusParameter);
        }
    
        public virtual ObjectResult<MostPopularDishes_Result> MostPopularDishes(Nullable<int> topVal, Nullable<int> catId)
        {
            var topValParameter = topVal.HasValue ?
                new ObjectParameter("topVal", topVal) :
                new ObjectParameter("topVal", typeof(int));
    
            var catIdParameter = catId.HasValue ?
                new ObjectParameter("catId", catId) :
                new ObjectParameter("catId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<MostPopularDishes_Result>("MostPopularDishes", topValParameter, catIdParameter);
        }
    
        public virtual ObjectResult<SelectOrderContactInfo_Result> SelectOrderContactInfo(Nullable<int> orderId)
        {
            var orderIdParameter = orderId.HasValue ?
                new ObjectParameter("OrderId", orderId) :
                new ObjectParameter("OrderId", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<SelectOrderContactInfo_Result>("SelectOrderContactInfo", orderIdParameter);
        }
    
        public virtual ObjectResult<ShowAllCategories_Result> ShowAllCategories()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<ShowAllCategories_Result>("ShowAllCategories");
        }
    
        public virtual ObjectResult<ShowAllOrders_Result> ShowAllOrders()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<ShowAllOrders_Result>("ShowAllOrders");
        }
    
        public virtual ObjectResult<ShowUnprocessedOrders_Result> ShowUnprocessedOrders()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<ShowUnprocessedOrders_Result>("ShowUnprocessedOrders");
        }
    
        public virtual int UpdateCategory(Nullable<int> categoryId, string newRusName, string newUkrName, Nullable<int> newPriority)
        {
            var categoryIdParameter = categoryId.HasValue ?
                new ObjectParameter("CategoryId", categoryId) :
                new ObjectParameter("CategoryId", typeof(int));
    
            var newRusNameParameter = newRusName != null ?
                new ObjectParameter("NewRusName", newRusName) :
                new ObjectParameter("NewRusName", typeof(string));
    
            var newUkrNameParameter = newUkrName != null ?
                new ObjectParameter("NewUkrName", newUkrName) :
                new ObjectParameter("NewUkrName", typeof(string));
    
            var newPriorityParameter = newPriority.HasValue ?
                new ObjectParameter("NewPriority", newPriority) :
                new ObjectParameter("NewPriority", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("UpdateCategory", categoryIdParameter, newRusNameParameter, newUkrNameParameter, newPriorityParameter);
        }
    
        public virtual int UpdateProduct(Nullable<int> productId, Nullable<int> categoryId, string nameRus, Nullable<decimal> price, string nameUkr, Nullable<int> count, Nullable<int> energy, Nullable<bool> sale, Nullable<bool> isHided, Nullable<int> priority, string ingrRus, string ingrUkr)
        {
            var productIdParameter = productId.HasValue ?
                new ObjectParameter("ProductId", productId) :
                new ObjectParameter("ProductId", typeof(int));
    
            var categoryIdParameter = categoryId.HasValue ?
                new ObjectParameter("CategoryId", categoryId) :
                new ObjectParameter("CategoryId", typeof(int));
    
            var nameRusParameter = nameRus != null ?
                new ObjectParameter("NameRus", nameRus) :
                new ObjectParameter("NameRus", typeof(string));
    
            var priceParameter = price.HasValue ?
                new ObjectParameter("Price", price) :
                new ObjectParameter("Price", typeof(decimal));
    
            var nameUkrParameter = nameUkr != null ?
                new ObjectParameter("NameUkr", nameUkr) :
                new ObjectParameter("NameUkr", typeof(string));
    
            var countParameter = count.HasValue ?
                new ObjectParameter("Count", count) :
                new ObjectParameter("Count", typeof(int));
    
            var energyParameter = energy.HasValue ?
                new ObjectParameter("Energy", energy) :
                new ObjectParameter("Energy", typeof(int));
    
            var saleParameter = sale.HasValue ?
                new ObjectParameter("Sale", sale) :
                new ObjectParameter("Sale", typeof(bool));
    
            var isHidedParameter = isHided.HasValue ?
                new ObjectParameter("IsHided", isHided) :
                new ObjectParameter("IsHided", typeof(bool));
    
            var priorityParameter = priority.HasValue ?
                new ObjectParameter("Priority", priority) :
                new ObjectParameter("Priority", typeof(int));
    
            var ingrRusParameter = ingrRus != null ?
                new ObjectParameter("IngrRus", ingrRus) :
                new ObjectParameter("IngrRus", typeof(string));
    
            var ingrUkrParameter = ingrUkr != null ?
                new ObjectParameter("IngrUkr", ingrUkr) :
                new ObjectParameter("IngrUkr", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("UpdateProduct", productIdParameter, categoryIdParameter, nameRusParameter, priceParameter, nameUkrParameter, countParameter, energyParameter, saleParameter, isHidedParameter, priorityParameter, ingrRusParameter, ingrUkrParameter);
        }
    
        public virtual int UpdateProductWeightDetails(Nullable<int> productId, string name, Nullable<decimal> value)
        {
            var productIdParameter = productId.HasValue ?
                new ObjectParameter("ProductId", productId) :
                new ObjectParameter("ProductId", typeof(int));
    
            var nameParameter = name != null ?
                new ObjectParameter("Name", name) :
                new ObjectParameter("Name", typeof(string));
    
            var valueParameter = value.HasValue ?
                new ObjectParameter("Value", value) :
                new ObjectParameter("Value", typeof(decimal));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("UpdateProductWeightDetails", productIdParameter, nameParameter, valueParameter);
        }
    }
}
