using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var context = new SushiTest1Entities1();
            var product = context.Products.ToList();
            ViewBag.Product = product;
            return View();
        }

        public ActionResult SortByCategory(int? id)
        {
            var context = new SushiTest1Entities1();
            var products = context.Products.ToList();
            var query = from pr in products
                where pr.CategoryId == id
                select pr;
            
            var res = query.ToList();

            var category = context.Categories.ToList();
            var qq = from ct in category
                where ct.CategoryId == id
                select ct.NameRus;

            ViewBag.Product = res;
            var  r= qq.First();
            ViewBag.Category = r;
            return View();
        }

        public ActionResult Cart()
        {

            return View();
        }

        public JsonResult AddToCart(int i)
        {
            
            return Json(new object());
        }

        public ActionResult OrderStatusSearch()
        {
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}