using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace F2_201801351.Models
{
    public class ListaPrecioModel
    {
        public int idModulo { get; set; }
        public string nombre { get; set; }

        public string abreviatura { get; set; }

        public string descripcion { get; set; }

        public int idLista { get; set; }

        public string fechaInicio { get; set; }

        public string fechaFin { get; set; }

        public string suscripcion { get; set; }

        public int ragoUsuario { get; set; }

        public double precio { get; set; }

    }
}