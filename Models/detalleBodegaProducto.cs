using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace F2_201801351.Models
{
    public class detalleBodegaProducto
    {
        public int idDetalle { get; set; }


        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo nombre esta vacio")]
        [Display(Name = "Producto ")]
        public int idProducto { get; set; }

        public string nombreProducto { get; set; }

        public string gestion { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo nombre esta vacio")]
        [Display(Name = "Forma del Lote ")]
        public string formaLote { get; set; }

        public List<ProductoModel> listaProducto { get; set; }
    }
}