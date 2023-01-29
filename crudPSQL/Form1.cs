using Microsoft.VisualBasic.ApplicationServices;

namespace crudPSQL
{
    public partial class Form1 : Form
    {
        private List<Usuarios> ListUser;
        private Usuarios usuario;
        private Crud Consultas;
        public Form1()
        {
            InitializeComponent();
            ListUser = new List<Usuarios>();
            Consultas = new Crud();
            usuario = new Usuarios();
            CargarDataGridView();
        }
        private int ObtenerId()
        {
            if (!this.txtId.Text.Trim().Equals(""))
            {
                if (int.TryParse(this.txtId.Text.Trim(), out int id))
                {
                    return id;
                }
                else return -1;
            }
            else return -1;
        }
        private bool ValidarDatos()
        {
            if (this.txtNombre.Text.Trim().Equals(""))
            {
                MessageBox.Show("Ingrese el Nombre.");
                return false;
            }
            if (this.txtCorreo.Text.Trim().Equals(""))
            {
                MessageBox.Show("Ingrese el Correo.");
                return false;
            }
            return true;
        }
        private void LimpiarCampos()
        {
            this.txtId.Clear();
            this.txtNombre.Clear();
            this.txtCorreo.Clear();
        }
        private void CargarTextBox()
        {
            this.usuario.id = ObtenerId();
            this.usuario.name = this.txtNombre.Text.Trim();
            this.usuario.email = this.txtCorreo.Text.Trim();
        }
        private void ValidarLetras(object sender, KeyPressEventArgs e)
        {
            if (!char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.IsWhiteSpace(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        private void CargarDataGridView()
        {
            this.tabla.Rows.Clear();
            this.tabla.Refresh();
            this.ListUser.Clear();
            this.ListUser = Consultas.GetUser();
            for (int i = 0; i < ListUser.Count; i++)
            {
                this.tabla.Rows.Add(
                    ListUser[i].id,
                    ListUser[i].name,
                    ListUser[i].email
                    );
            }
        }
        private void txtId_KeyPress(object sender, KeyPressEventArgs e)
        {
            ValidarLetras(sender, e);
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            try
            {
                if (!ValidarDatos())
                {
                    return;
                }
                CargarTextBox();
                if (Consultas.AddUser(usuario))
                {
                    MessageBox.Show("Usuario Registrado.");
                    CargarDataGridView();
                    LimpiarCampos();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error " + ex.ToString());
            }
        }
    }
}