using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace F2_201801351.Models
{
    public class BodegaModel
    {
        public int idBodega { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo nombre esta vacio")]
        [Display(Name = "Codigo ")]
        public string codigo { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo nombre esta vacio")]
        [Display(Name = "Nombre ")]
        public string nombre { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo nombre esta vacio")]
        [Display(Name = "Descripcion ")]
        public string descripcion { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo nombre esta vacio")]
        [Display(Name = "Direccion ")]
        public string direccion { get; set; }
    }
}