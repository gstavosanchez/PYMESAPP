using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace F2_201801351.Models
{
    public class ProductoModel
    {
        public int id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo nombre esta vacio")]
        [Display(Name = "Codigo ")]
        public string codigo { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo nombre esta vacio")]
        [Display(Name = "Codigo de Barra ")]
        public string codigoBarra { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo nombre esta vacio")]
        [Display(Name = "Nombre ")]
        public string nombre { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo nombre esta vacio")]
        [Display(Name = "Descripcion ")]
        public string descripcion { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo nombre esta vacio")]
        [Display(Name = "Clasificacion ")]
        public string clasificacion { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo nombre esta vacio")]
        [Display(Name = "Presentacion ")]
        public string presentacion { get; set; }



    }
}