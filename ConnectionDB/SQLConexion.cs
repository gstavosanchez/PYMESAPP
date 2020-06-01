using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace F2_201801351.ConnectionDB
{
    public class SQLConexion
    {

        string cadena = "Data Source=DESKTOP-UV0R28R;Initial Catalog=PYMESAPP;Integrated Security=True";
        public SqlConnection conectar = new SqlConnection();
        private SqlCommand comando;
        private SqlDataReader lector;
        private SqlDataAdapter da;
        private DataTable dt;
        public SQLConexion()
        {
            conectar.ConnectionString = cadena;
        }

        public void connect()
        {
            try
            {
                conectar.Open();
                Console.WriteLine("Conexion Exitosa");
                System.Diagnostics.Debug.WriteLine("Conexion Exitosa");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error en la conexion en la DB: " + ex);
                System.Diagnostics.Debug.WriteLine("Error en la conexion en la DB: " + ex);
            }
        }
        public void disconnect()
        {
            conectar.Close();
        }

        public DataTable ShowDataByQuery(string query)
        {
            dt = new DataTable();
            connect();
            try
            {
                //comando = new SqlCommand(cadena,conectar);
                //lector = comando.ExecuteReader();
                da = new SqlDataAdapter(query, conectar);
                da.Fill(dt);

            }
            catch (Exception ex)
            {

                Console.WriteLine("Error:" + ex);
                System.Diagnostics.Debug.WriteLine("Error:" + ex);
            }
            finally
            {
                disconnect();

            }
            return dt;

        }

        public Boolean ExcuteQuery(string query)
        {
            Boolean estado = false;
            try
            {
                connect();
                comando = new SqlCommand(query, conectar);
                comando.ExecuteNonQuery();
                estado = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error:" + ex);
                System.Diagnostics.Debug.WriteLine("Error:");
                estado = false;
            }
            finally
            {
                disconnect();
            }
            return estado;
            

        }

    }
}