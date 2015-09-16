using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Dynamic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;

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
            var productWeightDetails =  await context.ProductWeightDetails.ToListAsync(); //
          

            var allDishesQuery =
                from _ProductWeightDetails in productWeightDetails
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
            var Product = await context.Products.ToListAsync();       //  
            var Category = await context.Categories.ToListAsync();      //
           // var category = context.Categories;
            var d = context.ShowAllCategories().ToString();

            var allCategories =

                from _Category in Category
                join _Product in Product on _Category.CategoryId equals _Product.CategoryId into Product_join
                from _Product in Product_join.DefaultIfEmpty()
                group Category by new
                {
                    _Category.NameRus,
                    _Category.CategoryId
                } into g
                select new
                {
                    g.Key.NameRus,
                    CategoryId = (System.Int32?)g.Key.CategoryId,
                    TotalDishes = ((System.Int32?)g.Sum(p => p.Count) ?? (System.Int32?)0)
                

                }.ToExpando();

            object D = allCategories.ToList();
            ViewBag.AllCategories = D;

            var totalCategories =
                (
                from _Category in Category
                select Category.Count);
           var totalCat = totalCategories.ToList();
            ViewBag.TotalCategories = totalCat;

           

            return View();
        }

        [HttpPost]
        public ActionResult AddCategory(Category category)
        {
            if (ModelState.IsValid)
            {
                using (var context = new SushiTest1Entities1())
                {
                    category.NameUkr = "ukrxxxName"; //exception avoid
                    context.Categories.Add(category);
                    context.SaveChanges();
                    return RedirectToAction("Category", "Admin");
                }
            }
            return RedirectToAction("Category", "Admin");
        }

        public  ActionResult Index()
        {
            var context = new SushiTest1Entities1();
            var OrderDetails = context.OrderDetails.ToList();
            var Orders = context.Orders.ToList();
            var Products = context.Products.ToList();
            var OrdersTimeChangeds = context.OrdersTimeChangeds;

            var OrdersTimeChanged = context.OrdersTimeChangeds;


            var unprocessedOrders = from _OrderDetails in OrderDetails
                                    from _OrdersTimeChanged in OrdersTimeChanged
                                    where
                                      _OrdersTimeChanged.Order.StatusId == 2
                                    group new { _OrdersTimeChanged.Order, _OrderDetails.Product, OrderDetails } by new
                                    {

                                        OrderId = (System.Int32?)_OrdersTimeChanged.Order.OrderId,
                                        Time = _OrdersTimeChanged.Time
                                    } into g
                                    select new
                                    {
                                        _OrderId = (System.Int32?)g.Key.OrderId,
                                       // TotalPrice = (System.Decimal?)g.Sum(p => p.OrderDetails.Product.Price * p.OrderDetails.Count),
                                         TotalPrice = (System.Decimal?)g.Sum(p => p.OrderDetails.Sum(product => product.Price * product.Count)),
                                         //TotalPrice = g.Sum(OrderDetails.Select(product => product.Price * product.Count)),
                                        Time = (System.DateTime?)g.Key.Time
                                    }.ToExpando();

            object T = unprocessedOrders;
            ViewBag.List1 = T;





            var mostPopularDishes =
                (from _Product in Products
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