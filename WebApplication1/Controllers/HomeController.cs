using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.WebSockets;
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

        public JsonResult AddOrderToDb(string [][] data)
        {
            var context = new SushiTest1Entities1();
            var ordersDetails = context.OrderDetails.ToList();           
            Order order = new Order();
            order.Name = data[0][0];
            order.PhoneNumber = data[0][1];
            order.Email = data[0][3];
            order.Street = data[0][4];
            order.House = data[0][5];
            order.Room = data[0][6];

            context.Orders.Add(order);
            context.SaveChanges();

             var nextContext = new SushiTest1Entities1();
            var productList = context.Products.ToList();
            var orders = nextContext.Orders.ToList();
            var lastId = orders[orders.Count - 1].OrderId;
            OrderDetail orderDetail = new OrderDetail();
            for (int i = 0; i < data[1].Length; i++)
            {
                orderDetail.OrderId = lastId; 
                orderDetail.ProductId = Convert.ToInt32(data[1][i]);
                orderDetail.Count = Convert.ToInt32(data[2][i]);
                orderDetail.Price = Convert.ToInt32(data[3][i]);        
                nextContext.OrderDetails.Add(orderDetail);
                nextContext.SaveChanges();
            }

           

            var model = new object();
            return Json(model);
        }

        [HttpPost]
        public JsonResult AddMessageToDB(string[] data)
        {
            Massage mess = new Massage();
            mess.Name = data[0];
            mess.Email = data[1];
            mess.Text = data[2];

            var context = new SushiTest1Entities1(); ;
            context.Massages.Add(mess);
            context.SaveChanges();

            return Json(new { res = "good" });
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