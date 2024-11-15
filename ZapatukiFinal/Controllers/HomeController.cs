using Rotativa;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZapatukiFinal.Dtos;

namespace ZapatukiFinal.Controllers
{
    [HandleError]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Offers()
        {
            return View();
        }
        public ActionResult Categories()
        {
            return View();
        }
        public ActionResult Accesories()
        {
            return View();
        }
        public ActionResult Lastest()
        {
            return View();
        }
        public ActionResult Cart()
        {
            return View();
        }

        public ActionResult Suppliers()
        {
            return View();
        }  
    }
}