using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZapatukiFinal.Dtos.ViewModels;
using ZapatukiFinal.Dtos;
using ZapatukiFinal.Services;

namespace ZapatukiFinal.Controllers
{
    public class ProductRegisterController : Controller
    {
        private readonly ProductRegisterService _productService;
        public ProductRegisterController()
        {
            _productService = new ProductRegisterService();
        }

    [HttpGet]
        public ActionResult ProductRegister(ProductDto product, ResponseDto response = null)
        {
            var userLogged = Session["UserLogged"] as ResponseDto;

            if (userLogged == null || userLogged.Data == null)
            {
                return RedirectToAction("Login", "Login");
            }

            var types = _productService.GetProductTypes().ToList();
            var suppliers = _productService.GetSuppliers().ToList();
            var viewModel = new UserViewModel
            {
                Product = new ProductDto(),
                Response = new ResponseDto(),
                Types = types,
                Suppliers = suppliers
            };
            return View(viewModel);
        }

    [HttpPost]
        public ActionResult ProductRegister(ProductDto product)
        {
            try
            {
                ResponseDto response = _productService.ProductRegister(product);

                if (response.type == 1)
                {
                    return RedirectToAction("Seller", "Seller");
                }
                else
                {
                    return CreateProductRegisterViewModel(product, response);
                }
            }
            catch (Exception ex)
            {
                var response = new ResponseDto
                {
                    type = 0,
                    message = "Unhandled error, please reload the page"
                };
                return CreateProductRegisterViewModel(product, response);
            }

        }
        private ActionResult CreateProductRegisterViewModel(ProductDto product, ResponseDto response = null)
        {
            var userLogged = Session["UserLogged"] as ResponseDto;

            if (userLogged == null || userLogged.Data == null)
            {
                return RedirectToAction("Login", "Login");
            }

            var types = _productService.GetProductTypes().ToList();
            var suppliers = _productService.GetSuppliers().ToList();
            var viewModel = new UserViewModel
            {
                Product = new ProductDto(),
                Response = new ResponseDto(),
                Types = types,
                Suppliers = suppliers
            };
            return View(viewModel);
        }
    }
}
