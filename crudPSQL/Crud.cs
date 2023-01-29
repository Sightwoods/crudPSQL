using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace crudPSQL
{
    internal class Crud
    {
        private ConexionPostgres conexion;
        private List<Usuarios> usuarios;
        public Crud() {
            conexion = new ConexionPostgres();
            usuarios = new List<Usuarios>();
        }
        public List<Usuarios> GetUser()
        {   
            try
            {
                string Query = "SELECT * FROM get_all_users('users');";
                NpgsqlDataReader Reader = null;
                NpgsqlCommand command = new NpgsqlCommand(Query);
                command.Connection = conexion.GetConnection();
                Reader = command.ExecuteReader();
                Usuarios DGV_user = null;
                while (Reader.Read())
                {
                    DGV_user = new Usuarios();
                    DGV_user.id = Reader.GetInt32("Id");
                    DGV_user.name = Reader.GetString("name");
                    DGV_user.email = Reader.GetString("email");
                    usuarios.Add(DGV_user);
                }
                Reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error " + ex.ToString());
            }
            return usuarios;
        }
        internal bool AddUser(Usuarios usuario)
        {
            string insert = "SELECT insert_users(@name,@email)";
            NpgsqlCommand command = new NpgsqlCommand(insert, conexion.GetConnection());
            command.Parameters.Add(new NpgsqlParameter("@name", usuario.name));
            command.Parameters.Add(new NpgsqlParameter("@email", usuario.email));
            return command.ExecuteNonQuery() > 0;
        }
    }
}
