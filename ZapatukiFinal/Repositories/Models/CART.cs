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
    
    public partial class CART
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CART()
        {
            this.PRODUCT_CART = new HashSet<PRODUCT_CART>();
        }
    
        public int IdCart { get; set; }
        public int IdPerson { get; set; }
        public int IdCartState { get; set; }
        public double TotalPrice { get; set; }
        public System.DateTime Fecha { get; set; }
    
        public virtual CART_STATE CART_STATE { get; set; }
        public virtual PERSON PERSON { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PRODUCT_CART> PRODUCT_CART { get; set; }
    }
}
