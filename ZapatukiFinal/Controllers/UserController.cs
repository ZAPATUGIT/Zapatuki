using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZapatukiFinal.Dtos;
using ZapatukiFinal.Dtos.ViewModels;
using ZapatukiFinal.Repositories;
using ZapatukiFinal.Repositories.Models;
using ZapatukiFinal.Services;

namespace ZapatukiFinal.Controllers
{
    public class UserController : Controller
    {
        private readonly UserService _userService;

        public UserController()
        {
            _userService = new UserService();
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }
        public ActionResult ForgetPassword()
        {
            return View();
        }
        public ActionResult NewPassword()
        {
            return View();
        }
        public ActionResult UserRegistration()
        {
            // Cargar ciudades para la vista inicial
            var cities = _userService.GetCities().ToList(); // Obtener todas las ciudades o según un criterio inicial
            var viewModel = new UserViewModel
            {
                User = new UserDto(),
                Response = new ResponseDto(),
                Cities = cities // Cargar solo ciudades
            };
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult UserRegistration(UserDto user)
        {
            try
            {
                user.IdRole = 1; // Asignar un rol predeterminado
                ResponseDto response = _userService.UserRegistration(user);

                if (response.type == 1)
                {
                    return RedirectToAction("Login","User"); // Redirigir al login en caso de éxito
                }
                else
                {
                    return CreateUserViewModel(user, response);
                }
            }
            catch (Exception ex)
            {
                var response = new ResponseDto
                {
                    type = 0,
                    message = ex.Message
                };
                return CreateUserViewModel(user, response);
            }
        }

        [HttpPost]
        public ActionResult Login(UserDto user)
        {
            try
            {
                ResponseDto response = _userService.Login(user);

                if (response.type == 1)
                {
                    return RedirectToAction("Index","Home"); // Redirigir al Home en caso de éxito
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

        [HttpPost]
        public ActionResult ForgetPassword(UserDto user)
        {
            try
            {
                ResponseDto response = _userService.ForgetPassword(user);
                if (response.type == 1)
                {
                    return RedirectToAction("Login", "User"); // Redirigir al login en caso de éxito
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

        private ActionResult createForgetPasswrodViewModel(UserDto user,ResponseDto response)
        {
            var viewModel = new UserViewModel
            {
                User = new UserDto(),
                Response = response ?? new ResponseDto()
            };
            return View(viewModel);
        }

        private ActionResult createLoginViewModel(UserDto user, ResponseDto response)
        {
            var viewModel = new UserViewModel
            {
                User = new UserDto(),
                Response = response ?? new ResponseDto()
            };
            return View(viewModel);
        }

        private ActionResult CreateUserViewModel(UserDto user, ResponseDto response = null)
        {
            var cities = _userService.GetCities().ToList();

            var viewModel = new UserViewModel
            {
                User = user,
                Response = response ?? new ResponseDto(),
                Cities = cities
            };

            return View(viewModel);
        }
    }
}
