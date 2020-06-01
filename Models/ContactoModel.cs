using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace F2_201801351.Models
{
    public class ContactoModel
    {
        public int idComercial { get; set; }
        public int idGerencial { get; set; }
        public int idAdministrador { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo nombre esta vacio")]
        [Display(Name = "Contacto Comercial ")]
        public string nombreComercial { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo nombre esta vacio")]
        [Display(Name = "Contacto Gerencial")]
        public string nombreGerencial { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo nombre esta vacio")]
        [Display(Name = "Contacto Adminstrador")]
        public string nombreAdministradro { get; set; }




        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo nombre esta vacio")]
        [Display(Name = "Apellido del Comercial ")]
        public string apellidoComercial { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo nombre esta vacio")]
        [Display(Name = "Apellido del Gerencial")]
        public string apellidoGerencial { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo nombre esta vacio")]
        [Display(Name = "Apellido del Adminstrador")]
        public string apellidoAdministradro { get; set; }




        [Required(AllowEmptyStrings = false, ErrorMessage = "Error el email es requerido")]
        [Display(Name = "Correo Comercial")]
        [EmailAddress]
        public string emailComercial { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Error el email es requerido")]
        [Display(Name = "Correo Gerencial")]
        [EmailAddress]
        public string emailGerencial { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Error el email es requerido")]
        [Display(Name = "Correo Admistrador")]
        [EmailAddress]
        public string emailAdmin { get; set; }

        public string SuccessMessage { get; set; }



    }
}