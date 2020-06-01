using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using F2_201801351.Models;
using F2_201801351.ConnectionDB;
using System.Data;

namespace F2_201801351.Controllers
{
    [Authorize(Roles = "Inventario")]
    public class BodegaController : Controller
    {
        SQLConexion conexion = new SQLConexion();
        List<BodegaModel> listaBodega = new List<BodegaModel>();
        List<PasilloModel> listaPasillo = new List<PasilloModel>();
        List<EstanteModel> listaEstante = new List<EstanteModel>();
        List<NivelModel> listaNivel = new List<NivelModel>();
        
        public ActionResult Index()
        {
            
            return View();
        }

        public ActionResult New()
        {

            return View();
        }

        [HttpPost]
        public ActionResult New(BodegaModel m) 
        {
            if (ModelState.IsValid) { 
                string email = Session["Email"].ToString();
                string consulta = "EXEC sp_AgregarBodega '"+email+ "','"+m.codigo + "','" +m.nombre + "','" +m.descripcion + "','" +m.direccion + "' ;";
                System.Diagnostics.Debug.WriteLine(consulta);
                if (conexion.ExcuteQuery(consulta))
                {
                    return RedirectToAction("Index", "Bodega");

                }
               


            }

            return View();
        }

        public ActionResult NewPasillo() {
            return View();  
        }

        [HttpPost]
        public ActionResult NewPasillo(PasilloModel modelo)
        {
            if (ModelState.IsValid) {
                int idBodega = getIdBodega(modelo.idBodega);
                if (idBodega != 0)
                {
                    string consulta = "EXEC sp_AgregarPasillo " + idBodega + "," + modelo.codigo + "," + modelo.largo + "," + modelo.ancho + " ;";
                    System.Diagnostics.Debug.WriteLine(consulta);
                    if (conexion.ExcuteQuery(consulta))
                    {
                        return RedirectToAction("Index", "Bodega");

                    }
                }
                else {
                    ModelState.AddModelError("Error", "No existe el codigo  de la bodega");

                }

            
            }
            return View();
        }


        public ActionResult NewEstante() {

            return View();
        }
        [HttpPost]
        public ActionResult NewEstante(EstanteModel model) {
            
            if (ModelState.IsValid) {
                int idPasillo = getIdPasillo(model.idPasillo.ToString());
                if (idPasillo != 0)
                {
                    string consulta = "EXEC sp_AgregarEstante " + idPasillo + "," + model.codigo + "," + model.largo + "," + model.ancho + " ;";
                    System.Diagnostics.Debug.WriteLine(consulta);
                    if (conexion.ExcuteQuery(consulta))
                    {
                        return RedirectToAction("Index", "Bodega");

                    }

                }
                else {
                    ModelState.AddModelError("Error", "No existe el codigo del pasillo");
                }

            }
            return View();
        }

        public ActionResult NewNivel() {
            return View();
        }
        [HttpPost]
        public ActionResult NewNivel(NivelModel model) {
            if (ModelState.IsValid) {
                int idEstante = getIdEstante(model.idEstante);
                if (idEstante != 0)
                {
                    string consulta = "EXEC sp_AgregarNivel " + idEstante + ",'" + model.codigo + "';";
                    System.Diagnostics.Debug.WriteLine(consulta);
                    if (conexion.ExcuteQuery(consulta))
                    {
                        return RedirectToAction("Index", "Bodega");

                    }
                }
                else {
                    ModelState.AddModelError("Error", "No existe el codigo del Estaten");
                }
            }
            return View();
        }


        public bool IsEmpy(string s)
        {
            bool result;
            result = s == null || s == string.Empty;
            return result;
        }
       

        public ActionResult ViewBodega() {
            getListaBodega();

            return View(listaBodega);
        
        }

        public ActionResult ViewPasillo() {
            //getListaPasillo();

            //return View(listaPasillo);
            return View();
        }

        public ActionResult ViewEstante() {
            getListaEstante();

            return View(listaEstante);
        }

        public ActionResult ViewNivel() {
            getListaNivel();
            return View(listaNivel);
        }

        [HttpPost]
        public ActionResult ViewPasillo(string parametro) {
            if (ModelState.IsValid)
            {
                if (!IsEmpy(parametro))
                {
                    System.Diagnostics.Debug.WriteLine(parametro);
                    getListaPasillo(parametro);
                    return View(listaPasillo);
                }
            }
            
            
            return View();
        }
        public void getListaPasillo()
        {

            listaPasillo.Clear();
            string email = Session["Email"].ToString();
            string consulta = "select * from view_PasilloBodega where email like '%"+email+"%' ;";
            
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




        }
        


        public void getListaPasillo(string parametro) {

            listaPasillo.Clear();
            string email = Session["Email"].ToString();
            string consulta = "";
            if (isInteger(parametro) == true) {
                int idBodega = getIdBodega(parametro);
                consulta = "select * from view_PasilloBodega where idBodega = " + idBodega + " and email like '%"+email+"%';";
            }
            else
            {
                consulta = "select * from view_PasilloBodega where nombre like '%" + parametro + "%' and email like '%"+email+"%';";
            }

            System.Diagnostics.Debug.WriteLine(consulta);
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




        }
        public void getListaEstante() {
            listaEstante.Clear();
            string email = Session["Email"].ToString();
            string consulta = "select * from view_EstantePasillo where email like '%"+email+"%' ;";
            System.Diagnostics.Debug.WriteLine(consulta);
            DataTable dt = conexion.ShowDataByQuery(consulta);
            foreach (DataRow row in dt.Rows) {
                EstanteModel es = new EstanteModel();
                es.idEstante = Convert.ToInt32(row["idEstante"].ToString());
                es.codigo = row["codigo"].ToString();
                es.largo = Convert.ToInt32(row["largo"].ToString());
                es.ancho = Convert.ToInt32(row["ancho"].ToString());
                es.idPasillo = Convert.ToInt32(row["codigoPasillo"].ToString());
                listaEstante.Add(es);

            }

        }

        public void getListaNivel() {
            listaNivel.Clear();
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

        }



        public void getListaBodega() {
            listaBodega.Clear();
            string email = Session["Email"].ToString();
            string consulta = "select * from view_BodegaCliente where email like '" + email + "';";
            DataTable dt = conexion.ShowDataByQuery(consulta);
            foreach (DataRow row in dt.Rows) {
                BodegaModel bodega = new BodegaModel();
                bodega.idBodega = Convert.ToInt32(row["idBodega"].ToString());
                bodega.codigo = row["codigo"].ToString();
                bodega.nombre = row["nombre"].ToString();
                bodega.descripcion = row["descripcion"].ToString();
                bodega.direccion = row["direccion"].ToString();
                listaBodega.Add(bodega);
            }

        }
        public int getIdPasillo(string codigo)
        {
            getListaPasillo();
            foreach (PasilloModel pasillo in listaPasillo)
            {
                if (pasillo.codigo.Equals(codigo) || pasillo.codigo == Convert.ToInt32(codigo))
                {
                    return pasillo.idPasillo;

                }


            }
            return 0;

        }

        public int getIdEstante(string codigo)
        {
            getListaEstante();
            foreach (EstanteModel estante in listaEstante)
            {
                if (estante.codigo.Equals(codigo))
                {
                    return estante.idEstante;

                }


            }
            return 0;

        }

        public int getIdBodega(string codigo) {
            getListaBodega();
            foreach (BodegaModel bodega in listaBodega) {
                if (bodega.codigo.Equals(codigo)) {
                    return bodega.idBodega;
                
                }
            
            
            }
            return 0;
        
        }


        public Boolean isInteger(string data) {
            try {
                Convert.ToInt32(data);
                return true;
            } catch (Exception ex) {
                return false;
            }
        
        
        }


    }
}
