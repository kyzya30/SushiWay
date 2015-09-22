using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using WebApplication1.Addition_Classes;


namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }


        public JsonResult GetSushi()
        {
            var context = new SushiTest1Entities1();
            var product = context.Products.ToList();
            var model = new object[product.Count];

            for (int i = 0; i < product.Count; i++)
            {
                model[i] = new
                {
                    id = product[i].ProductId,
                    categoryId = product[i].CategoryId,
                    name = product[i].NameRus,
                    price = product[i].Price
                };
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCategory()
        {
            var context = new SushiTest1Entities1();
            var category = context.Categories.ToList();
            var model = new object[category.Count];

            for (int i = 0; i < category.Count; i++)
            {
                model[i] = new
                {
                    id = category[i].CategoryId,
                    name = category[i].NameRus
                };
            }

            //var model = new JavaScriptSerializer().Serialize(category);

            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public JsonResult AddOrderToDb(FormCollection id)
        {

            var model = new object();

            return Json(model);
        }

        public JsonResult GetOrderStatus(int find)
        {
            var context = new SushiTest1Entities1();
            var orders = context.Orders.ToList();
            var status = context.OrderStatus.ToList();
            var lastStatus = context.OrdersTimeChangeds.ToList();
            var model = new object[1];

            bool isExsist = orders.Any(q => find == q.OrderId);

            if (isExsist)
            {
                var query = (from st in lastStatus
                             where st.OrderId == find
                             orderby st.Time descending
                             select st).Take(1);
                var d = query.ToList().First();

                foreach (var e in status)
                {
                    if (e.OrderStatusId == d.OrderStatus)
                    {
                        model[0] = new { res = e.StatusNameRus };
                        break;
                    }
                }
            }
            else
            {
                model[0] = new { res = "Заказ не найдет" };
            }

            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public async Task<ActionResult> MarkTask()
        {
            using (HttpClient httpClient = new HttpClient())                   //
            {                                                                  //
                var response = await httpClient.GetAsync("http://google.com"); //  This is C# homework (async await).
                ViewBag.Res = response.Content.Headers.ToString();
            }
            var d = await Pause.Pauses();
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

        public ActionResult CategoryMenu()
        {
            var context = new SushiTest1Entities1();
            var categoty = context.Categories.ToList();

            ViewBag.Cat = categoty;

            return PartialView();
        }

        [HttpPost]
        public ActionResult SearchOrderStatusByNumber(int? orderNum)
        {
            var context = new SushiTest1Entities1();
            var status = context.OrderStatus.ToList();
            ViewBag.OrderNumber = orderNum;
            ViewBag.Status = status[0].StatusNameRus;

            return View();
        }
    }
}