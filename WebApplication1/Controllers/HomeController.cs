using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;
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

        [HttpPost]
        public JsonResult AddMessageToDB(string id)
        {
        
            return Json(new {res = "good"});
        }
        public JsonResult GetOrderStatus(string id)
        {
            int find = Convert.ToInt32(id);
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

       


       

        
    }
}