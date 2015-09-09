using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;

namespace WebApplication1.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        
        public ActionResult Category() 
        {
            
            
            return View();
        }
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
        public ActionResult Index()
        {
            
            return View();
        }

    }
}