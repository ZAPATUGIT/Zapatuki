using Rotativa;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using ZapatukiFinal.Controllers;
using ZapatukiFinal.Dtos;
using ZapatukiFinal.Repositories;
using ZapatukiFinal.Repositories.Models;
using ZapatukiFinal.Utilities;

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
            ResponseDto response = new ResponseDto();
            try
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
                IdRole = userDto.IdRole
            };
                if (_userRepo.UserExists(userDto.Email))
                {
                    response.type = 0;
                    response.message = "User exist";
                }
                else
                {
                    if (_userRepo.UserRegistration(pERSON))
                    {
                        response.type = 1;
                        response.message = "User created";
                        SendUserDetailsEmail(userDto);
                    }
                    else
                    {
                        response.type = 0;
                        response.message = "One or more fields are wrong, Verify";
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
        //public string Print(UserDto user)
        //{
        //    var filePath = Path.Combine(HttpContext.Current.Server.MapPath("~/ArchivosPDF"), $"User Details_{user.Email}.pdf");
        //    var pdfResult = new Rotativa.ViewAsPdf("Report"); // Reemplaza con el nombre de tu vista
        //    var pdfBytes = pdfResult.BuildFile("Report");
        //    System.IO.File.WriteAllBytes(filePath, pdfBytes);
        //    return filePath;
        //}
        private void SendUserDetailsEmail(UserDto user)
        {
            Email emailManager = new Email();

            //string addressee = user.Email;
            string addressee = "davidtorrado1402@gmail.com";
            string affair = "User Details Notification";
            string message = $@"
                           <html>
                                <body style='font-family: Arial, sans-serif; background-color: #f0f8ff; padding: 20px;'>
                                    <div style='border: 1px solid #40e0d0; border-radius: 10px; padding: 15px;'>
                                        <h2 style='color: #40e0d0;'>User Details Notification</h2>
                                        <p style='color: #333;'>Notificación de registro en Zapatuki <b>No responder a este correo.</b></p>
                                        <ol style='color: #40e0d0;'>
                                            <li>Address: {user.Address}</li>
                                            <li>First Name: {user.FirstName}</li>
                                            <li>Last Name: {user.LastName}</li>
                                            <li>Document Number: {user.DocumentNumber}</li>
                                            <li>Phone: {user.Phone}</li>
                                            <li>Email: {user.Email}</li>
                                        </ol>
                                        <p style='color: #40e0d0;'>Gracias por usar nuestro servicio.</p>
                                    </div>
                                </body>
                            </html>";
            
            //var pdfPath = Print(user);

            // Adjuntar el archivo PDF
            //var attachment = new Attachment(pdfPath);

            ;
            
            emailManager.SendEmail(addressee, affair, message, true);
        }
    }
}