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
        public ProductDto Product { get; set; }
        public IEnumerable<CITY> Cities { get; set; }
        public IEnumerable<PRODUCT_TYPE> Types { get; set; }
        public IEnumerable<SUPPLIER> Suppliers { get; set; }
    }
}