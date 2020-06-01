using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace F2_201801351.Models
{
    public class DetalleSalida
    {
        public int idDetalleSalida { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo nombre esta vacio")]
        [Display(Name = "Cliente")]
        public string cliente { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo nombre esta vacio")]
        [RegularExpression("([0-9]+)", ErrorMessage = "Solo Numeros")]
        [Display(Name = "Cantidad de Producto")]
        public int cantidad { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo nombre esta vacio")]
        [RegularExpression("(([0-9]+).?([0-9]*))", ErrorMessage = "Solo Numeros")]
        [Display(Name = "Costo de Producto")]
        public double costoSalida { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo nombre esta vacio")]
        [Display(Name = "Producto")]
        public int idDetalleEntrada { get; set; }

        public List<DetalleEntrada> listaDetalleEntrada { get; set; }

    }
}