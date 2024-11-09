using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.Web;
using ZapatukiFinal.Dtos;
using ZapatukiFinal.Repositories.Models;

namespace ZapatukiFinal.Repositories
{
    public class UserRepository
    {
        private readonly ZAPATUKIEntities9 _db;

        public UserRepository() {
            _db = new ZAPATUKIEntities9();
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

    }
}
