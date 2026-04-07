using System.Drawing;
using System.Windows.Forms;
using GestionSueldos.UI;

namespace GestionSueldos.Forms
{
    partial class FrmListarTrabajadores
    {
        private System.ComponentModel.IContainer components = null;

        private Panel            pnlHeader;
        private Label            lblHeaderTitle;
        private Label            lblConteo;
        private DataGridView     dgv;
        private ModernButton     btnCerrar;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();

            // ── Formulario ──────────────────────────────────────────────────────
            this.Text            = "Sueldo Pro — Listado de Trabajadores";
            this.ClientSize      = new Size(900, 560);
            this.BackColor       = AppTheme.BgDark;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox     = false;
            this.StartPosition   = FormStartPosition.CenterParent;
            this.Font            = AppTheme.FontLabel;

            // ── Header ──────────────────────────────────────────────────────────
            pnlHeader = new Panel
            {
                Size      = new Size(900, 70),
                Location  = new Point(0, 0),
                BackColor = AppTheme.BgCard,
            };
            lblHeaderTitle = new Label { Text = "Listado de Trabajadores", Font = AppTheme.FontHeader, ForeColor = AppTheme.TextPrimary, AutoSize = true, Location = new Point(24, 14) };
            lblConteo      = new Label { Text = "0 empleados", Font = AppTheme.FontSubtitle, ForeColor = AppTheme.TextMuted, AutoSize = true, Location = new Point(24, 44) };
            pnlHeader.Controls.AddRange(new Control[] { lblHeaderTitle, lblConteo });

            // ── DataGridView ──────────────────────────────────────────────────
            dgv = new DataGridView
            {
                Size                  = new Size(860, 400),
                Location              = new Point(20, 84),
                BackgroundColor       = AppTheme.BgCard,
                ForeColor             = AppTheme.TextPrimary,
                GridColor             = AppTheme.Border,
                ColumnHeadersHeight   = 42,
                RowTemplate           = { Height = 38 },
                AllowUserToAddRows    = false,
                AllowUserToDeleteRows = false,
                ReadOnly              = true,
                SelectionMode         = DataGridViewSelectionMode.FullRowSelect,
                BorderStyle           = BorderStyle.None,
                CellBorderStyle       = DataGridViewCellBorderStyle.SingleHorizontal,
                AutoSizeColumnsMode   = DataGridViewAutoSizeColumnsMode.Fill,
            };

            // Estilo encabezados
            dgv.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor   = AppTheme.BgDark,
                ForeColor   = AppTheme.TextMuted,
                Font        = new Font("Segoe UI", 8.5f, FontStyle.Bold),
                SelectionBackColor = AppTheme.BgDark,
                SelectionForeColor = AppTheme.TextMuted,
                Padding     = new Padding(4, 0, 0, 0),
            };

            // Estilo filas
            dgv.DefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor         = AppTheme.BgCard,
                ForeColor         = AppTheme.TextPrimary,
                Font              = AppTheme.FontLabel,
                SelectionBackColor = AppTheme.AccentPrimary,
                SelectionForeColor = Color.White,
                Padding           = new Padding(4, 0, 0, 0),
            };

            // Estilo filas alternas
            dgv.AlternatingRowsDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor         = AppTheme.BgHover,
                ForeColor         = AppTheme.TextPrimary,
                SelectionBackColor = AppTheme.AccentPrimary,
                SelectionForeColor = Color.White,
            };

            dgv.EnableHeadersVisualStyles = false;
            dgv.RowHeadersVisible         = false;

            // ── Columnas ─────────────────────────────────────────────────────
            dgv.Columns.Add(new DataGridViewTextBoxColumn { Name = "colRut",       HeaderText = "RUT",          FillWeight = 15 });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { Name = "colNombre",    HeaderText = "Nombre",       FillWeight = 25 });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { Name = "colDireccion", HeaderText = "Dirección",    FillWeight = 25 });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { Name = "colTelefono",  HeaderText = "Teléfono",     FillWeight = 15 });
            dgv.Columns.Add(new DataGridViewTextBoxColumn { Name = "colSueldo",    HeaderText = "Sueldo Líquido", FillWeight = 15 });

            var colEliminar = new DataGridViewButtonColumn
            {
                Name             = "colEliminar",
                HeaderText       = "",
                Text             = "Eliminar",
                UseColumnTextForButtonValue = true,
                FillWeight       = 5,
                FlatStyle        = FlatStyle.Flat,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    BackColor  = Color.FromArgb(50, 244, 63, 94),
                    ForeColor  = AppTheme.AccentRed,
                    Font       = AppTheme.FontLabelBold,
                    SelectionBackColor = AppTheme.AccentRed,
                    SelectionForeColor = Color.White,
                }
            };
            dgv.Columns.Add(colEliminar);

            dgv.CellContentClick += dgv_CellContentClick;

            // ── Botón Cerrar ─────────────────────────────────────────────────
            btnCerrar = new ModernButton
            {
                Text     = "Cerrar",
                Size     = new Size(860, 40),
                Location = new Point(20, 500),
            };
            btnCerrar.SetSecondary();
            btnCerrar.Click += btnCerrar_Click;

            this.Controls.AddRange(new Control[] { pnlHeader, dgv, btnCerrar });
            this.ResumeLayout(false);
        }
    }
}
