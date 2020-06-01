using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using F2_201801351.Models;
using F2_201801351.ConnectionDB;

namespace F2_201801351.Controllers
{
    [Authorize(Roles = "Inventario")]
    public class CostosController : Controller
    {

        SQLConexion conexion = new SQLConexion();
        List<detalleBodegaProducto> lista = new List<detalleBodegaProducto>();
        const string saldos = "saldos";
        const string lote = "lotes";
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Lotes() {

            detalleBodegaProducto detalle = new detalleBodegaProducto();
            detalle.listaProducto = getListaProducto();
            //return View(getListItemProdudcto());
            return View(detalle);
        }
        [HttpPost]
        public ActionResult Lotes(detalleBodegaProducto model) {
            if (ModelState.IsValid) {
                if (model.formaLote == "UEPS" || model.formaLote == "PEPS")
                {
                    System.Diagnostics.Debug.WriteLine(model.idProducto);
                    if (!datosDuplicados(model.idProducto))
                    {
                        string consulta = "insert into detalleProductoBodega(idProducto,gestion,formaLote) values (" + model.idProducto + ",'" + lote + "','" + model.formaLote + "');";
                        System.Diagnostics.Debug.WriteLine(consulta);
                        if (conexion.ExcuteQuery(consulta))
                        {
                            return RedirectToAction("DetalleLotes", "Costos");

                        }
                    }
                    else {
                        ModelState.AddModelError("Error", "Datos duplicados con el producto");
                    }
                    


                }
                else {
                    ModelState.AddModelError("Error", "No ingreso de fomra correcta el UEPS o PEPS");

                }
            
            }
            detalleBodegaProducto detalle = new detalleBodegaProducto();
            detalle.listaProducto = getListaProducto();
            return View(detalle);
        }


        public ActionResult Saldos() {

            List<ProductoModel> lista = getListaProducto();
            return View(lista);
        }

        [HttpGet]
        public ActionResult AddSaldos(int id) {
            System.Diagnostics.Debug.WriteLine(id);
            if (id != 0) {
                if (!datosDuplicados(id)) {
                    string consulta = "insert into detalleProductoBodega(idProducto,gestion) values (" + id + ",'" + saldos + "');";
                    System.Diagnostics.Debug.WriteLine(consulta);
                    if (conexion.ExcuteQuery(consulta))
                    {
                        return RedirectToAction("DetalleSaldos", "Costos");

                    }

                }
                

                
            }

            return RedirectToAction("Saldos", "Costos");
        }
        public ActionResult DetalleLotes() {
            getListaDetalleProducto(lote);
            return View(lista);
        }

        public ActionResult DetalleSaldos() {
            getListaDetalleProducto(saldos);
            return View(lista);
        }
        [HttpGet]
        public ActionResult Delete(int id) {
            string consulta = "delete from detalleProductoBodega where idDetalle = " + id + " ;";
            if (conexion.ExcuteQuery(consulta))
            {
                return RedirectToAction("Index", "Costos");
            }
            return View();

        }
        

        public List<ProductoModel> getListaProducto() {
            List<ProductoModel> lista = new List<ProductoModel>();
            string email = Session["Email"].ToString();
            string consulta = "select * from view_ProductoCliente where email like '" + email + "';";
            DataTable dt = conexion.ShowDataByQuery(consulta);

            foreach (DataRow row in dt.Rows) {
                ProductoModel producto = new ProductoModel();
                producto.id = Convert.ToInt32(row["idProducto"].ToString());
                producto.codigo = row["codigo"].ToString();
                producto.codigoBarra = row["codigoBarra"].ToString();
                producto.nombre = row["nombre"].ToString();
                producto.descripcion = row["descripcion"].ToString();
                producto.clasificacion = row["clasificacion"].ToString();
                producto.presentacion = row["presentacicon"].ToString();
                lista.Add(producto);
            }

            return lista;

        }

        public void getListaDetalleProducto(string parametro) {
            lista.Clear();
            string email = Session["Email"].ToString();
            string consulta = "select * from view_GestionCostos where email like '%" + email + "%' and gestion like '%"+parametro+"%' ;";
            DataTable dt = conexion.ShowDataByQuery(consulta);
            foreach (DataRow row in dt.Rows)
            {
                detalleBodegaProducto detalle = new detalleBodegaProducto();
                detalle.idDetalle = Convert.ToInt32(row["idDetalle"].ToString());
                detalle.idProducto = Convert.ToInt32(row["idProducto"].ToString());
                detalle.nombreProducto = row["producto"].ToString();
                detalle.gestion = row["gestion"].ToString();
                detalle.formaLote = row["formaLote"].ToString();
                lista.Add(detalle);
            }

        }

        public void getListaDetalleProducto()
        {
            lista.Clear();
            string email = Session["Email"].ToString();
            string consulta = "select * from view_GestionCostos where email like '%" + email + "%';";
            DataTable dt = conexion.ShowDataByQuery(consulta);
            foreach (DataRow row in dt.Rows)
            {
                detalleBodegaProducto detalle = new detalleBodegaProducto();
                detalle.idDetalle = Convert.ToInt32(row["idDetalle"].ToString());
                detalle.idProducto = Convert.ToInt32(row["idProducto"].ToString());
                detalle.nombreProducto = row["producto"].ToString();
                detalle.gestion = row["gestion"].ToString();
                detalle.formaLote = row["formaLote"].ToString();
                lista.Add(detalle);
            }

        }
        public Boolean datosDuplicados(int id) 
        {
            getListaDetalleProducto();
            foreach(detalleBodegaProducto detalle in lista) {
                if (detalle.idProducto == id) {
                    return true;
                }
            
            }
            return false;
        }


        public List<SelectListItem> getListItemProdudcto() {
            List<SelectListItem> lista = new List<SelectListItem>();
            List<ProductoModel> listaProducto = getListaProducto();
            foreach(ProductoModel pro in listaProducto) {
                lista.Add(new SelectListItem() { Text = pro.nombre, Value = pro.id.ToString() });   
            }
            return lista;
        }


    }
}
