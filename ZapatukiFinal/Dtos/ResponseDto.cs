using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZapatukiFinal.Repositories.Models;

namespace ZapatukiFinal.Dtos
{
    public class ResponseDto
    {
        public int? IdCity { get; set; }
        public int type {  get; set; }
        public string message { get; set; }
        public UserDto Data { get; set; }
    }
}