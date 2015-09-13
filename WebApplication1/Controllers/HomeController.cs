using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Addition_Classes;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        public async Task<ActionResult> Index()
        {
            var context = new SushiTest1Entities1();
            var product = context.Products.ToList();
            ViewBag.Product = product;

            using (HttpClient httpClient = new HttpClient())                   //
            {                                                                  //
                var response = await httpClient.GetAsync("http://google.com"); //  This is C# homework (async await).
            }                                                                  //
            return View();
        }

        public async Task<ActionResult> MarkTask()
        {
            using (HttpClient httpClient = new HttpClient())                   //
            {                                                                  //
                var response = await httpClient.GetAsync("http://google.com"); //  This is C# homework (async await).
                ViewBag.Res = response.Content.Headers.ToString();
            }
           var d =  await Pause.Pauses();
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

            var r = qq.First();

            ViewBag.Category = r;
            ViewBag.Product = res;

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