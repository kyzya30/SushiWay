using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Dynamic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Controllers
{
    public static class ExtensionClass
    {
        public static ExpandoObject ToExpando(this object anonymousObject)
        {
            IDictionary<string, object> anonymousDictionary = HtmlHelper.AnonymousObjectToHtmlAttributes(anonymousObject);

            IDictionary<string, object> expando = new ExpandoObject();

            foreach (var item in anonymousDictionary)
            {
                expando.Add(item);
            }
            return (ExpandoObject)expando;
        }
    }
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Orders()
        {
            return View();
        }

        public ActionResult Login()
        {  
            return View();
        }
        public async Task<ActionResult> Dishes()                                          //C# Homework Async/Await by R.Kuzmenko
        {                                                                                 //
            var context = new SushiTest1Entities1();                                      //
            var ProductWeightDetails =  await context.ProductWeightDetails.ToListAsync(); //
          

            var allDishesQuery =
                from _ProductWeightDetails in ProductWeightDetails
                select new
                {
                    ProductId = (System.Int32?)_ProductWeightDetails.Product.ProductId,
                    _ProductWeightDetails.Product.NameRus,
                    Priority = (System.Int32?)_ProductWeightDetails.Product.CategoryId,
                    Category = _ProductWeightDetails.Product.Category.NameRus,
                    Weight = _ProductWeightDetails.Value,
                    _ProductWeightDetails.Name,
                    Price = (System.Decimal?)_ProductWeightDetails.Product.Price
                }.ToExpando();
            var allDishes = allDishesQuery;
            ViewBag.AllDishes = allDishes;
            return View();
        }
        public async Task<ActionResult> Category()                      //C# Homework Async/Await by R.Kuzmenko
        {                                                               //
            var context = new SushiTest1Entities1();                    //
            var product = await context.Products.ToListAsync();         //  
            var category = await context.Categories.ToListAsync();      //
           // var category = context.Categories;

            var allCategories =

                from _Category in category
                join _Product in product on _Category.CategoryId equals _Product.CategoryId
                group new { _Category, _Product } by new
                {
                    _Category.NameRus,
                    _Category.CategoryId
                }
                into g
                select new
                {
                    g.Key.NameRus,
                    CategoryId = (System.Int32?)g.Key.CategoryId,
                    TotalDishes = (System.Int32?)g.Sum(p => p._Product.Count)
                }.ToExpando();
            var D = allCategories.ToList();
            ViewBag.AllCategories = D;

            var totalCategories =
                from _Category in category
                select category.Count;
           var totalCat = totalCategories.ToList();
            ViewBag.TotalCategories = totalCat;
            return View();
        }
        public  ActionResult Index()
        {
            var context = new SushiTest1Entities1();
            ViewBag.Product = context.Products.Count();
            var OrderDetails = context.OrderDetails.ToList();
            var Orders = context.Orders.ToList();
            var Product = context.Products.ToList();


            var unprocessedOrders = from _OrderDetails in OrderDetails
                                    join _Orders in Orders on new { OrderId = _OrderDetails.OrderDetailsId } equals
                                        new { OrderId = _Orders.OrderId }
                                    where
                                        _Orders.StatusId == 2
                                    group new { _Orders, _OrderDetails } by new
                                    {

                                       // _Orders.Add,
                                        _Orders.Street,
                                        _Orders.House,
                                        _Orders.Room,
                                        _Orders.OrderId
                                    }
            into g
                                    select new
                                    {

                                        OrderId = (System.Int32?)g.Key.OrderId,
                                        //AddDate = (System.DateTime?)g.Key.AddDate,
                                        g.Key.Street,
                                        g.Key.House,
                                        g.Key.Room,
                                        TotalPrice = (System.Decimal?)g.Sum(p => p._OrderDetails.Price)
                                    }.ToExpando();

            object T = unprocessedOrders;
            ViewBag.List1 = T;



            dynamic model = new ExpandoObject();

            var mostPopularDishes =
                (from _Product in Product
                 where
                     _Product.CategoryId == 1
                 orderby
                     _Product.NumberOfOrders descending
                 select new
                 {
                     _Product.NumberOfOrders,
                     _Product.ProductId,
                     _Product.NameRus,
                     _Product.Price
                 }.ToExpando()).Take(5);
            object T2 = mostPopularDishes.ToList();
            ViewBag.MostPopularDishes = T2;

            return View();

           
        }

    }
}