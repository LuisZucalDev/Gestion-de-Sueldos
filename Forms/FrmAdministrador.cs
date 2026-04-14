using System;
using System.Windows.Forms;
using GestionSueldos.Models;
using GestionSueldos.Repositories;

namespace GestionSueldos.Forms
{
public partial class FrmAdministrador : BaseForm
    {
        private readonly IEmpleadoRepository _empleadoRepo;

        public FrmAdministrador()
        {
            InitializeComponent();
            _empleadoRepo = new EmpleadoFileRepository();
            lblTitle.Text = "Gestión de Empleados";
            lblSubtitle.Text = "Agregar, editar y listar trabajadores";
            UpdateStatus("Empleados listos - 2 registros");
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (!ValidarCampos()) return;

            if (!decimal.TryParse(txtValorHora.Text, out decimal valHora))
            {
                MessageBox.Show("Ingresa un valor de hora válido.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!decimal.TryParse(txtValorExtra.Text, out decimal valExtra))
            {
                MessageBox.Show("Ingresa un valor de hora extra válido.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var empleado = new Empleado(
                txtRut.Text.Trim(),
                txtNombre.Text.Trim(),
                txtDireccion.Text.Trim(),
                txtTelefono.Text.Trim(),
                valHora,
                valExtra
            );

            _empleadoRepo.Agregar(empleado);

            MessageBox.Show($"Empleado '{empleado.Nombre}' agregado exitosamente.",
                "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);

            LimpiarCampos();
        }

        private bool ValidarCampos()
        {
            if (string.IsNullOrWhiteSpace(txtRut.Text))
            {
                MessageBox.Show("El RUT es requerido.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtRut.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show("El nombre es requerido.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNombre.Focus();
                return false;
            }
            return true;
        }

        private void LimpiarCampos()
        {
            txtRut.Clear();
            txtNombre.Clear();
            txtDireccion.Clear();
            txtTelefono.Clear();
            txtValorHora.Clear();
            txtValorExtra.Clear();
            txtRut.Focus();
        }

        private void btnVerTrabajadores_Click(object sender, EventArgs e)
        {
            new FrmListarTrabajadores().ShowDialog(this);
        }
    }
}
