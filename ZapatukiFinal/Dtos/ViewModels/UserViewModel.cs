using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ZapatukiFinal.Repositories.Models;

namespace ZapatukiFinal.Dtos.ViewModels
{
    public class UserViewModel
    {
        public UserDto User { get; set; }
        public ResponseDto Response { get; set; }
        public IEnumerable<CITY> Cities { get; set; }
    }
}