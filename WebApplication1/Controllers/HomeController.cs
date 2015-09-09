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

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}