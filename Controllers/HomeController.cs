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
    public class HomeController : Controller
    {
        SQLConexion conexion = new SQLConexion();

        // GET: Home
        public ActionResult Index()
        {
            return View(getListaPrecio());
        }

        public ActionResult New(int id) {

            ClienteModel modelo = new ClienteModel();
            string consulta = "SELECT * FROM view_listadoPreciosModulo WHERE idLista = " + id;
            DataTable dt = conexion.ShowDataByQuery(consulta);
            foreach (DataRow row in dt.Rows)
            {
             modelo.tamano = row["rangoUsuarios"].ToString();

            }
            modelo.idCliente = id;
            
            return View(modelo);
        }
        [HttpPost]
        public ActionResult New(ClienteModel clienteModel) {

            if (ModelState.IsValid) {
                int idLi = clienteModel.idCliente;

                string consulta = "SELECT * FROM view_listadoPreciosModulo WHERE idLista = " + idLi;
                DataTable dt = conexion.ShowDataByQuery(consulta);
                string tamano = "";
               
                foreach (DataRow row in dt.Rows)
                {
                    tamano = row["rangoUsuarios"].ToString();
                }
                if (clienteModel.tamano == tamano)
                {
                    string consultaDos = "EXEC sp_AgregarCliente "+idLi+",'"+clienteModel.nombre+"','"+clienteModel.nit+"','"+clienteModel.tipoEmpresa+"',"+tamano+",'"+clienteModel.noTarjeta+"','"+clienteModel.nombreTarjeta+"','"+clienteModel.tipoTarjeta+"','"+clienteModel.CRVTarjeta+"';";
                    System.Diagnostics.Debug.WriteLine(consultaDos);
                    conexion.ExcuteQuery(consultaDos);
                    return RedirectToAction("Create", "Home");


                }
                else {
                    return View();
                }
                


                
            }
            

            return View();
        }
        public ActionResult Create()
        {
            /*List<tipoContactoModel> lista = new List<tipoContactoModel>();
            string consulta = "select * from TipoContacto;";
            DataTable dt = conexion.ShowDataByQuery(consulta);
            foreach (DataRow row in dt.Rows)
            {
                tipoContactoModel obj = new tipoContactoModel();
                obj.id = Int32.Parse(row["idTipo"].ToString());
                obj.nombre = row["nombre"].ToString();
                System.Diagnostics.Debug.WriteLine(obj.nombre);

                lista.Add(obj);
                
                    
                
            }
            return View(lista);*/
            return View();
        }

        [HttpPost]
        public ActionResult Create(ContactoModel mo)
        {
            string comercial = "Comercial";
            string admin = "Admistrador"; 
            string gerencial = "Gerencial";
            if (ModelState.IsValid)
            {
                if (!dataDuplicate(mo.emailAdmin, "select p.nick as email from Usuario p;"))
                {
                    string consultaComercial = "EXEC sp_AgregarContactoMultiple '" + mo.nombreComercial + "','" + mo.apellidoComercial + "','" + mo.emailComercial + "','" + comercial + "'";
                    string consultaGerencial = "EXEC sp_AgregarContactoMultiple '" + mo.nombreGerencial + "','" + mo.apellidoGerencial + "','" + mo.emailGerencial + "','" + gerencial + "'";
                    string consultaAdmin = "EXEC sp_AgregarContactoMultiple '" + mo.nombreAdministradro + "','" + mo.apellidoAdministradro + "','" + mo.emailAdmin + "','" + admin + "'";
                    string consultaUsuario = "EXEC sp_AgregarUsuario '" + mo.nombreAdministradro + "','" + mo.apellidoAdministradro + "','" + mo.emailAdmin + "';";

                    System.Diagnostics.Debug.WriteLine(consultaComercial);
                    conexion.ExcuteQuery(consultaComercial);

                    System.Diagnostics.Debug.WriteLine(consultaGerencial);
                    conexion.ExcuteQuery(consultaGerencial);

                    System.Diagnostics.Debug.WriteLine(consultaAdmin);
                    conexion.ExcuteQuery(consultaAdmin);

                    System.Diagnostics.Debug.WriteLine(consultaUsuario);
                    conexion.ExcuteQuery(consultaUsuario);

                    mo.SuccessMessage = "Se agrego el conctacto exitasamente " +
                        "Usuario:" + mo.emailAdmin + " Password: default";
                    ModelState.AddModelError("Error", mo.SuccessMessage);

                    Session["Email"] = mo.emailAdmin;
                    return RedirectToAction("Edit", "Account");


                }
                else {
                    ModelState.AddModelError("Error", "Error el email ya existe:"+mo.emailAdmin+". Intente con otro");
                }

            }
            else {
                ModelState.AddModelError("Error", "Error en la creacion");
                
                

            }
            
            
            return View();
        }



        public List<ListaPrecioModel> getListaPrecio()
        {
            List<ListaPrecioModel> listPrecioModel = new List<ListaPrecioModel>();
            SQLConexion dbConexion = new SQLConexion();
            string consulta = "SELECT * FROM view_listadoPreciosModulo;";
            DataTable dt = dbConexion.ShowDataByQuery(consulta);
            foreach (DataRow row in dt.Rows)
            {
                ListaPrecioModel nuevo = new ListaPrecioModel();
                nuevo.idModulo = Int32.Parse(row["idModulo"].ToString());
                nuevo.nombre = row["nombre"].ToString();
                nuevo.abreviatura = row["abreviatura"].ToString();
                nuevo.descripcion = row["descripcion"].ToString();
                nuevo.idLista = Int32.Parse(row["idLista"].ToString());
                nuevo.fechaInicio = row["fechaInicio"].ToString();
                nuevo.fechaFin = row["fechaFin"].ToString();
                nuevo.suscripcion = row["suscripcion"].ToString();
                nuevo.ragoUsuario = Int32.Parse(row["rangoUsuarios"].ToString());
                nuevo.precio = Convert.ToDouble(row["precio"].ToString());

                listPrecioModel.Add(nuevo);
            }

            return listPrecioModel;

        }

        public Boolean dataDuplicate(string email, string consulta)
        {
            SQLConexion conexion = new SQLConexion();
            DataTable dt = conexion.ShowDataByQuery(consulta);
            foreach (DataRow row in dt.Rows)
            {
                string parametro = row["email"].ToString();
                if (email == parametro)
                {
                    return true;
                }

            }

            return false;
        }

        [Authorize]
        public ActionResult Modulo() 
        {
            return View(getListModulo());    
        
        }

        public List<ModuloModel> getListModulo()
        {
            List<ModuloModel> listModuloModel = new List<ModuloModel>();
            SQLConexion dbConexion = new SQLConexion();
            string email = Session["Email"].ToString();
            string consulta = "SELECT * FROM view_AsignacionModulo where email like '%"+email+"%' ;";
            DataTable dt = dbConexion.ShowDataByQuery(consulta);
            foreach (DataRow row in dt.Rows)
            {
                ModuloModel nuevo = new ModuloModel();
                nuevo.id = Int32.Parse(row["idAsignacion"].ToString());
                nuevo.nombre = row["nombre"].ToString();
                nuevo.abreviatura = row["abreviatura"].ToString();
              

                listModuloModel.Add(nuevo);
            }

            return listModuloModel;

        }




    }
}
