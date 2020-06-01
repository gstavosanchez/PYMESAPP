using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using F2_201801351.Models;
using F2_201801351.ConnectionDB;
using System.Web.Security;

namespace F2_201801351.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {

            LoginModel objeLoginModel = new LoginModel();

            return View(objeLoginModel);
        }
        [HttpPost]
        public ActionResult Login(LoginModel model) {

            if (ModelState.IsValid) {
                Boolean validacion = Validar(model.email, model.password);
                if (validacion != false)
                {
                    System.Diagnostics.Debug.WriteLine("Bienvenido al sistema");
                    Session["Email"] = model.email;
                    FormsAuthentication.SetAuthCookie(model.email,false);
                    return RedirectToAction("Index", "Producto");
                }
                else {
                    System.Diagnostics.Debug.WriteLine("Erro en la contrasena o usuario");
                    ModelState.AddModelError("Error", "Email o Password is not valid");
                }
            }


            return View();
        }

        public ActionResult Edit() {

            UsuarioModel model = new UsuarioModel();
            string correo = Session["Email"].ToString();
            string consulta = "select * from usuario where nick like '%" + correo + "%'";
            SQLConexion conexion = new SQLConexion();
            DataTable dt = conexion.ShowDataByQuery(consulta);
            foreach (DataRow row in dt.Rows) {
                model.id = Convert.ToInt32(row["idUsuario"].ToString());
                model.nombre = row["nombre"].ToString();
                model.apellido = row["apellido"].ToString();
                model.email = row["nick"].ToString();
                model.password = row["contrasena"].ToString();
                model.confiPassword = row["contrasena"].ToString();

            }

            return View(model);

        }

        [HttpPost]
        public ActionResult Edit(UsuarioModel m)
        {
            if (ModelState.IsValid) {
                
                string consulta = "update Usuario set  nombre = '"+m.nombre+ "',apellido = '"+m.apellido+ "',nick = '"+m.email+ "',contrasena = '"+m.password+ "' where idUsuario = "+m.id+";";
                SQLConexion conexion = new SQLConexion();
                if (conexion.ExcuteQuery(consulta)) {
                    Session["Email"] = m.email;
                    return RedirectToAction("Index", "Producto");

                }

            
            
            }
            

            return View();

        }


        public ActionResult Register() {
            UsuarioModel model = new UsuarioModel();

            return View(model);
        }

        [HttpPost]
        public ActionResult Register(UsuarioModel m) {
            if (ModelState.IsValid) {
                SQLConexion conexion = new SQLConexion();
                if (!dataDuplicate(m.email, "select p.nick as email from Usuario p;"))
                {
                    string consulta = "EXEC sp_AgregarUsuarioDefualt '" + m.nombre + "','" + m.apellido + "','" + m.email + "','" + m.password + "';";

                    conexion.ExcuteQuery(consulta);

                    return RedirectToAction("Login", "Account");


                }
                else 
                {
                    ModelState.AddModelError("Error", "Ya existe el correo");

                }
                



            }

            return View();
        
        }

        public ActionResult Logout() 
        {
            
            Session.Abandon();
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");


        }


        public Boolean Validar(string email,string contrasena) {
            SQLConexion conexion = new SQLConexion();
            string consulta = "select * from Usuario where nick like '" + email + "' and contrasena like '" + contrasena + "'";
            DataTable dt = conexion.ShowDataByQuery(consulta);
            string emailVerificado = "";
            string contrasenaVerificado = "";
            foreach (DataRow row in dt.Rows)
            {
                emailVerificado = row["nick"].ToString();
                contrasenaVerificado = row["contrasena"].ToString();


            }

            if (!IsEmpy(emailVerificado) && !IsEmpy(contrasenaVerificado))
            {

                return true;
            }
            else {
                return false;
            }
            
            
        }

        public bool IsEmpy(string s)
        {
            bool result;
            result = s == null || s == string.Empty;
            return result;
        }

        public Boolean dataDuplicate(string email,string consulta) {
            SQLConexion conexion = new SQLConexion();
            DataTable dt = conexion.ShowDataByQuery(consulta);
            foreach (DataRow row in dt.Rows) { 
                string parametro = row["email"].ToString();
                if (email == parametro) {
                    return true;
                }

            }

            return false;
        }


    }
}
