using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ZapatukiFinal.Controllers
{
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
        

        //[HttpGet]
        //public ActionResult Logout()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public ActionResult logout()
        //{
        //    Session["Userlogged"] = null;
        //    Session.Abandon();
        //    return Redirect ("Index, Home");
        //}
    }
}