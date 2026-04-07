using System;
using System.Collections.Generic;
using System.Windows.Forms;
using GestionSueldos.Models;
using GestionSueldos.Repositories;

namespace GestionSueldos.Forms
{
    public partial class FrmListarTrabajadores : Form
    {
        private readonly IEmpleadoRepository _empleadoRepo;
        private readonly ISueldoRepository   _sueldoRepo;
        private          List<Empleado>      _empleados;

        public FrmListarTrabajadores()
        {
            InitializeComponent();
            _empleadoRepo = new EmpleadoFileRepository();
            _sueldoRepo   = new SueldoFileRepository();
            CargarDatos();
        }

        private void CargarDatos()
        {
            _empleados = new List<Empleado>(_empleadoRepo.ObtenerTodos());
            ResultadoSueldo ultimoSueldo = _sueldoRepo.ObtenerUltimo();

            dgv.Rows.Clear();

            for (int i = 0; i < _empleados.Count; i++)
            {
                Empleado emp = _empleados[i];
                string sueldo = (ultimoSueldo != null && i == _empleados.Count - 1)
                    ? ultimoSueldo.SueldoLiquido.ToString("C", new System.Globalization.CultureInfo("es-CL"))
                    : "No disponible";

                dgv.Rows.Add(emp.Rut, emp.Nombre, emp.Direccion, emp.Telefono, sueldo);
            }

            lblConteo.Text = $"{_empleados.Count} empleado(s) registrado(s)";
        }

        private void dgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            // Columna Eliminar (índice 5)
            if (e.ColumnIndex == dgv.Columns["colEliminar"].Index)
            {
                string rut    = dgv.Rows[e.RowIndex].Cells["colRut"].Value?.ToString();
                string nombre = dgv.Rows[e.RowIndex].Cells["colNombre"].Value?.ToString();

                DialogResult res = MessageBox.Show(
                    $"¿Eliminar al empleado '{nombre}' ({rut})?",
                    "Confirmar eliminación",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning);

                if (res == DialogResult.Yes)
                {
                    _empleadoRepo.Eliminar(rut);
                    CargarDatos();
                }
            }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
