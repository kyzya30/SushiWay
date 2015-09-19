using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Dynamic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
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


           
            ViewBag.AllDishes = context.AllDishes().ToList();
            return View();
        }

       

        public async Task<ActionResult> Category()                      //C# Homework Async/Await by R.Kuzmenko
        {      
           
            var context = new SushiTest1Entities1();                    //
            var Product = await context.Products.ToListAsync();       //  
            var Category = await context.Categories.ToListAsync();      //
           // var category = context.Categories;
            //var addCategories = context.ShowAllCategories().ToList();

            List<ShowAllCategories_Result> addCategories = context.ShowAllCategories().ToList();
           // ViewBag.AllCategories = addCategories;

           // var totalCategories =
           //     (
           //     from _Category in Category
           //     select Category.Count);
           //var totalCat = totalCategories.ToList();
           // ViewBag.TotalCategories = totalCat;

            return View(addCategories);
        }

        [HttpPost]
        public ActionResult AddCategory(Category category)
        {
            if (ModelState.IsValid)
            {
                using (var context = new SushiTest1Entities1())
                {
                   
                    context.Categories.Add(category);
                    context.SaveChanges();
                    return RedirectToAction("Category", "Admin");
                }
            }
            return RedirectToAction("Category", "Admin");
        }

        public ActionResult AddNewDish()
        {
            
            return View();
        }


        [HttpPost]
        public ActionResult FindDishes(Product product)
        {
            if (ModelState.IsValid)
            {
                using (var context = new SushiTest1Entities1())
                {
                    //var c = context.FindCategory(category.NameRus).ToList();
                    //ViewBag.AllCategories = c;
                    var c = context.FindDishes(product.NameRus).ToList();
                    ViewBag.AllDishes = c;

                }
            }
            return View("~/Views/Admin/Dishes.cshtml", ViewBag.AllDishes);
        }
        [HttpPost]
        public ActionResult FindCategory(Category category)
        {
            List<FindCategory_Result> c;
            if (ModelState.IsValid)
            {
                using (var context = new SushiTest1Entities1())
                {
                     c = context.FindCategory(category.NameRus).ToList();
                    List<FindCategory_Result> findCategory =  c.ToList();
                    List<ShowAllCategories_Result> addCategories = new List<ShowAllCategories_Result>(); //return res
                    //findCategory[0]. = addCategories[0].

                    for (int i = 0; i < findCategory.Count; i++)
                    {
                        addCategories[i].TotalCategories = findCategory[i].TotalCategories;
                        addCategories[i].NameRus = findCategory[i].NameRus;
                        addCategories[i].CategoryId = findCategory[i].CategoryId;
                        addCategories[i].Priority = findCategory[i].Priority;
                        addCategories[i].TotalDishes = findCategory[i].TotalDishes;
                    }
                    
                    return View("~/Views/Admin/Category.cshtml", addCategories);
                    //ViewBag.TotalCategories = category.NameRus.Length.ToString();
                }

            }
            return View();
            //return View(Url.RouteUrl(Category().Result));
            //return RedirectToAction("FindCategory", "Admin");
            // return Redirect("/Admin/Category");
            //return RedirectToAction("Category", "Admin");
            // return View("~/Views/Admin/Category.cshtml", ViewBag.AllCategories);
            //return View(findCategory);

        }
        [HttpPost]
        public ActionResult ModifyCategoryModal(Category category)
        {
            if (ModelState.IsValid)
            {
                using (var context = new SushiTest1Entities1())
                {
                    var d = context.Categories.First(i => i.NameRus == category.NameRus);
                    d.Priority = category.Priority;
                    context.SaveChanges();
                  
                }
            }
            return RedirectToAction("Category", "Admin");
           // return View();
        }

        public  ActionResult Index()
        {
            var context = new SushiTest1Entities1();
            var showUnprocessedOrders = context.ShowUnprocessedOrders().ToList();
            
            var Products = context.Products.ToList();
            ViewBag.List1 = showUnprocessedOrders;





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