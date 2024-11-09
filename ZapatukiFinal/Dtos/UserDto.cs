using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;
using ZapatukiFinal.Repositories.Models;

namespace ZapatukiFinal.Dtos
{
    public class UserDto
    {
        public int IdRole { get; set; }
        public int IdCity { get; set; }
        public string Address { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string DocumentNumber { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}