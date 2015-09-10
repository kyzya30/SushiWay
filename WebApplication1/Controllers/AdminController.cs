using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;

namespace WebApplication1.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        
       
        public ActionResult Dishes()
        {
            return View();
        }
        public ActionResult Orders()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }
        public ActionResult Category()
        {
            var context = new SushiTest1Entities1();
            var Product = context.Products.ToList();
            var Category = context.Categories.ToList();

            var allCategories =

                from _Category in Category
                join _Product in Product on _Category.CategoryId equals _Product.CategoryId
                group new {_Category, _Product} by new
                {
                    _Category.NameRus,
                    _Category.CategoryId
                }
                into g
                select new
                {
                    g.Key.NameRus,
                    CategoryId = (System.Int32?) g.Key.CategoryId,
                    TotalDishes = (System.Int32?) g.Sum(p => p._Product.Count)
                };
            var D = allCategories.ToList();
            ViewBag.AllCategories = D;

            var totalCategories =
                from _Category in Category
                select Category.Count;
            var totalCat = totalCategories.ToList();
            ViewBag.TotalCategories = totalCat;
            return View();
        }
        public ActionResult Index()
        {
            var context = new SushiTest1Entities1();
            //ViewBag.Product = context.Products;
            var OrderDetails = context.OrderDetails.ToList();
            var Orders = context.Orders.ToList();
            var Product = context.Products.ToList();


            var unprocessedOrders = from _OrderDetails in OrderDetails
            join _Orders in Orders on new{OrderId = _OrderDetails.OrderDetailsId} equals
                new{OrderId = _Orders.OrderId}
            where
                _Orders.StatusId == 2
            group new{_Orders, _OrderDetails} by new{
           
                _Orders.AddDate,
                _Orders.Street,
                _Orders.House,
                _Orders.Room,
                _Orders.OrderId
            }
            into g
            select new{
            
                OrderId = (System.Int32?)g.Key.OrderId,
                AddDate = (System.DateTime?)g.Key.AddDate,
                g.Key.Street,
                g.Key.House,
                g.Key.Room,
                TotalPrice = (System.Decimal?)g.Sum(p => p._OrderDetails.Price)
            };
           
            object T = unprocessedOrders.ToList();
            ViewBag.List1 = T;
            //dynamic model = new ExpandoObject();

            var mostPopularDishes =
                (from _Product in Product
                    where
                        _Product.CategoryId == 1
                    orderby
                        _Product.NamberOfOrders descending
                    select new
                    {
                        _Product.NamberOfOrders,
                        _Product.ProductId,
                        _Product.NameRus,
                        _Product.Price
                    }).Take(5);
            object T2 = mostPopularDishes.ToList();
            ViewBag.MostPopularDishes = T2;

            return View();

           
        }

    }
}