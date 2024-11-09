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

        public ResponseDto Login(UserDto userDto)
        {
            ResponseDto response = new ResponseDto();

            if (!_userRepo.UserExists(userDto.Email))
            {
                response.type = 0;
                response.message = "User doesn´t exist";
            }
            else
            {
                bool isValidPassword = _userRepo.validatePassword(userDto.Email, userDto.Password);
                if (isValidPassword)
                {
                    response.type = 1;
                    response.message = "Login successfull";
                } else
                {
                    response.type = 0;
                    response.message = "Invalid Password";
                }
            }

            return response;
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
                Password = BCrypt.Net.BCrypt.HashPassword(userDto.Password),
                IdRole =userDto.IdRole
            };
            ResponseDto response = new ResponseDto();
            try
            {
                if (_userRepo.UserExists(userDto.Email))
                {
                    response.type = 0;
                    response.message = "User doesn´t exist";
                }
                else
                {
                    if (_userRepo.UserRegistration(pERSON))
                    {
                        response.type = 1;
                        response.message = "User created";
                    }
                    else
                    {
                        response.type = 0;
                        response.message = "Something goes wrong";
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