using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace F2_201801351.Models
{
    public class UsuarioModel
    {
        public int id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo nombre esta vacio")]
        [Display(Name = "Nombre ")]
        public string nombre { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo nombre esta vacio")]
        [Display(Name = "Apellido ")]
        public string apellido { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Error el email es requerido")]
        [Display(Name = "Correo")]
        [EmailAddress]
        public string email { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "La Contraseña esta vacio")]
        [Display(Name = "Contraseña ")]
        [DataType(DataType.Password)]
        public string password { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "La confirmacion de la contraseña esta vacio")]
        [Display(Name = "Confir Contraseña")]
        [DataType(DataType.Password)]
        [Compare("password", ErrorMessage = "La contraseña no coincide con el campo anterior")]
        public string confiPassword { get; set; }

    }
}