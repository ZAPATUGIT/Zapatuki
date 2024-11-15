using System;
using System.Web.Mvc;
using ZapatukiFinal.Dtos.ViewModels;
using ZapatukiFinal.Dtos;
using ZapatukiFinal.Services;

namespace ZapatukiFinal.Controllers
{
    public class LoginController : Controller
    {
        private readonly LoginService _loginService;
        public LoginController()
        {
            _loginService = new LoginService();
        }

    [HttpGet]
        public ActionResult Login()
        {
            // Cargar para la vista inicial
            var viewModel = new UserViewModel
            {
                User = new UserDto(),
                Response = new ResponseDto(),
            };
            return View(viewModel);
        }

    [HttpPost]
        public ActionResult Login(UserDto user)
        {
            try
            {
                ResponseDto response = _loginService.Login(user);
                
                if (response.type == 1)
                {
                    var responseLog = new ResponseDto
                    {
                        Data = response.Data
                    };

                    Session["UserLogged"] = responseLog;

                    switch (response.Data.IdRole)
                    {
                        case 1:
                            return RedirectToAction("Index", "Home"); // Usuario
                        case 2:
                            return RedirectToAction("Seller", "Seller"); // Vendedor
                        case 3:
                            return RedirectToAction("Administrator", "Admin");
                        default:
                            return RedirectToAction("Index", "Home"); // Rol desconocido
                    }
                }
                else
                {
                    return createLoginViewModel(user, response);
                }
            }
            catch (Exception ex)
            {
                var response = new ResponseDto
                {
                    type = 0,
                    message = ex.Message
                };
                return createLoginViewModel(user, response);
            }
        }
        private ActionResult createLoginViewModel(UserDto user, ResponseDto response = null)
        {
            var viewModel = new UserViewModel
            {
                User = user,
                Response = response ?? new ResponseDto()
            };
            return View(viewModel);
        }




    [HttpGet]
        public ActionResult ForgetPassword()
        {
            // Cargar para la vista inicial
            var viewModel = new UserViewModel
            {
                User = new UserDto(),
                Response = new ResponseDto(),
            };
            return View(viewModel);
        }

    [HttpPost]
        public ActionResult ForgetPassword(UserDto user)
        {
            try
            {
                ResponseDto response = _loginService.ForgetPassword(user);
                if (response.type == 1)
                {
                    return RedirectToAction("Login", "Login"); // Redirigir al login en caso de éxito
                }
                else
                {
                    return createForgetPasswrodViewModel(user, response);
                }
            }
            catch (Exception ex)
            {
                var response = new ResponseDto
                {
                    type = 0,
                    message = ex.Message
                };
                return createForgetPasswrodViewModel(user, response);
            }
        }
        private ActionResult createForgetPasswrodViewModel(UserDto user, ResponseDto response = null)
        {
            var viewModel = new UserViewModel
            {
                User = user,
                Response = response ?? new ResponseDto()
            };
            return View(viewModel);
        }


    [HttpPost]
        public ActionResult Logout()
        {
            Session.Clear();
            Session.Abandon();
            return RedirectToAction("Login", "Login");
        }


    [HttpGet]
        public ActionResult Cart()
        {
            UserDto user = Session["UserLogged"] as UserDto;
            return View(user);
        }
    }
}
