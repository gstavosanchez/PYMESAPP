using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace F2_201801351.Models
{
    public class ClienteModel
    {

        public int idCliente { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo nombre esta vacio")]
        [Display(Name = "Nombre ")]
        public string nombre { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo  esta vacio")]
        [Display(Name = "NIT")]
        public string nit { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo  esta vacio")]
        [Display(Name = "Tipo Empresa ")]
        public string tipoEmpresa { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo  esta vacio")]
        [Display(Name = "Tamaño ")]
        public string tamano { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo  esta vacio")]
        [Display(Name = "No.Tarjeta ")]
        public string noTarjeta { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo  esta vacio")]
        [Display(Name = "Nombre Tarjeta ")]
        public string nombreTarjeta { get; set; }


        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo  esta vacio")]
        [Display(Name = "Tipo Tarjeta")]
        public string tipoTarjeta { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo  esta vacio")]
        [Display(Name = "CRV Tarjeta ")]
        public string CRVTarjeta { get; set; }

    }
}