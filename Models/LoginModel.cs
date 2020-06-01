using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace F2_201801351.Models
{
    public class LoginModel
    {

        [Required(AllowEmptyStrings = false, ErrorMessage = "Error el email es requerido")]
        [Display(Name = "Correo:")]
        [EmailAddress]
        public string email { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Error la contraseña es requerido")]
        [Display(Name = "Password:")]
        [DataType(DataType.Password)]
        public string password { get; set; }
    }
}