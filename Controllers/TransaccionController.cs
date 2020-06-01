using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using F2_201801351.Models;
using F2_201801351.ConnectionDB;
using System.Data;
using System.Drawing.Printing;

namespace F2_201801351.Controllers
{
    [Authorize(Roles = "Inventario")]
    public class TransaccionController : Controller
    {
        List<DetalleEntrada> listaEntrada = new List<DetalleEntrada>();
        SQLConexion conexion = new SQLConexion();
        private int cantidadREs;
        DetalleEntrada entradaGeneral = new DetalleEntrada();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Entrada() {
            DetalleEntrada detalleEntrada = new DetalleEntrada();
            detalleEntrada.listaDetalleBodegaPruducto = getListaDetalleBodegaProducto();
            return View(detalleEntrada);
        }

        [HttpPost]
        public ActionResult Entrada(DetalleEntrada modelo) {
            if (ModelState.IsValid) {
                if (modelo.costoProudcto != 0 && modelo.cantidad != 0)
                {
                    double costoTotal = modelo.costoProudcto * modelo.cantidad;
                    string consulta = "insert into detalleEntrada(provedoor,idDetalle,cantidad,costoProducto,precioUnitario,costoTotal) values ('" + modelo.proeveedor + "'," + modelo.idDetalle + "," + modelo.cantidad + "," + modelo.costoProudcto + ","+modelo.costoProudcto+"," + costoTotal + ");";
                    //System.Diagnostics.Debug.WriteLine("idDetalle:" + modelo.idDetalle + " Proveedor: " + modelo.proeveedor + " costo:" + modelo.costoProudcto + " cantidad:" + modelo.cantidad);
                    System.Diagnostics.Debug.WriteLine(consulta);
                    if (conexion.ExcuteQuery(consulta))
                    {
                        return RedirectToAction("DetalleEntradas", "Transaccion");

                    }

                }
                else {
                    ModelState.AddModelError("Error", "No se aceptan ceros(0).");
                }
                
            }


            DetalleEntrada detalleEntrada = new DetalleEntrada();
            detalleEntrada.listaDetalleBodegaPruducto = getListaDetalleBodegaProducto();
            return View(detalleEntrada);
        }


        public ActionResult DetalleEntradas() {
            getListDetalleEntrada(); 
            return View(listaEntrada);
        }

        [HttpGet]
        public ActionResult Update(int id) {
            DetalleEntrada detalle = getObjetDetalleEntrada(id);
            detalle.idDetalleEntrada = id;
            detalle.listaBodega = getListaBodega();
            detalle.listaPasillo = getListaPasillo();
            detalle.listaEstante = getListaEstante();
            detalle.listaNivel = getListNivelModel();
                
            return View(detalle);
        }
        


        [HttpPost]
        public ActionResult Update(DetalleEntrada m) {
            if (ModelState.IsValid) {
                int bandera = isEmpyBodegaPasillo(m.idBodega, m.idPasillo, m.idEstante, m.idNivel);
                if (bandera != 0)
                {
                    if (!ubicacionDuplicada(m.idDetalleEntrada,m.idBodega, m.idPasillo, m.idEstante, m.idNivel))
                    {
                        if (m.idBodega != 0 && m.idPasillo != 0 && m.idEstante != 0 && m.idNivel != 0)
                        {
                            string consulta = "EXEC sp_UpdateDetalleEntrada " + m.idDetalleEntrada + ",'" + m.proeveedor + "'," + m.precioUnitario + "," + m.idBodega + "," + m.idPasillo + "," + m.idEstante + "," + m.idNivel + ";";
                            System.Diagnostics.Debug.WriteLine(consulta);
                            if (conexion.ExcuteQuery(consulta))
                            {
                                
                                return RedirectToAction("DetalleEntradas", "Transaccion");

                            }
                        }
                        else {
                            ModelState.AddModelError("Error", "El id del bodega o pasillo no puede ser 0");

                        }
                        
                    }
                    else {
                        ModelState.AddModelError("Error", "Ya existe un producto en esa ubicacion");
                    }



                }
                else {
                    ModelState.AddModelError("Error", "No existe esa asigancion para la bodega, de pasillo, estante o nivel");

                }
            
            }
            DetalleEntrada detalle = getObjetDetalleEntrada(m.idDetalleEntrada);
            detalle.listaBodega = getListaBodega();
            detalle.listaPasillo = getListaPasillo();
            detalle.listaEstante = getListaEstante();
            detalle.listaNivel = getListNivelModel();

            return View(detalle);
        }

        public ActionResult Salida() {
            getListDetalleEntrada();
            DetalleSalida salida = new DetalleSalida();
            salida.listaDetalleEntrada = listaEntrada; 

            return View(salida);
        }
        public void getUpdateDetalleEntrada(int id, int cantidad) {
            System.Diagnostics.Debug.WriteLine(id);
            System.Diagnostics.Debug.WriteLine(cantidad);
            DetalleEntrada detalle = getObjetDetalleEntrada(id);
            detalle.idDetalleEntrada = id;
            detalle.cantidad = cantidad;
            detalle.listaBodega = getListaBodega();
            detalle.listaPasillo = getListaPasillo();
            detalle.listaEstante = getListaEstante();
            detalle.listaNivel = getListNivelModel();

            this.entradaGeneral = detalle;
            
            
            
        }
        [HttpGet]
        public ActionResult UpdateUbicacion(int id,int otherParam)
        {
            DetalleEntrada detalles = getObjetDetalleEntrada(id);
            detalles.idDetalleEntrada = id;
            detalles.cantidad = otherParam;
            detalles.listaBodega = getListaBodega();
            detalles.listaPasillo = getListaPasillo();
            detalles.listaEstante = getListaEstante();
            detalles.listaNivel = getListNivelModel();

            System.Diagnostics.Debug.WriteLine(detalles.cantidad);
            return View(detalles);
        }
        [HttpPost]
        public ActionResult UpdateUbicacion(DetalleEntrada m)
        {
            string consss = "Consultassss EXEC sp_UpdateCantidad " + m.idDetalleEntrada + "," + m.cantidad + " ;";
            System.Diagnostics.Debug.WriteLine(consss);
            if (ModelState.IsValid)
            {
                string cons = "Consulta EXEC sp_UpdateCantidad " + m.idDetalleEntrada + "," + m.cantidad + " ;";
                System.Diagnostics.Debug.WriteLine(cons);
                int bandera = isEmpyBodegaPasillo(m.idBodega, m.idPasillo, m.idEstante, m.idNivel);
                if (bandera != 0)
                {
                    if (!ubicacionDuplicada(m.idDetalleEntrada, m.idBodega, m.idPasillo, m.idEstante, m.idNivel))
                    {
                        if (m.idBodega != 0 && m.idPasillo != 0 && m.idEstante != 0 && m.idNivel != 0)
                        {
                            
                                string consulta = "EXEC sp_UpdateCantidad " + m.idDetalleEntrada + "," + m.cantidad + " ;";
                                System.Diagnostics.Debug.WriteLine(consulta);
                                if (conexion.ExcuteQuery(consulta))
                                {

                                    return RedirectToAction("DetalleEntradas", "Transaccion");

                                }

                            

                        }
                        else
                        {
                            ModelState.AddModelError("Error", "El id del bodega o pasillo no puede ser 0");

                        }

                    }
                    else
                    {
                        ModelState.AddModelError("Error", "Ya existe un producto en esa ubicacion");
                    }



                }
                else
                {
                    ModelState.AddModelError("Error", "No existe esa asigancion para la bodega, de pasillo, estante o nivel");

                }

            }
            DetalleEntrada detalle = getObjetDetalleEntrada(m.idDetalleEntrada);
            detalle.cantidad = cantidadREs;
            detalle.listaBodega = getListaBodega();
            detalle.listaPasillo = getListaPasillo();
            detalle.listaEstante = getListaEstante();
            detalle.listaNivel = getListNivelModel();

            return View(detalle);


        }

        [HttpPost]
        public ActionResult Salida(DetalleSalida model) {

            if (ModelState.IsValid)
            {
                DetalleEntrada entrada = getObjetDetalleEntrada(model.idDetalleEntrada);
                if (entrada != null)
                {
                    if (entrada.cantidad >= model.cantidad)
                    {
                        if (model.cantidad != 0)
                        {
                            double costoTotalSalida = model.cantidad * entrada.costoProudcto;
                            string consulta = "EXEC sp_AgregarSalida " + model.idDetalleEntrada + ",'" + model.cliente + "'," + model.cantidad + "," + costoTotalSalida + ";";
                            System.Diagnostics.Debug.WriteLine(consulta);
                            if (conexion.ExcuteQuery(consulta))
                            {
                                int cantidadRestante = entrada.cantidad - model.cantidad;
                                this.cantidadREs = cantidadRestante;
                                System.Diagnostics.Debug.WriteLine(cantidadREs);
                                return RedirectToAction("UpdateUbicacion", "Transaccion", new { @id = entrada.idDetalleEntrada, @otherParam = cantidadRestante });

                            }
                        }
                        else {

                            ModelState.AddModelError("Error", "No se puede realizar la salida de cero productos");
                        }
                        

                        

                    }
                    else {

                        ModelState.AddModelError("Error", "Cantidad supera el limite");
                    }

                }
                else 
                {
                    ModelState.AddModelError("Error", "No existe el objeto");

                }
                  
            
            }


            getListDetalleEntrada();
            DetalleSalida salida = new DetalleSalida();
            salida.listaDetalleEntrada = listaEntrada;
            return View(salida);
        }


        public List<detalleBodegaProducto> getListaDetalleBodegaProducto()
        {
            List<detalleBodegaProducto> lista = new List<detalleBodegaProducto>();
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
            return lista;

        }
        
        public void getListDetalleEntrada() {
            listaEntrada.Clear();
            string email = Session["Email"].ToString();
            string consulta = "select * from view_DetalleEntrada where email like '%" + email + "%';";
            DataTable dt = conexion.ShowDataByQuery(consulta);
            foreach (DataRow row in dt.Rows) {
                DetalleEntrada entrada = new DetalleEntrada();
                entrada.idDetalleEntrada = Convert.ToInt32(row["idDetalleEntrada"].ToString());
                entrada.proeveedor = row["provedoor"].ToString();
                entrada.idDetalle = Convert.ToInt32(row["idDetalle"].ToString());
                entrada.nombreProducto = row["producto"].ToString();
                entrada.cantidad = Convert.ToInt32(row["cantidad"].ToString());
                entrada.costoProudcto = Convert.ToDouble(row["costoProducto"].ToString());
                entrada.precioUnitario = Convert.ToDouble(row["precioUnitario"].ToString());
                entrada.costoTotal = Convert.ToDouble(row["costoTotal"].ToString());
                if (IsEmpy(row["idBodega"].ToString()) && IsEmpy(row["idPasillo"].ToString()) && IsEmpy(row["idEstante"].ToString()) && IsEmpy(row["idNivel"].ToString())) {
                    entrada.idBodega = 0;
                    entrada.idPasillo = 0;
                    entrada.idEstante = 0;
                    entrada.idNivel = 0;
                }
                else {
                    entrada.idBodega = Convert.ToInt32(row["idBodega"].ToString());
                    entrada.idPasillo = Convert.ToInt32(row["idPasillo"].ToString());
                    entrada.idEstante = Convert.ToInt32(row["idEstante"].ToString());
                    entrada.idNivel = Convert.ToInt32(row["idNivel"].ToString());
                }
                
                
                listaEntrada.Add(entrada);
            }

        }

        public Boolean ubicacionDuplicada(int id,int idBodega,int idPasillo,int idEstante,int idNivel) {
            this.getListDetalleEntrada();
            foreach (DetalleEntrada ent in listaEntrada) 
            {
                if (ent.idBodega == idBodega && ent.idPasillo == idPasillo && ent.idEstante == idEstante && ent.idNivel == idNivel && id != ent.idDetalleEntrada) {
                    return true;
                }
                
            }
            return false;
            
        }
        public bool IsEmpy(string s)
        {
            bool result;
            result = s == null || s == string.Empty;
            return result;
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            string consulta = "delete from detalleEntrada where idDetalleEntrada = " + id + " ;";
            if (conexion.ExcuteQuery(consulta))
            {
                return RedirectToAction("Index", "Transaccion");
            }
            return View();

        }

        public List<BodegaModel> getListaBodega()
        {
            List<BodegaModel> listaBodega = new List<BodegaModel>();
            string email = Session["Email"].ToString();
            string consulta = "select * from view_BodegaCliente where email like '" + email + "';";
            DataTable dt = conexion.ShowDataByQuery(consulta);
            foreach (DataRow row in dt.Rows)
            {
                BodegaModel bodega = new BodegaModel();
                bodega.idBodega = Convert.ToInt32(row["idBodega"].ToString());
                bodega.codigo = row["codigo"].ToString();
                bodega.nombre = row["nombre"].ToString();
                bodega.descripcion = row["descripcion"].ToString();
                bodega.direccion = row["direccion"].ToString();
                listaBodega.Add(bodega);
            }
            return listaBodega;

        }

        public List<PasilloModel> getListaPasillo() {
            List<PasilloModel> listaPasillo = new List<PasilloModel>();
            string email = Session["Email"].ToString();
            string consulta = "select * from view_PasilloBodega where email like '%" + email + "%' ;";

            DataTable dt = conexion.ShowDataByQuery(consulta);
            foreach (DataRow row in dt.Rows)
            {
                PasilloModel pasillo = new PasilloModel();
                pasillo.idPasillo = Convert.ToInt32(row["idPasillo"].ToString());
                pasillo.codigo = Convert.ToInt32(row["codigo"].ToString());
                pasillo.largo = Convert.ToInt32(row["largo"].ToString());
                pasillo.ancho = Convert.ToInt32(row["ancho"].ToString());
                pasillo.idBodega = row["codigoBodega"].ToString();
                pasillo.nombreBodega = row["nombre"].ToString();
                listaPasillo.Add(pasillo);
            }
            return listaPasillo;
        }
        public List<EstanteModel> getListaEstante() {
            List<EstanteModel> listaEstante = new List<EstanteModel>();
            string email = Session["Email"].ToString();
            string consulta = "select * from view_EstantePasillo where email like '%" + email + "%' ;";
            System.Diagnostics.Debug.WriteLine(consulta);
            DataTable dt = conexion.ShowDataByQuery(consulta);
            foreach (DataRow row in dt.Rows)
            {
                EstanteModel es = new EstanteModel();
                es.idEstante = Convert.ToInt32(row["idEstante"].ToString());
                es.codigo = row["codigo"].ToString();
                es.largo = Convert.ToInt32(row["largo"].ToString());
                es.ancho = Convert.ToInt32(row["ancho"].ToString());
                es.idPasillo = Convert.ToInt32(row["codigoPasillo"].ToString());
                listaEstante.Add(es);

            }
            return listaEstante;

        }

        public List<NivelModel> getListNivelModel() {
            List<NivelModel> listaNivel = new List<NivelModel>();
            string email = Session["Email"].ToString();
            string consulta = "select * from view_NivelEstante where email like '%" + email + "%' ;";
            System.Diagnostics.Debug.WriteLine(consulta);
            DataTable dt = conexion.ShowDataByQuery(consulta);
            foreach (DataRow row in dt.Rows)
            {
                NivelModel niv = new NivelModel();
                niv.idNivel = Convert.ToInt32(row["idNivel"].ToString());
                niv.codigo = Convert.ToInt32(row["codigo"].ToString());
                niv.idEstante = row["codigoEstante"].ToString();
                niv.ancho = Convert.ToInt32(row["ancho"].ToString());
                niv.largo = Convert.ToInt32(row["largo"].ToString());
                listaNivel.Add(niv);
            }
            return listaNivel;

        }

        public DetalleEntrada getObjetDetalleEntrada(int id) {
            getListDetalleEntrada();
            foreach (DetalleEntrada detalle in listaEntrada)
            {
                if (detalle.idDetalleEntrada == id) {
                    return detalle;
                
                
                }
            
            }
            return null;
        
        }


        public int isEmpyBodegaPasillo(int idBodega, int idPasillo, int idEstante, int idNivel)
        {
            string consulta = "select * from view_AsignacionBPEN where idBodega = "+idBodega+" and idPasillo = "+idPasillo+" and idEstante = "+idEstante+" and idNivel = "+idNivel+" ;";
            System.Diagnostics.Debug.WriteLine(consulta);
            DataTable dt = conexion.ShowDataByQuery(consulta);
            int id = 0;
            foreach (DataRow row in dt.Rows) {
                if (IsEmpy(row["idNivel"].ToString())) {
                    id = Convert.ToInt32(row["idNivel"].ToString());
                    System.Diagnostics.Debug.WriteLine("id en el isEmpy"+id);
                }
                else { 
                    id = Convert.ToInt32(row["idNivel"].ToString());
                    System.Diagnostics.Debug.WriteLine("id en else" + id);

                }
            
            
            }
            return id;

        }

    }
}
