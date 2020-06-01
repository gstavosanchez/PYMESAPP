using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace F2_201801351.Models
{
    public class NivelModel
    {
       public int idNivel { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo nombre esta vacio")]
        [Display(Name = "Codigo ")]
        [RegularExpression("([0-9]+)", ErrorMessage = "Solo Numeros")]
        public int codigo { set; get; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo nombre esta vacio")]
        [Display(Name = "Codigo Estante")]
        public string idEstante { set; get; }

        public int ancho { get; set; }
        public int largo { get; set; }
    }
}