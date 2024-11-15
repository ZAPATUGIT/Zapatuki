using System;
using System.Collections.Generic;
using System.Linq;
using ZapatukiFinal.Dtos;
using ZapatukiFinal.Repositories;
using ZapatukiFinal.Repositories.Models;

namespace ZapatukiFinal.Services
{
    public class ProductUpdateService
    {
        private readonly UserRepository _productRepo;
        public ProductUpdateService()
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

        public ResponseDto Search(ProductDto product)
        {
            ResponseDto response = new ResponseDto();

            try
            {
                if (!_productRepo.ProductExists(product.Name, product.Size, product.Color))
                {
                    response.type = 0;
                    response.message = "Product doesn´t exist";
                }
                else
                {
                    ProductDto productData = _productRepo.GetProdcutByName(product.Name, product.Size, product.Color);
                    if (_productRepo.ProductExists(product.Name, product.Size, product.Color))
                    {
                        response.Datapro = productData;
                        response.type = 1;
                        response.message = "Product found";
                    }
                    else
                    {
                        response.type = 0;
                        response.message = "Product not found";
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

        public ResponseDto ProductUpdate(ProductDto productDto)
        {
            ResponseDto response = new ResponseDto();
            try
            {
                bool updateResult = _productRepo.UpdateProduct(productDto.Reference, productDto.NewIdProductType,
                                                                productDto.NewIdSupplier, productDto.NewName, productDto.NewBrand,
                                                                productDto.NewColor, productDto.NewSize, productDto.NewDescription,
                                                                productDto.NewStock, productDto.NewPrice);
                ProductDto product = _productRepo.GetProdcutByName(productDto.Name, productDto.Brand, productDto.Color);
                if (updateResult)
                {
                    response.Datapro = product;
                    response.type = 1;
                    response.message = "Product updated successfully";
                }
                else
                {
                    response.type = 0;
                    response.message = "Failed to update the product";
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