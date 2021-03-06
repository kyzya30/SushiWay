﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.WebSockets;
using Topics.Radical;
using WebApplication1.Addition_Classes;
using WebApplication1.Models;


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
            var productsList = context.Products.ToList();
            var productQuery = from product in productsList
                               where product.IsHided == false
                               orderby product.Priority descending
                               select product;

            var products = productQuery.ToList();

            var model = new object[products.Count];
            var topQuery = from product in products
                           orderby product.NumberOfOrders descending
                           select product.NumberOfOrders;

            var topList = topQuery.ToList();

            int topNumber = TopProduct(topList);


            for (int i = 0; i < products.Count; i++)
            {
                if (!products[i].IsHided)
                {
                    model[i] = new
                    {
                        id = products[i].ProductId,
                        categoryId = products[i].CategoryId,
                        name = products[i].NameRus,
                        price = products[i].Price,
                        ingridients = products[i].IngridientsRus,
                        kkal = products[i].Energy,
                        sale = products[i].Sale,
                        top = products[i].Sale != true && products[i].NumberOfOrders >= topNumber,
                        hot = (products[i].AddDate >= DateTime.Today) && (products[i].Sale != true) && (products[i].NumberOfOrders < topNumber),
                        count = products[i].Count,
                        energy = products[i].Energy,
                    };
                }

            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetCategory()
        {
            var context = new SushiTest1Entities1();
            var categoryList = context.Categories.ToList();
            var query = from cat in categoryList
                        orderby cat.Priority descending
                        select cat;


            var category = query.ToList();

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

        public JsonResult AddOrderToDb(string[][] data)
        {
            var context = new SushiTest1Entities1();
            var ordersDetails = context.OrderDetails.ToList();
            Order order = new Order();
            order.Name = data[0][0] ?? "no data";
            order.PhoneNumber = data[0][1];
            order.Email = data[0][2] ?? "no data";
            order.Street = data[0][3];
            order.House = data[0][4];
            order.Room = data[0][5];

            //context.Orders.Add(order);
            context.AddOrder(order.Name, order.PhoneNumber, order.Email, order.Street, order.House, order.Room);


            context.SaveChanges();

            var nextContext = new SushiTest1Entities1();
            var statusList = nextContext.OrderStatus.ToList();
            var orders = nextContext.Orders.ToList();
            var lastId = orders[orders.Count - 1].OrderId;

            var myStatus = statusList[4];
            var myOrder = orders[orders.Count - 1];

            var lastStatus = new OrdersTimeChanged();

            lastStatus.OrderId = lastId;
            lastStatus.OrderStatus = 5;
            lastStatus.Time = DateTime.Now;

            //nextContext.OrdersTimeChangeds.Add(lastStatus);
            nextContext.AddOrderTimeChanged(lastStatus.OrderId,
                lastStatus.OrderStatus, lastStatus.Time);

            var productList = nextContext.Products.ToList();
            var resList = new List<Product>();

            var productforUpd = new List<ProductForUpdate>();
            for (int i = 0; i < data[1].Length; i++)
            {
                var prod = new ProductForUpdate();
                prod.Id = Convert.ToInt32(data[1][i]);
                prod.count = Convert.ToInt32(data[2][i]);
                productforUpd.Add(prod);
            }

            foreach (var q in productforUpd)
            {
                foreach (var w in productList)
                {
                    if (q.Id == w.ProductId)
                    {
                        nextContext.AddOrdersTimeProduct(q.count + w.NumberOfOrders, q.Id);
                        nextContext.SaveChanges();
                    }
                }
            }


            nextContext.SaveChanges();

            OrderDetail orderDetail = new OrderDetail();

            for (int i = 0; i < data[1].Length; i++)
            {
                orderDetail.OrderId = lastId;
                orderDetail.ProductId = Convert.ToInt32(data[1][i]);
                orderDetail.Count = Convert.ToInt32(data[2][i]);
                orderDetail.Price = Convert.ToDecimal(data[3][i]);
                //nextContext.OrderDetails.Add(orderDetail);
                nextContext.AddOrderDetails(orderDetail.OrderId, orderDetail.ProductId, orderDetail.Count,
                    orderDetail.Price);
                nextContext.SaveChanges();
            }



            var model = new object[1];
            model[0] = new { res = lastId };
            return Json(model);
        }

        [HttpPost]
        public JsonResult AddMessageToDB(string[] data)
        {
            Message mess = new Message();
            mess.Name = data[0];
            mess.Email = data[1];
            mess.Text = data[2];

            var context = new SushiTest1Entities1();
            context.Messages.Add(mess);
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
                model[0] = new { res = "Заказ не найден" };
            }

            return Json(model, JsonRequestBehavior.AllowGet);
        }

        private int TopProduct(List<int> list)
        {
            if (list.Count < 5)
            {
                return int.MaxValue;
            }
            else
            {
                return list[4];
            }
        }
    }
}