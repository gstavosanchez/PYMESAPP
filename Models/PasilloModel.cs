using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace F2_201801351.Models
{
    public class PasilloModel
    {
        public int idPasillo { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo nombre esta vacio")]
        [RegularExpression("([0-9]+)", ErrorMessage = "Solo Numeros")]
        [Display(Name = "Codigo ")]
        public int codigo { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo nombre esta vacio")]
        [RegularExpression("([0-9]+)", ErrorMessage = "Solo Numeros")]
        [Display(Name = "Largo ")]
        public int largo { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo nombre esta vacio")]
        [RegularExpression("([0-9]+)", ErrorMessage = "Solo Numeros")]
        [Display(Name = "ancho ")]
        public int ancho { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo nombre esta vacio")]
        [Display(Name = "Codigo Bodega ")]
        public string idBodega { get; set; }

        public string nombreBodega { get; set; }

    }
}