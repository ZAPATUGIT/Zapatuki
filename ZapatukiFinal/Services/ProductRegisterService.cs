using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZapatukiFinal.Dtos;
using ZapatukiFinal.Repositories;
using ZapatukiFinal.Repositories.Models;

namespace ZapatukiFinal.Services
{
    public class ProductRegisterService
    {
        private readonly UserRepository _productRepo;
        public ProductRegisterService()
        {
            _productRepo = new UserRepository();
        }
        public IEnumerable<PRODUCT_TYPE> GetProductTypes()
        {
            return _productRepo.GetProductTypes().ToList();
        }

        public IEnumerable<SUPPLIER> GetSuppliers()
        {
            return _productRepo.GetSuppliers().ToList();
        }

        public ResponseDto ProductRegister(ProductDto ProductDto)
        {
            ResponseDto response = new ResponseDto();
            try
            {
                PRODUCT pRODUCT = new PRODUCT
                {
                    IdProductType = ProductDto.IdProductType,
                    IdSupplier = ProductDto.IdSupplier,
                    Name = ProductDto.Name,
                    Brand = ProductDto.Brand,
                    Reference = ProductDto.Reference,
                    Color = ProductDto.Color,
                    Size = ProductDto.Size,
                    Description = ProductDto.Description,
                    Stock = ProductDto.Stock,
                    Price = ProductDto.Price
                };
                if (_productRepo.ProductExists(ProductDto.Name, ProductDto.Color))
                {
                    response.type = 0;
                    response.message = "Product exist";
                }
                else
                {
                    if (_productRepo.ProductRegistration(pRODUCT))
                    {
                        response.type = 1;
                        response.message = "Product created";
                    }
                    else
                    {
                        response.type = 0;
                        response.message = "Something goes wrong";
                    }
                }
                return response;
            }
            catch (Exception e)
            {
                response.type = 0;
                response.message = "Unhandled error, please reload the page";
                return response;
            }
        }
     }
 }
