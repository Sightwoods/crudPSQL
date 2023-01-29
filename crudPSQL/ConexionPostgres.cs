using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;

namespace crudPSQL
{
    internal class ConexionPostgres : DatosConexion
    {
        private NpgsqlConnection conexion;
        private string cadena;
        public ConexionPostgres() {
            cadena = "" +
                "Server=" + Server +
                "; Port=" + Port +
                "; User Id=" + UserId +
                "; Password=" + Password +
                "; Database="+ Database +";";
            conexion = new NpgsqlConnection(cadena);
        }
        public NpgsqlConnection GetConnection()
        {
            try
            {
                if (conexion.State != System.Data.ConnectionState.Open)
                {
                    conexion.Open();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error " + ex.ToString());
            }
            return conexion;
        }
    }
}
