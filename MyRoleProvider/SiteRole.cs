using F2_201801351.ConnectionDB;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace F2_201801351.MyRoleProvider
{
    public class SiteRole : RoleProvider
    {
        public override string ApplicationName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] GetRolesForUser(string username)
        {
            
            SQLConexion conexion = new SQLConexion();
            string consulta = "select * from view_emailCompra where email like '%"+username+"%'";
            DataTable dt = conexion.ShowDataByQuery(consulta);
            string[] resulset = new string[(dt.Rows.Count)];
            int contador = 0;
            foreach (DataRow row in dt.Rows) { 
                string campo = row["abreviatura"].ToString();
                resulset[contador] = campo;
                contador++;
            }
            
            return resulset;
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}