using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZapatukiFinal.Services
{
        public interface InterfaceSessionService
        {
            bool IsUserLoggedIn { get; set; } // Propiedad para verificar si el usuario está logueado
            string LoggedInUserEmail { get; set; } // Propiedad para almacenar el email del usuario logueado
        }
    }

