using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZapatukiFinal.Dtos.ViewModels;
using ZapatukiFinal.Dtos;
using ZapatukiFinal.Services;
using System.Web.UI;

namespace ZapatukiFinal.Controllers
{
    public class ProductUpdateController : Controller
    {
        private readonly ProductUpdateService _productService;
        public ProductUpdateController()
        {
            _productService = new ProductUpdateService();
        }

    [HttpGet]
        public ActionResult ProductSearch(ProductDto product, ResponseDto response = null)
        {
            var userLogged = Session["UserLogged"] as ResponseDto;

            if (userLogged == null || userLogged.Data == null)
            {
                return RedirectToAction("Seller", "Seller");
            }

            var viewModel = new UserViewModel
            {
                Product = new ProductDto(),
                Response = new ResponseDto(),
            };
            return View(viewModel);
        }

    [HttpPost]
        public ActionResult ProductSearch(ProductDto product)
        {
            try
            {
                ResponseDto response = _productService.Search(product);

                if (response.type == 1)
                {
                    var responseProd = new ResponseDto
                    {
                        Datapro = response.Datapro
                    };
                    Session["ProductSelected"] = responseProd;
                    if (Session["ProductSelected"] == null)
                    {
                        return CreateProductSearchViewModel(product, response);
                    }
                    else
                    {
                        return RedirectToAction("ProductUpdate", "ProductUpdate");
                    }
                }
                else
                {
                    return CreateProductSearchViewModel(product, response);
                }
            }
            catch (Exception ex)
            {
                var response = new ResponseDto
                {
                    type = 0,
                    message = "Unhandled error, please reload the page"
                };
                return CreateProductSearchViewModel(product, response);
            }
        }

        private ActionResult CreateProductSearchViewModel(ProductDto product, ResponseDto response = null)
        {
            var userLogged = Session["UserLogged"] as ResponseDto;

            if (userLogged == null || userLogged.Data == null)
            {
                return RedirectToAction("Login", "Login");
            }
            var viewModel = new UserViewModel
            {
                Product = new ProductDto(),
                Response = response ?? new ResponseDto()
            };
            return View(viewModel);
        }


    [HttpGet]
        public ActionResult ProductUpdate(ProductDto product, ResponseDto response = null)
        {
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
        public ActionResult ProductUpdate(ProductDto product)
        {
            try
            {
                ResponseDto response = _productService.ProductUpdate(product);

                if (response.type == 1)
                {
                    Session["ProductSelected"] = new ResponseDto { Datapro = product };
                    return RedirectToAction("Seller", "Seller");
                }
                else
                {
                    return RedirectToAction("ProductUpdate", "ProductUpdate");
                }
            }
            catch (Exception ex)
            {
                var response = new ResponseDto
                {
                    type = 0,
                    message = "Unhandled error, please reload the page"
                };
                return CreateProductUpdateViewModel(product, response);
            }
        }

        private ActionResult CreateProductUpdateViewModel(ProductDto product, ResponseDto response = null)
        {
            var userLogged = Session["UserLogged"] as ResponseDto;

            if (userLogged == null || userLogged.Data == null)
            {
                return RedirectToAction("Login", "Login");
            }

            var viewModel = new UserViewModel
            {
                Product = product,
                Response = response ?? new ResponseDto()
            };

            return View(viewModel);
        }
    }
}
