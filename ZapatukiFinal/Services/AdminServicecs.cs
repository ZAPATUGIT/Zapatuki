using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZapatukiFinal.Dtos;
using ZapatukiFinal.Repositories;

namespace ZapatukiFinal.Services
{
    public class AdminService
    {
        private readonly UserRepository _adminRepository;

        public AdminService(UserRepository userRepository)
        {
            _adminRepository = userRepository;
        }
        public ResponseDto DeletePersonByEmail(string email)
        {
            return _adminRepository.DeletePersonByEmail(email);
        }
    }
}