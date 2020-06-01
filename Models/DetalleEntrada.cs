using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace F2_201801351.Models
{
    public class DetalleEntrada
    {
        public int idDetalleEntrada { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo nombre esta vacio")]
        [Display(Name = "Provedor ")]
        public string proeveedor { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo nombre esta vacio")]
        [Display(Name = "Producto ")]
        public int idDetalle { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo nombre esta vacio")]
        [RegularExpression("([0-9]+)", ErrorMessage = "Solo Numeros")]
        [Display(Name = "Cantidad de Producto")]
        public int cantidad { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo nombre esta vacio")]
        [RegularExpression("(([0-9]+).?([0-9]*))", ErrorMessage = "Solo Numeros")]
        [Display(Name = "Costo de Producto")]
        public double costoProudcto { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo nombre esta vacio")]
        [RegularExpression("(([0-9]+).?([0-9]*))", ErrorMessage = "Solo Numeros")]
        [Display(Name = "Precio Unitario")]
        public double precioUnitario { get; set; }

        public double costoTotal { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo nombre esta vacio")]
        [Display(Name = "Bodega")]
        public int idBodega { get; set; }
        public string codigoBodega { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo nombre esta vacio")]
        [Display(Name = "Pasillo")]
        public int idPasillo { get; set; }
        public string codigoPasillo { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo nombre esta vacio")]
        [Display(Name = "Estante")]
        public int idEstante { get; set; }
        public string codigoEstante { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo nombre esta vacio")]
        [Display(Name = "Nivel")]
        public int idNivel { get; set; }
        public string codigoNivel { get; set; }
        public string nombreProducto { get; set; }

        public List<detalleBodegaProducto> listaDetalleBodegaPruducto { get; set; }

        public List<BodegaModel> listaBodega { get; set; }

        public List<PasilloModel> listaPasillo { get; set; }

        public List<EstanteModel> listaEstante { get; set; }

        public List<NivelModel> listaNivel { get; set; }


    }
}