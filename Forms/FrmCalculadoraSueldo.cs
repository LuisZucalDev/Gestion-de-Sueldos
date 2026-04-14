using System;
using System.Globalization;
using System.Threading;
using System.Windows.Forms;
using GestionSueldos.Models;
using GestionSueldos.Repositories;
using GestionSueldos.Services;

namespace GestionSueldos.Forms
{
    public partial class FrmCalculadoraSueldo : BaseForm
    {
        private readonly SalaryCalculatorService _calculadoraService;
        private readonly ISueldoRepository       _sueldoRepo;
        private ResultadoSueldo                  _ultimoResultado;

        public FrmCalculadoraSueldo()
        {
            InitializeComponent();
            _calculadoraService = new SalaryCalculatorService();
            _sueldoRepo         = new SueldoFileRepository();

            // Poblar combos desde el servicio
            foreach (string afp in _calculadoraService.ObtenerNombresAFP())
                cmbAFP.Items.Add(afp);

            foreach (string salud in _calculadoraService.ObtenerNombresSalud())
                cmbSalud.Items.Add(salud);
        }

        private void btnCalcular_Click(object sender, EventArgs e)
        {
            if (!ValidarEntradas()) return;

            try
            {
                decimal horas  = decimal.Parse(txtHorasTrabajadas.Text, CultureInfo.InvariantCulture);
                string extrasStr = txtHorasExtras.Text.Trim();
                decimal extras = string.IsNullOrEmpty(extrasStr) ? 0 : decimal.Parse(extrasStr, CultureInfo.InvariantCulture);

                _ultimoResultado = _calculadoraService.Calcular(
                    horas, extras,
                    cmbAFP.SelectedItem.ToString(),
                    cmbSalud.SelectedItem.ToString()
                );

                // Configurar cultura para mostrar pesos chilenos
                Thread.CurrentThread.CurrentCulture = new CultureInfo("es-CL");

                lblValorBruto.Text    = _ultimoResultado.SueldoBruto.ToString("C");
                lblValorAFP.Text      = _ultimoResultado.DescuentoAFP.ToString("C");
                lblValorSalud.Text    = _ultimoResultado.DescuentoSalud.ToString("C");
                lblValorLiquido.Text  = _ultimoResultado.SueldoLiquido.ToString("C");

                pnlResultado.Visible = true;
                btnGuardar.Enabled   = true;
                btnListar.Enabled    = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al calcular: " + ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (_ultimoResultado == null)
            {
                MessageBox.Show("Primero debes calcular el sueldo.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _sueldoRepo.Guardar(_ultimoResultado);
            MessageBox.Show("Sueldo guardado exitosamente en 'informacion_sueldo.txt'.",
                "Guardado", MessageBoxButtons.OK, MessageBoxIcon.Information);
            btnGuardar.Enabled = false;
        }

        private void btnListar_Click(object sender, EventArgs e)
        {
            new FrmListarTrabajadores().ShowDialog(this);
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtHorasTrabajadas.Clear();
            txtHorasExtras.Clear();
            cmbAFP.SelectedIndex   = -1;
            cmbSalud.SelectedIndex = -1;
            pnlResultado.Visible   = false;
            btnGuardar.Enabled     = false;
            btnListar.Enabled      = false;
            _ultimoResultado       = null;
        }

        private bool ValidarEntradas()
        {
            if (!decimal.TryParse(txtHorasTrabajadas.Text, out decimal horas) || horas < 0)
            {
                MessageBox.Show("Ingresa un número válido de horas trabajadas.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtHorasTrabajadas.Focus();
                return false;
            }

            string extrasStr = txtHorasExtras.Text.Trim();
            if (!string.IsNullOrEmpty(extrasStr) && (!decimal.TryParse(extrasStr, out decimal extras) || extras < 0))
            {
                MessageBox.Show("Ingresa un número válido de horas extras.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtHorasExtras.Focus();
                return false;
            }

            if (cmbAFP.SelectedIndex < 0)
            {
                MessageBox.Show("Selecciona un sistema AFP.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (cmbSalud.SelectedIndex < 0)
            {
                MessageBox.Show("Selecciona un sistema de salud.", "Validación",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }
    }
}
