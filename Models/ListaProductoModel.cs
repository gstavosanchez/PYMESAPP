using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace F2_201801351.Models
{
    public class ListaProductoModel
    {
        public int id { get; set; }
        public string codigo { get; set; }
        public string codigoBarra { get; set; }
        public string nombre { get; set; }
        public string descripcion { get; set; }
        public string clasificacion { get; set; }
        public string presentacion { get; set; }
        public string estado { get; set; }
    }
}