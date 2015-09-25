using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Data.Entity;
using System.Dynamic;
using System.IO.MemoryMappedFiles;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.Ajax.Utilities;
using EmitMapper;


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
        

        public ActionResult Login()
        {  
            return View();
        }
        public async Task<ActionResult> Dishes()                                          //C# Homework Async/Await by R.Kuzmenko
        {                                                                                 //
            var context = new SushiTest1Entities1();                                      //
            var productWeightDetails =  await context.ProductWeightDetails.ToListAsync(); //
            List<AllDishes_Result> addDishes = context.AllDishes().ToList();
            return View(addDishes);
        }

        public async Task<ActionResult> Category()                      //C# Homework Async/Await by R.Kuzmenko
        {      
           
            var context = new SushiTest1Entities1();                    //
            var Product = await context.Products.ToListAsync();       //  
            var Category = await context.Categories.ToListAsync();      //
            var addCategories = context.ShowAllCategories().ToList();
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

        [HttpGet]
        public ActionResult AddNewDish()
        {
            var context = new SushiTest1Entities1();
            var Category = context.Categories.ToList();
            ViewBag.Categories = Category;
            return View();
        }

        [HttpPost]
        public ActionResult AddNewDish(string NameRus, string NameUkr, string isHided, int? dishCategory, byte prod, int Priority, decimal? Price, double Weight, string Energy, int Count, string ingredientsTxt, HttpPostedFileBase uploadPhoto)
        {
            //string fileName = System.IO.Path.GetFileName(uploadPhoto.FileName);
            var context = new SushiTest1Entities1();
            var products = context.Products.ToList();

            bool isHidedProduct = (isHided != null) ? true : false;

            //context.AddProduct(dishCategory, NameRus, NameUkr, 0, Count, Energy, 0, Price, false, isHidedProduct);
            context.SaveChanges();

            int fileName = context.Products.ToList().Last().ProductId;
            
            uploadPhoto.SaveAs(Server.MapPath("~/Content/Images/Products/" + fileName + ".jpeg"));
            return RedirectToAction("Dishes");
        }

        public ActionResult AddNewOrder()
        {
           
            return View();
        }
       
        public ActionResult Orders()
        {
            var context = new SushiTest1Entities1();
            //List<ShowAllCategories_Result> showAllOrders = context.ShowAllCategories().ToList();
            List<ShowAllOrders_Result> showAllOrders = context.ShowAllOrders().ToList();
            return View(showAllOrders);
        }
        [HttpPost]
        public ActionResult FindDishes(Product product)
        {
            List<AllDishes_Result> allD = new List<AllDishes_Result>();

            if (ModelState.IsValid)
            {
                using (var context = new SushiTest1Entities1())
                {
                    List<FindDishes_Result> c = context.FindDishes(product.NameRus).ToList();
                    for (int i = 0; i < c.Count; i++)
                    {
                        allD.Add(new AllDishes_Result());
                    }
                    for (int i = 0; i < c.Count; i++)
                    {
                        allD[i].ProductId = c[i].ProductId;
                        allD[i].NameRus = c[i].NameRus;
                        allD[i].Priority = c[i].Priority;
                        allD[i].Category = c[i].Category;
                        allD[i].Weight = c[i].Weight;
                        allD[i].NameOfWeight = c[i].NameRus;
                        allD[i].Price = c[i].Price;
                        allD[i].TotalDishes = c[i].TotalDishes;
                    }
                }
            }
            return View("~/Views/Admin/Dishes.cshtml", allD);
        }
        [HttpPost]
        public ActionResult FindCategory(Category category)
        {
            List<ShowAllCategories_Result> addCategories = new List<ShowAllCategories_Result>();
            if (ModelState.IsValid)
            {
                using (var context = new SushiTest1Entities1())
                {
                    List<FindCategory_Result> findCategory = context.FindCategory(category.NameRus).ToList();
                    for (int i = 0; i < findCategory.Count; i++)
                    {
                        addCategories.Add(new ShowAllCategories_Result());
                    }
                    for (int i = 0; i < findCategory.Count; i++)
                    {
                        addCategories[i].TotalCategories = findCategory[i].TotalCategories;
                        addCategories[i].NameRus = findCategory[i].NameRus;
                        addCategories[i].CategoryId = findCategory[i].CategoryId;
                        addCategories[i].Priority = findCategory[i].Priority;
                        addCategories[i].TotalDishes = findCategory[i].TotalDishes;
                        //addCategories[i].NameUkr = findCategory[i].NameUkr;
                    }
                }
            }
             return View("~/Views/Admin/Category.cshtml", addCategories);
        }
        [HttpPost]
        public ActionResult FindOrders(Order order)
        {

            List<ShowAllOrders_Result> addOrders = new List<ShowAllOrders_Result>();
            if (ModelState.IsValid)
            {
                using (var contex = new SushiTest1Entities1())
                {
                    string s = order.OrderId.ToString();
                    List<FindOrders_Result> findOrders = contex.FindOrders(s).ToList();
                    for (int i = 0; i < findOrders.Count; i++)
                    {
                        addOrders.Add(new ShowAllOrders_Result());
                    }
                    for (int i = 0; i < findOrders.Count; i++)
                    {
                        addOrders[i].OrderId = findOrders[i].OrderId;
                        addOrders[i].Street = findOrders[i].Street;
                        addOrders[i].House = findOrders[i].Street;
                        addOrders[i].Room = findOrders[i].Room;
                        addOrders[i].MaxStatusTime = findOrders[i].MaxStatusTime;
                        addOrders[i].TotalPrice = findOrders[i].TotalPrice;
                        addOrders[i].StatusNameRus = findOrders[i].StatusNameRus;
                    }
                }
            }
            return View("~/Views/Admin/Orders.cshtml", addOrders);
            
        }
        [HttpPost]
        public ActionResult DeleteCategoryItem(int[] idSelected)
        {
           
                using (var context = new SushiTest1Entities1())
                {
                    if(idSelected !=null)
                    { 
                    for (int i = 0; i < idSelected.Length; i++)
                    {
                        int f = idSelected[i];
                        var d = context.Categories.First(z => z.CategoryId == f);
                        context.Categories.Remove(d);
                            context.SaveChanges();
                        }
                    }

                    context.SaveChanges();
                    return RedirectToAction("Category", "Admin");
                
            }
            
        }

        [HttpPost]
        public JsonResult HideDishModal(int[] idSelected)
        {
            if (ModelState.IsValid)
            {
                using (var context = new SushiTest1Entities1())
                {
                    for (int i = 0; i < idSelected.Length; i++)
                    {
                        int f = idSelected[i];
                        var d = context.Products.First(z => z.ProductId == f);
                        d.IsHided = true;
                    }
                    context.SaveChanges();
                }
                return Json(new { Success = true, Url = Url.Action("Dishes", "Admin") });
            }
            return Json(new { Success = true, Url = Url.Action("Dishes", "Admin") });
        }

        [HttpPost]
        public JsonResult DeleteDishModal(int[] idSelected)
        {
            if (ModelState.IsValid)
            {
                using (var context = new SushiTest1Entities1())
                {
                    for (int i = 0; i < idSelected.Length; i++)
                    {
                        int f = idSelected[i];
                        var d1 = context.ProductWeightDetails.First(z => z.ProductId == f);
                        context.ProductWeightDetails.Remove(d1);
                        context.SaveChanges();
                    }
                    for (int i = 0; i < idSelected.Length; i++)
                    {
                        int f = idSelected[i];
                        var d = context.Products.First(z => z.ProductId == f);
                        context.Products.Remove(d);
                        context.SaveChanges();
                    }
                    return Json(new { Success = true, Url = Url.Action("Dishes", "Admin") });
                }
            }
            return Json(new { Success = true, Url = Url.Action("Dishes", "Admin") });
        }

      
        [HttpPost]
        public ActionResult ModifyCategoryModal(int CategoryId, string NameRus, string NameUkr,int Priority)
        {
            if (ModelState.IsValid)
            {
                using (var context = new SushiTest1Entities1())
                {
                    //var d = context.UpdateCategory(CategoryId,NameRus,NameUkr,Priority); 
                }
            }
            return RedirectToAction("Category", "Admin");

        }
        [HttpPost]
        public ActionResult ChangeOrderStatus(int[] idSelected,int drpdwnVal)
        {
            using (var context = new SushiTest1Entities1())
            {    
                if(idSelected !=null)
                {      
                for (int i = 0; i < idSelected.Length; i++)
                {
                    //context.InsertValOrdTimeCh(idSelected[i], drpdwnVal);
                }
                }
            }
            return View();
        }
        [HttpPost]
        public ActionResult DeleteOrderModal(int[] idSelected)
        {
            if (ModelState.IsValid)
            {
                using (var context = new SushiTest1Entities1())
                {
                    for (int i = 0; i < idSelected.Length; i++)
                    {
                        string f = idSelected[i].ToString();
                        //context.DelOrdersDetailsId(f);  
                    }
                    for (int i = 0; i < idSelected.Length; i++)
                    {
                        string f = idSelected[i].ToString();
                    
                        //context.DelOrdersTimeChanged(f);
                    }

                    for (int i = 0; i < idSelected.Length; i++)
                    {
                        int f = idSelected[i];
                        var d = context.Orders.First(z => z.OrderId == f);
                        context.Orders.Remove(d);
                        context.SaveChanges();
                    }
                }
            }

            return View();
        }

        public  ActionResult Index()
        {
            var context = new SushiTest1Entities1();
            List <ShowUnprocessedOrders_Result> ShUnpOrd = context.ShowUnprocessedOrders().ToList();

            //var showUnprocessedOrders = context.ShowUnprocessedOrders().ToList();
            //var Products = context.Products.ToList();
            //ViewBag.List1 = showUnprocessedOrders;

            //var mostPopularDishes =
            //    (from _Product in Products
            //     where
            //         _Product.CategoryId == 1
            //     orderby
            //         _Product.NumberOfOrders descending
            //     select new
            //     {
            //         _Product.NumberOfOrders,
            //         _Product.ProductId,
            //         _Product.NameRus,
            //         _Product.Price
            //     }.ToExpando()).Take(5);
            //object T2 = mostPopularDishes.ToList();
            //ViewBag.MostPopularDishes = T2;

            return View(ShUnpOrd);

           
        }

    }
}