using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ZapatukiFinal.Repositories.Models;

namespace ZapatukiFinal.Dtos
{
    public class ProductDto
    {
        public int IdProduct { get; set; }
        public int  IdProductType { get; set; }
        public int IdSupplier { get; set; }
        public string Name { get; set; }
        public string Brand { get; set; }
        public string Reference { get; set; }
        public string Color { get; set; }
        public string Size { get; set; }
        public string Description { get; set; }
        public int Stock { get; set; }
        public float Price { get; set; }

        public int NewIdProductType { get; set; }
        public int NewIdSupplier { get; set; }
        public string NewName { get; set; }
        public string NewBrand { get; set; }
        public string NewColor { get; set; }
        public string NewSize { get; set; }
        public string NewDescription { get; set; }
        public int NewStock { get; set; }
        public float NewPrice { get; set; }
    }
}