//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ZapatukiFinal.Repositories.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class PRODUCT_CART
    {
        public int IdProductCart { get; set; }
        public int IdProduct { get; set; }
        public int IdCart { get; set; }
    
        public virtual CART CART { get; set; }
        public virtual PRODUCT PRODUCT { get; set; }
    }
}
