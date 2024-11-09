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
        public ActionResult LoadCities()
        {
            var cities = new List<CITY>();

            cities = _userService.GetCities().ToList();

            var viewModel = new UserViewModel
            {
                Response = new ResponseDto
                {
                    Cities = cities
                }
            };

            return View("UserRegistration", viewModel);
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
                    return RedirectToAction("Login"); // Redirigir al login en caso de éxito
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

        private ActionResult CreateUserViewModel(UserDto user, ResponseDto response = null)
        {
            var cities = _userService.GetCities().ToList(); // Solo obtener ciudades

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
