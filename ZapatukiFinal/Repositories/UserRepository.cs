using System;
using System.Collections.Generic;
using System.Linq;
using ZapatukiFinal.Repositories.Models;
using BCrypt.Net;

namespace ZapatukiFinal.Repositories
{
    public class UserRepository
    {
        private readonly ZAPATUKIEntities11 _db;

        public UserRepository() {
            _db = new ZAPATUKIEntities11();
        }

        public IEnumerable<CITY> GetCities()
        {
            return _db.CITies.ToList();
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

    }
}
