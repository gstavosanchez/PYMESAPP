using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace F2_201801351.Models
{
    public class EstanteModel
    {
        public int idEstante { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo nombre esta vacio")]
        [Display(Name = "Codigo ")]
        public string codigo { set; get; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo nombre esta vacio")]
        [RegularExpression("([0-9]+)", ErrorMessage = "Solo Numeros")]
        public int largo { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo nombre esta vacio")]
        [RegularExpression("([0-9]+)", ErrorMessage = "Solo Numeros")]
        public int ancho { get; set; }


        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo nombre esta vacio")]
        [RegularExpression("([0-9]+)", ErrorMessage = "Solo Numeros")]
        public int idPasillo { get; set; }
    }
}