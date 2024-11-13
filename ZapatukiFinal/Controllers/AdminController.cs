using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZapatukiFinal.Dtos.ViewModels;
using ZapatukiFinal.Dtos;

namespace ZapatukiFinal.Controllers
{
    public class AdminController : Controller
    {

        [HttpGet]
        public ActionResult Administrator()
        {
            var userLogged = Session["UserLogged"] as ResponseDto;

            if (userLogged == null || userLogged.Data == null)
            {
                return RedirectToAction("Login", "Login");
            }

            var viewModel = new UserViewModel
            {
                User = userLogged.Data,
                Response = new ResponseDto(),
            };
            return View(viewModel);
        }

    }
}

