using System.Drawing;
using System.Windows.Forms;
using GestionSueldos.UI;

namespace GestionSueldos.Forms
{
    partial class FrmCalculadoraSueldo
    {
        private System.ComponentModel.IContainer components = null;

        // ── Controles ─────────────────────────────────────────────────────────
        private Panel        pnlHeader;
        private Label        lblHeaderTitle;
        private Label        lblHeaderSub;
        private Panel        pnlLeft;
        private Panel        pnlRight;
        private Label        lblHorasLbl;
        private TextBox      txtHorasTrabajadas;
        private Label        lblExtrasLbl;
        private TextBox      txtHorasExtras;
        private Label        lblAFPLbl;
        private ComboBox     cmbAFP;
        private Label        lblSaludLbl;
        private ComboBox     cmbSalud;
        private ModernButton btnCalcular;
        private ModernButton btnLimpiar;
        private Panel        pnlResultado;
        private Label        lblBrutoLbl;
        private Label        lblValorBruto;
        private Label        lblAFPDescLbl;
        private Label        lblValorAFP;
        private Label        lblSaludDescLbl;
        private Label        lblValorSalud;
        private Panel        pnlLiquidoBorder;
        private Label        lblLiquidoTitle;
        private Label        lblValorLiquido;
        private ModernButton btnGuardar;
        private ModernButton btnListar;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();

            // ── Formulario ──────────────────────────────────────────────────────
            this.Text            = "Sueldo Pro — Calculadora de Remuneración";
            this.ClientSize      = new Size(900, 600);
            this.BackColor       = AppTheme.BgDark;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox     = false;
            this.StartPosition   = FormStartPosition.CenterScreen;
            this.Font            = AppTheme.FontLabel;

            // ── Header ──────────────────────────────────────────────────────────
            pnlHeader = new Panel
            {
                Size      = new Size(900, 70),
                Location  = new Point(0, 0),
                BackColor = AppTheme.BgCard,
            };
            pnlHeader.Paint += (s, e) =>
            {
                using (var pen = new System.Drawing.Pen(AppTheme.AccentPrimary, 2))
                    e.Graphics.DrawLine(pen, 0, pnlHeader.Height - 1, pnlHeader.Width, pnlHeader.Height - 1);
            };

            lblHeaderTitle = new Label
            {
                Text      = "Calculadora de Remuneración",
                Font      = AppTheme.FontHeader,
                ForeColor = AppTheme.TextPrimary,
                AutoSize  = true,
                Location  = new Point(24, 14),
            };

            lblHeaderSub = new Label
            {
                Text      = "Ingresa los datos del trabajador para calcular el sueldo líquido",
                Font      = AppTheme.FontSubtitle,
                ForeColor = AppTheme.TextMuted,
                AutoSize  = true,
                Location  = new Point(24, 42),
            };

            pnlHeader.Controls.AddRange(new Control[] { lblHeaderTitle, lblHeaderSub });

            // ── Panel izquierdo (formulario) ────────────────────────────────────
            pnlLeft = new Panel
            {
                Size      = new Size(420, 490),
                Location  = new Point(20, 84),
                BackColor = AppTheme.BgCard,
                Padding   = new Padding(24),
            };

            lblHorasLbl = MakeLabel("Horas Trabajadas", new Point(24, 20));
            txtHorasTrabajadas = MakeInput(new Point(24, 42), "0");

            lblExtrasLbl = MakeLabel("Horas Extras", new Point(24, 94));
            txtHorasExtras = MakeInput(new Point(24, 116), "0");

            lblAFPLbl = MakeLabel("Sistema AFP", new Point(24, 170));
            cmbAFP    = MakeCombo(new Point(24, 192));

            lblSaludLbl = MakeLabel("Sistema de Salud", new Point(24, 246));
            cmbSalud    = MakeCombo(new Point(24, 268));

            btnCalcular = new ModernButton
            {
                Text     = "Calcular Sueldo",
                Size     = new Size(372, 44),
                Location = new Point(24, 326),
            };
            btnCalcular.Click += btnCalcular_Click;

            btnLimpiar = new ModernButton
            {
                Text     = "Limpiar Campos",
                Size     = new Size(372, 38),
                Location = new Point(24, 378),
            };
            btnLimpiar.SetSecondary();
            btnLimpiar.Click += btnLimpiar_Click;

            pnlLeft.Controls.AddRange(new Control[] {
                lblHorasLbl, txtHorasTrabajadas,
                lblExtrasLbl, txtHorasExtras,
                lblAFPLbl,   cmbAFP,
                lblSaludLbl, cmbSalud,
                btnCalcular, btnLimpiar
            });

            // ── Panel derecho (resultados) ──────────────────────────────────────
            pnlRight = new Panel
            {
                Size      = new Size(430, 490),
                Location  = new Point(456, 84),
                BackColor = AppTheme.BgCard,
            };

            // Resultados inicialmente ocultos
            pnlResultado = new Panel
            {
                Size     = new Size(430, 490),
                Location = new Point(0, 0),
                BackColor = Color.Transparent,
                Visible  = false,
            };

            lblBrutoLbl = MakeLabel("Sueldo Bruto", new Point(24, 24));
            lblValorBruto = new Label
            {
                Text      = "$0",
                Font      = AppTheme.FontResult,
                ForeColor = AppTheme.TextPrimary,
                AutoSize  = true,
                Location  = new Point(24, 44),
            };

            // Línea divisoria
            var sep = new Panel { Size = new Size(382, 1), Location = new Point(24, 90), BackColor = AppTheme.Border };

            lblAFPDescLbl  = MakeLabel("Descuento AFP", new Point(24, 104));
            lblValorAFP    = new Label { Text = "-$0", Font = AppTheme.FontLabelBold, ForeColor = AppTheme.AccentRed, AutoSize = true, Location = new Point(24, 124) };

            lblSaludDescLbl = MakeLabel("Descuento Salud", new Point(24, 158));
            lblValorSalud   = new Label { Text = "-$0", Font = AppTheme.FontLabelBold, ForeColor = AppTheme.AccentRed, AutoSize = true, Location = new Point(24, 178) };

            // Panel líquido con fondo especial
            pnlLiquidoBorder = new Panel
            {
                Size      = new Size(382, 100),
                Location  = new Point(24, 220),
                BackColor = Color.FromArgb(20, 99, 102, 241),
            };
            pnlLiquidoBorder.Paint += (s, e) =>
            {
                using (var pen = new System.Drawing.Pen(AppTheme.AccentPrimary, 1))
                    e.Graphics.DrawRectangle(pen, 0, 0, pnlLiquidoBorder.Width - 1, pnlLiquidoBorder.Height - 1);
            };

            lblLiquidoTitle = new Label
            {
                Text      = "SUELDO LÍQUIDO",
                Font      = new Font("Segoe UI", 8f, FontStyle.Bold),
                ForeColor = AppTheme.AccentPrimary,
                AutoSize  = true,
                Location  = new Point(16, 12),
            };

            lblValorLiquido = new Label
            {
                Text      = "$0",
                Font      = AppTheme.FontResultBig,
                ForeColor = AppTheme.AccentGreen,
                AutoSize  = true,
                Location  = new Point(16, 36),
            };

            pnlLiquidoBorder.Controls.AddRange(new Control[] { lblLiquidoTitle, lblValorLiquido });

            btnGuardar = new ModernButton
            {
                Text     = "Guardar Registro",
                Size     = new Size(382, 44),
                Location = new Point(24, 336),
                Enabled  = false,
            };
            btnGuardar.Click += btnGuardar_Click;

            btnListar = new ModernButton
            {
                Text     = "Ver Historial de Trabajadores",
                Size     = new Size(382, 38),
                Location = new Point(24, 388),
                Enabled  = false,
            };
            btnListar.SetSecondary();
            btnListar.Click += btnListar_Click;

            pnlResultado.Controls.AddRange(new Control[] {
                lblBrutoLbl, lblValorBruto, sep,
                lblAFPDescLbl, lblValorAFP,
                lblSaludDescLbl, lblValorSalud,
                pnlLiquidoBorder,
                btnGuardar, btnListar
            });

            pnlRight.Controls.Add(pnlResultado);

            // ── Añadir todo al formulario ───────────────────────────────────────
            this.Controls.AddRange(new Control[] { pnlHeader, pnlLeft, pnlRight });
            this.ResumeLayout(false);
        }

        // Helpers de creación de controles
        private Label MakeLabel(string text, Point location) => new Label
        {
            Text      = text,
            Font      = AppTheme.FontLabel,
            ForeColor = AppTheme.TextMuted,
            AutoSize  = true,
            Location  = location,
        };

        private TextBox MakeInput(Point location, string placeholder = "") => new TextBox
        {
            Size            = new Size(372, 36),
            Location        = location,
            BackColor       = AppTheme.BgInput,
            ForeColor       = AppTheme.TextPrimary,
            BorderStyle     = BorderStyle.FixedSingle,
            Font            = AppTheme.FontInput,
            PlaceholderText = placeholder,
        };

        private ComboBox MakeCombo(Point location) => new ComboBox
        {
            Size          = new Size(372, 36),
            Location      = location,
            BackColor     = AppTheme.BgInput,
            ForeColor     = AppTheme.TextPrimary,
            Font          = AppTheme.FontInput,
            DropDownStyle = ComboBoxStyle.DropDownList,
            FlatStyle     = FlatStyle.Flat,
        };
    }
}
