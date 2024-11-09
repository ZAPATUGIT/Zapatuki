using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using ZapatukiFinal.Dtos;
using ZapatukiFinal.Repositories;
using ZapatukiFinal.Repositories.Models;

namespace ZapatukiFinal.Services
{
    public class UserService
    {
        private readonly UserRepository _userRepo;
        public UserService()
        {
            _userRepo = new UserRepository();
        }

        public IEnumerable<CITY> GetCities()
        {
            return _userRepo.GetCities().ToList();
        }

        public ResponseDto UserRegistration(UserDto userDto)
        {
            PERSON pERSON = new PERSON
            {
                IdCity = userDto.IdCity,
                Address = userDto.Address,
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                DocumentNumber = userDto.DocumentNumber,
                Phone = userDto.Phone,
                Email = userDto.Email,
                Password = userDto.Password,
                IdRole =userDto.IdRole
            };
            ResponseDto response = new ResponseDto();
            try
            {
                if (_userRepo.UserExists(userDto.Email))
                {
                    response.type = 0;
                    response.message = "El usuario ya existe";
                }
                else
                {
                    if (_userRepo.UserRegistration(pERSON))
                    {
                        response.type = 1;
                        response.message = "Creado con exito";
                    }
                    else
                    {
                        response.type = 0;
                        response.message = "Algo pasó";
                    }
                }
                return response;
            }
            catch (Exception e) {
                response.type = 0;
                response.message = e.InnerException !=null ? e.InnerException.ToString(): e.Message;
                return response;
            }
        }
    }
}