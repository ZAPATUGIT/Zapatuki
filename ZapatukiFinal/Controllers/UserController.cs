using System;
using System.Linq;
using System.Web.Mvc;
using ZapatukiFinal.Dtos;
using ZapatukiFinal.Dtos.ViewModels;
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
        
        [HttpGet]
        public ActionResult UserRegistration()
        {
            // Cargar ciudades para la vista inicial
            var cities = _userService.GetCities().ToList();
            var viewModel = new UserViewModel
            {
                User = new UserDto(),
                Response = new ResponseDto(),
                Cities = cities // Cargar ciudades
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
                    return RedirectToAction("Login","Login"); // Redirigir al login en caso de éxito
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
                    message = "Unhandled error, please reload the page"
                };
                return CreateUserViewModel(user, response);
            }
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
