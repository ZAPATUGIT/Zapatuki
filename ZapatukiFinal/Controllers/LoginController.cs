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
                    Session["UserLogged"] = response.Data;
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



    [HttpGet]
        public ActionResult Administrator()
        {
            var viewModel = new UserViewModel
            {
                User = new UserDto(),
                Response = new ResponseDto(),
            };
            return View(viewModel);
        }

    [HttpPost]
        public ActionResult Admiistrator()
        {
            var viewModel = new UserViewModel
            {
                User = new UserDto(),
                Response = new ResponseDto()
                {
                    type = 1,
                    message = "Welcome to the admin section",
                    Data = new UserDto()
                }
            };
            return View(viewModel);
        }
        private ActionResult createAdminViewModel(UserDto user, ResponseDto response)
        {
            var viewModel = new UserViewModel
            {
                User = new UserDto(),
                Response = response ?? new ResponseDto()
            };
            return View(viewModel);
        }



    [HttpGet]
        public ActionResult Seller()
        {
            var viewModel = new UserViewModel
            {
                User = new UserDto(),
                Response = new ResponseDto(),
            };
            return View(viewModel); // Asegúrate de que este objeto no sea null
        }

    [HttpPost]
        public ActionResult Selller()
        {
            var viewModel = new UserViewModel
            {
                User = new UserDto(),
                Response = new ResponseDto()
                {
                    type = 1,
                    message = "Welcome to the admin section",
                    Data = new UserDto()
                }
            };
            return View(viewModel);
        }
        private ActionResult createSellerViewModel(UserDto user, ResponseDto response)
        {
            var viewModel = new UserViewModel
            {
                User = new UserDto(),
                Response = response ?? new ResponseDto()
            };
            return View(viewModel);
        }



        [HttpGet]
        public ActionResult Cart()
        {
            UserDto user = Session["UserLogged"] as UserDto;
            return View(user);
        }
    }
}
