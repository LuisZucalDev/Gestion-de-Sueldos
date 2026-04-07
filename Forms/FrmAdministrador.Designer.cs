using System.Drawing;
using System.Windows.Forms;
using GestionSueldos.UI;

namespace GestionSueldos.Forms
{
    partial class FrmAdministrador
    {
        private System.ComponentModel.IContainer components = null;

        private Panel        pnlHeader;
        private Label        lblHeaderTitle;
        private Label        lblHeaderSub;
        private Panel        pnlForm;
        private Label        lblRutLbl;
        private TextBox      txtRut;
        private Label        lblNombreLbl;
        private TextBox      txtNombre;
        private Label        lblDireccionLbl;
        private TextBox      txtDireccion;
        private Label        lblTelefonoLbl;
        private TextBox      txtTelefono;
        private Label        lblValorHoraLbl;
        private TextBox      txtValorHora;
        private Label        lblValorExtraLbl;
        private TextBox      txtValorExtra;
        private ModernButton btnAgregar;
        private ModernButton btnVerTrabajadores;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();

            // ── Formulario ──────────────────────────────────────────────────────
            this.Text            = "Sueldo Pro — Panel de Administrador";
            this.ClientSize      = new Size(560, 560);
            this.BackColor       = AppTheme.BgDark;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox     = false;
            this.StartPosition   = FormStartPosition.CenterScreen;
            this.Font            = AppTheme.FontLabel;

            // ── Header ──────────────────────────────────────────────────────────
            pnlHeader = new Panel
            {
                Size      = new Size(560, 70),
                Location  = new Point(0, 0),
                BackColor = AppTheme.BgCard,
            };
            pnlHeader.Paint += (s, e) =>
            {
                using (var pen = new System.Drawing.Pen(AppTheme.AccentGreen, 2))
                    e.Graphics.DrawLine(pen, 0, pnlHeader.Height - 1, pnlHeader.Width, pnlHeader.Height - 1);
            };

            lblHeaderTitle = new Label { Text = "Registro de Empleados", Font = AppTheme.FontHeader, ForeColor = AppTheme.TextPrimary, AutoSize = true, Location = new Point(24, 14) };
            lblHeaderSub   = new Label { Text = "Agrega nuevos empleados al sistema", Font = AppTheme.FontSubtitle, ForeColor = AppTheme.AccentGreen, AutoSize = true, Location = new Point(24, 42) };
            pnlHeader.Controls.AddRange(new Control[] { lblHeaderTitle, lblHeaderSub });

            // ── Panel formulario ────────────────────────────────────────────────
            pnlForm = new Panel
            {
                Size      = new Size(516, 450),
                Location  = new Point(22, 88),
                BackColor = AppTheme.BgCard,
                Padding   = new Padding(24),
            };

            // Columna izquierda
            lblRutLbl      = MakeLabel("RUT del Empleado", new Point(24, 20));
            txtRut         = MakeInput(new Point(24, 42), "Ej. 12.345.678-9");

            lblNombreLbl   = MakeLabel("Nombre Completo", new Point(24, 96));
            txtNombre      = MakeInput(new Point(24, 118), "Ej. Juan Pérez");

            lblDireccionLbl = MakeLabel("Dirección", new Point(24, 172));
            txtDireccion    = MakeInput(new Point(24, 194), "Ej. Av. Principal 123");

            lblTelefonoLbl = MakeLabel("Teléfono", new Point(24, 248));
            txtTelefono    = MakeInput(new Point(24, 270), "Ej. +56 9 1234 5678");

            lblValorHoraLbl  = MakeLabel("Valor Hora ($)", new Point(24, 322));
            txtValorHora     = MakeInput(new Point(24, 344), "5000");

            lblValorExtraLbl = MakeLabel("Valor Hora Extra ($)", new Point(270, 322));
            txtValorExtra    = new TextBox
            {
                Size            = new Size(222, 36),
                Location        = new Point(270, 344),
                BackColor       = AppTheme.BgInput,
                ForeColor       = AppTheme.TextPrimary,
                BorderStyle     = BorderStyle.FixedSingle,
                Font            = AppTheme.FontInput,
                PlaceholderText = "7000",
            };

            // Ajustar ancho de txtValorHora para dejar espacio al extra
            txtValorHora.Size = new Size(222, 36);

            btnAgregar = new ModernButton
            {
                Text     = "Agregar Empleado",
                Size     = new Size(468, 44),
                Location = new Point(24, 396),
            };
            btnAgregar.Click += btnAgregar_Click;

            pnlForm.Controls.AddRange(new Control[] {
                lblRutLbl,      txtRut,
                lblNombreLbl,   txtNombre,
                lblDireccionLbl,txtDireccion,
                lblTelefonoLbl, txtTelefono,
                lblValorHoraLbl,  txtValorHora,
                lblValorExtraLbl, txtValorExtra,
                btnAgregar
            });

            // ── Botón Ver Trabajadores ──────────────────────────────────────────
            btnVerTrabajadores = new ModernButton
            {
                Text     = "Ver Listado de Trabajadores",
                Size     = new Size(516, 38),
                Location = new Point(22, 550),
            };
            btnVerTrabajadores.SetSecondary();
            btnVerTrabajadores.Click += btnVerTrabajadores_Click;

            this.Controls.AddRange(new Control[] { pnlHeader, pnlForm });
            this.ResumeLayout(false);
        }

        private Label MakeLabel(string text, Point location) => new Label
        {
            Text = text, Font = AppTheme.FontLabel, ForeColor = AppTheme.TextMuted,
            AutoSize = true, Location = location,
        };

        private TextBox MakeInput(Point location, string placeholder = "") => new TextBox
        {
            Size = new Size(468, 36), Location = location,
            BackColor = AppTheme.BgInput, ForeColor = AppTheme.TextPrimary,
            BorderStyle = BorderStyle.FixedSingle, Font = AppTheme.FontInput,
            PlaceholderText = placeholder,
        };
    }
}
