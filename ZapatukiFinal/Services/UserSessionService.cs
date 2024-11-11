using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZapatukiFinal.Services
{
        public class UserSessionService : InterfaceSessionService
        {
            public bool IsUserLoggedIn { get; set; }
            public string LoggedInUserEmail { get; set; }
        }
    }
