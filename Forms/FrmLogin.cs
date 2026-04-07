using System;
using System.Windows.Forms;
using GestionSueldos.Repositories;
using GestionSueldos.Services;

namespace GestionSueldos.Forms
{
    public partial class FrmLogin : Form
    {
        private readonly AuthService           _authService;
        private readonly IEmpleadoRepository   _empleadoRepo;

        public FrmLogin()
        {
            InitializeComponent();
            _empleadoRepo = new EmpleadoFileRepository();
            _authService  = new AuthService(_empleadoRepo);
        }

        private void btnIniciarSesion_Click(object sender, EventArgs e)
        {
            string usuario   = txtUsuario.Text.Trim();
            string contrasena = txtContrasena.Text.Trim();

            if (string.IsNullOrWhiteSpace(usuario) || string.IsNullOrWhiteSpace(contrasena))
            {
                MostrarError("Por favor ingresa usuario y contraseña.");
                return;
            }

            AuthService.TipoUsuario tipo = _authService.ValidarCredenciales(usuario, contrasena);

            switch (tipo)
            {
                case AuthService.TipoUsuario.Administrador:
                    AbrirVentana(new FrmAdministrador());
                    break;

                case AuthService.TipoUsuario.UsuarioNormal:
                    AbrirVentana(new FrmCalculadoraSueldo());
                    break;

                default:
                    MostrarError("Credenciales incorrectas. Intenta nuevamente.");
                    break;
            }
        }

        private void AbrirVentana(Form form)
        {
            form.FormClosed += (s, args) => this.Show();
            this.Hide();
            form.Show();
        }

        private void MostrarError(string mensaje)
        {
            lblError.Text    = mensaje;
            lblError.Visible = true;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void txtContrasena_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnIniciarSesion_Click(sender, e);
        }
    }
}
