using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using F2_201801351.ConnectionDB;

namespace F2_201801351.Controllers
{
    public class InventarioController : Controller
    {
        
        SQLConexion  conexion = new SQLConexion();
        
        [Authorize(Roles = "Inventario")]
        public ActionResult Index()
        {
            
            return View();
        }

        public string getUsuario()
        {
            string consulta = "select * from view_emailCompra;";
            string cadena = "";
            DataTable dt = conexion.ShowDataByQuery(consulta);
            int tamano = (dt.Rows.Count);
            int contador = 0;
            foreach (DataRow row in dt.Rows) 
            {
                if (contador == (tamano - 1))
                {
                    string email = row["email"].ToString();
                    cadena += email;
                }
                else {
                    string email = row["email"].ToString();
                    cadena += email + ",";
                }
                
                
                contador = contador + 1;
            }
             
            
            return cadena;

        }

        
    }
}
