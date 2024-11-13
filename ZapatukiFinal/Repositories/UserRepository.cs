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

        public IEnumerable<CITY> GetCities()
        {
            try
            {
                return _db.CITies.ToList();
            }
            catch (Exception ex)
            {
                // Manejo de errores, puedes registrar el error o lanzar una excepción personalizada
                Console.WriteLine(ex.Message);
                return Enumerable.Empty<CITY>(); // Retorna una lista vacía en caso de error
            }
            return _db.CITies.ToList();

            
        }
        public UserRepository()
        {
            _db = new ZAPATUKIEntities11();
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

        public bool UserExists(string email)
        {
            return _db.People.Any(u => u.Email == email);
        }

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

        public UserDto GetPersonByEmail(string email)
        {
            var person = _db.People.FirstOrDefault(p => p.Email == email);
            if (person != null)
            {
                return new UserDto
                {
                    IdPerson = person.IdPerson,
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

    }
}
