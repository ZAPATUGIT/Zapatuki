using System;
using System.Collections.Generic;
using System.Linq;
using ZapatukiFinal.Repositories.Models;
using BCrypt.Net;
using System.Web.Helpers;
using ZapatukiFinal.Dtos;
using ZapatukiFinal.Utilities;
using System.Data.SqlClient;

namespace ZapatukiFinal.Repositories
{
    public class UserRepository
    {
        private readonly ZAPATUKIEntities11 _db;
        public UserRepository()
        {
            _db = new ZAPATUKIEntities11();
        }

        //Registro de usuarios
        public IEnumerable<CITY> GetCities()
        {
            try
            {
                return _db.CITies.ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Enumerable.Empty<CITY>(); // Retorna una lista vacía en caso de error
            }
        }
        public bool UserRegistration(PERSON pERSON)
        {
            try
            {
                {
                    _db.People.Add(pERSON);
                    _db.SaveChanges();

                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        //Validar login
        public bool UserExists(string email)
        {
            return _db.People.Any(u => u.Email == email);
        }
        public UserDto GetPersonByEmail(string email)
        {
            var person = _db.People.FirstOrDefault(p => p.Email == email);
            if (person != null)
            {
                return new UserDto
                {
                    IdPerson = person.IdPerson,
                    IdCity = person.IdCity,
                    Address = person.Address,
                    FirstName = person.FirstName,
                    LastName = person.LastName,
                    DocumentNumber = person.DocumentNumber,
                    Phone = person.Phone,
                    Email = person.Email,
                    IdRole = person.IdRole
                };
            }
            return null;
        }
        public bool validatePassword(string email, string password)
        {
            PERSON user = _db.People.FirstOrDefault(u => u.Email == email);
            return BCrypt.Net.BCrypt.Verify(password, user.Password);
        }


        // Renovar contraseña
        public bool validateDocument(string documentNumber, string email)
        {
            return _db.People.Any(u => u.DocumentNumber == documentNumber && u.Email == email); // Devuelve true si existe / si corresponde
        }
        public bool UpdatePassword(string email, string newPassword)
        {
            try
            {
                var person = _db.People.FirstOrDefault(p => p.Email == email);
                person.Password = newPassword;
                _db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        //Registro productos
        public IEnumerable<PRODUCT_TYPE> GetProductTypes()
        {
            try
            {
                return _db.PRODUCT_TYPE.ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Enumerable.Empty<PRODUCT_TYPE>(); // Retorna una lista vacía en caso de error
            }
        }
        public IEnumerable<SUPPLIER> GetSuppliers()
        {
            try
            {
                return _db.SUPPLIERs.ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Enumerable.Empty<SUPPLIER>(); // Retorna una lista vacía en caso de error
            }
        }
        public bool ProductRegistration(PRODUCT pRODUCT)
        {
            try
            {
                {
                    _db.PRODUCTs.Add(pRODUCT);
                    _db.SaveChanges();

                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        //validacion producto
        public bool ProductExists(string name, string size, string color)
        {
            return _db.PRODUCTs.Any(p => p.Name == name && p.Size == size && p.Color == color);
        }


        //Actualizar
        public ProductDto GetProdcutByName(string name, string size, string color)
        {
            var product = _db.PRODUCTs.FirstOrDefault(p => p.Name == name && p.Size == size && p.Color == color);
            if (product != null)
            {
                return new ProductDto
                {
                    IdProduct = product.IdProduct,
                    IdProductType = product.IdProductType,
                    IdSupplier = product.IdProduct,
                    Name = product.Name,
                    Brand = product.Brand,
                    Reference = product.Reference,
                    Color = product.Color,
                    Size = product.Size,
                    Description = product.Description,
                    Stock = product.Stock,
                    Price = (float)product.Price
                };
            }
            return null;
        }

        public bool UpdateProduct(
                string reference, int NewIdProductType, int NewIdSupplier, string NewName, string NewBrand, string NewColor,
                string NewSize, string NewDescription, int NewStock, float NewPrice
                )
        {
            try
            {
                var product = _db.PRODUCTs.FirstOrDefault(p => p.Reference == reference);
                if (product != null)
                    product.IdProductType = NewIdProductType;
                product.IdSupplier = NewIdSupplier;
                product.Name = NewName;
                product.Brand = NewBrand;
                product.Reference = reference;
                product.Color = NewColor;
                product.Size = NewSize;
                product.Description = NewDescription;
                product.Stock = NewStock;
                product.Price = (float)NewPrice;

                _db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        //Delete por correo, solo el admin puede
        public ResponseDto DeletePersonByEmail(string email)
        {
            try
            {
                // Ejecutar el stored procedure para eliminar a la persona
                int rowsAffected = _db.Database.ExecuteSqlCommand("EXEC DeletePersonByEmail @Email",
                    new SqlParameter("@Email", email));

                if (rowsAffected > 0)
                {
                    return new ResponseDto
                    {
                        type = 1,
                        message = "User Deleted succesfull."
                    };
                }
                else
                {
                    return new ResponseDto
                    {
                        type = 0,
                        message = "User doesn´t exist"
                    };
                }
            }
            catch (Exception ex)
            {
                // Manejar la excepción
                return new ResponseDto
                {
                    type = 0,
                    message = "Delete error"
                };
            }
        }
    }
}
