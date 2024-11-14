using System;
using System.Linq;
using System.Web.Mvc;
using ZapatukiFinal.Dtos.ViewModels;
using ZapatukiFinal.Dtos;
using ZapatukiFinal.Services;

namespace ZapatukiFinal.Controllers
{
    public class SellerController : Controller
    {

    [HttpGet]
        public ActionResult Seller()
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
