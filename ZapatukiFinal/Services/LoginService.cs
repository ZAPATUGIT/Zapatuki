using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZapatukiFinal.Dtos;
using ZapatukiFinal.Dtos.ViewModels;
using ZapatukiFinal.Repositories;
using ZapatukiFinal.Utilities;

namespace ZapatukiFinal.Services
{
    public class LoginService
    {
        private readonly UserRepository _loginRepo;
        public LoginService()
        {
            _loginRepo = new UserRepository();
        }

        public ResponseDto Login(UserDto user)
        {
            ResponseDto response = new ResponseDto();
            try
            {
                if (!_loginRepo.UserExists(user.Email))
                {
                    response.type = 0;
                    response.message = "User doesn´t exist";
                }
                else
                {
                    UserDto userData = _loginRepo.GetPersonByEmail(user.Email);
                    if (_loginRepo.validatePassword(user.Email, user.Password))
                    {
                        response.Data = userData;
                        response.type = 1;
                        response.message = "User logged";
                    }
                    else
                    {   
                        response.type = 0;
                        response.message = "Invalid Password";
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

        public ResponseDto ForgetPassword(UserDto userDto)
        {
            ResponseDto response = new ResponseDto();
            try
            {
                if (!_loginRepo.validateDocument(userDto.DocumentNumber, userDto.Email))
                {
                    response.type = 0;
                    response.message = "User doesn´t exist or data doesn´t correspond";
                }
                else
                {
                    if (userDto.NewPassword != userDto.ConfirmPassword)
                    {
                        response.type = 0;
                        response.message = "Passwords do not match";
                    }
                    else
                    {
                        ;
                        string hashedPassword = BCrypt.Net.BCrypt.HashPassword(userDto.NewPassword);
                        bool updateResult = _loginRepo.UpdatePassword(userDto.Email, hashedPassword);

                        if (updateResult)
                        {
                            response.type = 1;
                            response.message = "Password updated successfully";
                        }
                        else
                        {
                            response.type = 0;
                            response.message = "Failed to update the password";
                        }
                    }
                }
                return response;
            }
            catch (Exception e)
            {
                response.type = 0;
                response.message = e.InnerException != null ? e.InnerException.ToString() : e.Message;
                return response;
            }
        }

    }
}