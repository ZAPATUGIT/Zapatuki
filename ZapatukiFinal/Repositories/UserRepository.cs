using System;
using System.Collections.Generic;
using System.Linq;
using ZapatukiFinal.Repositories.Models;
using BCrypt.Net;
using System.Web.Helpers;
using ZapatukiFinal.Dtos;

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


        // Renovar contraseña
        public bool validatePassword(string email, string password)
        {
            PERSON user = _db.People.FirstOrDefault(u => u.Email == email);
            return BCrypt.Net.BCrypt.Verify(password, user.Password);
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
        public bool validateDocument(string documentNumber, string email)
        {
            return _db.People.Any(u => u.DocumentNumber == documentNumber && u.Email == email); // Devuelve true si existe / si corresponde
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
        public bool ProductExists(string name, string color)
        {
            return _db.PRODUCTs.Any(u => u.Name == name && u.Color == color);
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


    }
}
