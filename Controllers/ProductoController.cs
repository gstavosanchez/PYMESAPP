using F2_201801351.ConnectionDB;
using F2_201801351.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace F2_201801351.Controllers
{
    [Authorize]
    public class ProductoController : Controller
    {
        SQLConexion conexion = new SQLConexion();
        List<ListaProductoModel> listProducto;
        // GET: Producto
        public ActionResult Index()
        {
            return View();
        }
        
        public ActionResult New() {

            return View();
        
        }

        [HttpPost]
        public ActionResult New(ProductoModel m,string activo,string inactivo) {

            if (ModelState.IsValid) {
                string email = Session["Email"].ToString();

                if (activo.Equals("true") && !IsEmpy(activo))
                {
                    string ac = "activo";
                    string consulta = "EXEC sp_AgregarProducto '" + email + "','" + m.codigo + "','" + m.codigoBarra + "','" + m.nombre + "','" + m.descripcion + "','" + m.clasificacion + "','" + ac + "','" + m.presentacion + "' ;";
                    System.Diagnostics.Debug.WriteLine(consulta);
                    if (conexion.ExcuteQuery(consulta))
                    {
                        return RedirectToAction("Index", "Producto");
                    }
                    else
                    {
                        ModelState.AddModelError("Error", "Consulta Invalida");

                    }

                }
                else if (inactivo.Equals("true") && !IsEmpy(inactivo))
                {
                    string ina = "inactivo";
                    string consulta = "EXEC sp_AgregarProducto '" + email + "','" + m.codigo + "','" + m.codigoBarra + "','" + m.nombre + "','" + m.descripcion + "','" + m.clasificacion + "','" + ina + "','" + m.presentacion + "' ;";
                    if (conexion.ExcuteQuery(consulta))
                    {
                        return RedirectToAction("Index", "Producto");
                    }
                    else
                    {
                        ModelState.AddModelError("Error", "Consulta Invalida");
                    }
                }
                else {
                    ModelState.AddModelError("Error", "Campos Incompletos");

                }
                
            
            
            }
            return View();
        }
        public ActionResult See() {
            llenarLista();

            return View(listProducto);
        
        }
        public bool IsEmpy(string s)
        {
            bool result;
            result = s == null || s == string.Empty;
            return result;
        }

        public void llenarLista() {
            string email = Session["Email"].ToString();
            string consulta = "select * from view_ProductoCliente where email like '" + email + "';";
            DataTable dt = conexion.ShowDataByQuery(consulta);
            listProducto = new List<ListaProductoModel>();
            foreach (DataRow row in dt.Rows) {
                ListaProductoModel producto = new ListaProductoModel();
                producto.id = Convert.ToInt32(row["idProducto"].ToString());
                producto.codigo = row["codigo"].ToString();
                producto.codigoBarra = row["codigoBarra"].ToString();
                producto.nombre = row["nombre"].ToString();
                producto.descripcion = row["descripcion"].ToString();
                producto.clasificacion = row["clasificacion"].ToString();
                producto.presentacion = row["presentacicon"].ToString();
                producto.estado = row["estado"].ToString();
                listProducto.Add(producto);

            }


        }
        [HttpGet]
        public ActionResult Delete(int id) {
            string consulta = "delete Producto where idProducto = " + id + " ;";
            if (conexion.ExcuteQuery(consulta)) {
                return RedirectToAction("See", "Producto");
            }
            return View();
        
        }
        



    }
}
