using System.Drawing;
using System.Windows.Forms;
using GestionSueldos.UI;

namespace GestionSueldos.Forms
{
    partial class FrmLogin
    {
        private System.ComponentModel.IContainer components = null;

        private Panel      pnlCard;
        private Label      lblTitle;
        private Label      lblSubtitle;
        private Label      lblUsuarioLbl;
        private TextBox    txtUsuario;
        private Label      lblContrasenaLbl;
        private TextBox    txtContrasena;
        private ModernButton btnIniciarSesion;
        private ModernButton btnCancelar;
        private Label      lblError;
        private Label      lblHint;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();

            // ── Formulario ──────────────────────────────────────────────────────
            this.Text            = "Gestión de Sueldos — Inicio de Sesión";
            this.ClientSize      = new Size(480, 560);
            this.BackColor       = AppTheme.BgDark;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox     = false;
            this.StartPosition   = FormStartPosition.CenterScreen;
            this.Font            = AppTheme.FontLabel;

            // ── Card central ────────────────────────────────────────────────────
            pnlCard = new Panel
            {
                Size      = new Size(380, 440),
                Location  = new Point(50, 60),
                BackColor = AppTheme.BgCard,
            };
            pnlCard.Paint += (s, e) =>
            {
                // Borde superior acento
                using (var pen = new System.Drawing.Pen(AppTheme.AccentPrimary, 2))
                    e.Graphics.DrawLine(pen, 0, 0, pnlCard.Width, 0);
            };

            // ── Título ──────────────────────────────────────────────────────────
            lblTitle = new Label
            {
                Text      = "Sueldo Pro",
                Font      = AppTheme.FontTitle,
                ForeColor = AppTheme.TextPrimary,
                AutoSize  = true,
                Location  = new Point(30, 30),
            };

            lblSubtitle = new Label
            {
                Text      = "Gestión Profesional de Remuneraciones",
                Font      = AppTheme.FontSubtitle,
                ForeColor = AppTheme.AccentPrimary,
                AutoSize  = true,
                Location  = new Point(30, 70),
            };

            // ── Campo Usuario ───────────────────────────────────────────────────
            lblUsuarioLbl = new Label
            {
                Text      = "RUT / Usuario",
                Font      = AppTheme.FontLabel,
                ForeColor = AppTheme.TextMuted,
                AutoSize  = true,
                Location  = new Point(30, 120),
            };

            txtUsuario = new TextBox
            {
                Size            = new Size(320, 36),
                Location        = new Point(30, 142),
                BackColor       = AppTheme.BgInput,
                ForeColor       = AppTheme.TextPrimary,
                BorderStyle     = BorderStyle.FixedSingle,
                Font            = AppTheme.FontInput,
                PlaceholderText = "Ej. 12.345.678-9",
            };

            // ── Campo Contraseña ────────────────────────────────────────────────
            lblContrasenaLbl = new Label
            {
                Text      = "Contraseña",
                Font      = AppTheme.FontLabel,
                ForeColor = AppTheme.TextMuted,
                AutoSize  = true,
                Location  = new Point(30, 196),
            };

            txtContrasena = new TextBox
            {
                Size            = new Size(320, 36),
                Location        = new Point(30, 218),
                BackColor       = AppTheme.BgInput,
                ForeColor       = AppTheme.TextPrimary,
                BorderStyle     = BorderStyle.FixedSingle,
                Font            = AppTheme.FontInput,
                UseSystemPasswordChar = true,
                PlaceholderText = "••••••••",
            };
            txtContrasena.KeyDown += txtContrasena_KeyDown;

            // ── Hint ────────────────────────────────────────────────────────────
            lblHint = new Label
            {
                Text      = "Admin: usuario 'admin' / clave 'admin'\nEmpleados: RUT como usuario / primeros 4 dígitos como clave",
                Font      = new Font("Segoe UI", 7.5f),
                ForeColor = AppTheme.TextMuted,
                Size      = new Size(320, 42),
                Location  = new Point(30, 262),
            };

            // ── Label error ─────────────────────────────────────────────────────
            lblError = new Label
            {
                Text      = "",
                Font      = AppTheme.FontLabel,
                ForeColor = AppTheme.TextError,
                AutoSize  = false,
                Size      = new Size(320, 36),
                Location  = new Point(30, 310),
                Visible   = false,
                TextAlign = ContentAlignment.MiddleCenter,
            };

            // ── Botón Iniciar Sesión ─────────────────────────────────────────────
            btnIniciarSesion = new ModernButton
            {
                Text     = "Iniciar Sesión",
                Size     = new Size(320, 44),
                Location = new Point(30, 350),
            };
            btnIniciarSesion.Click += btnIniciarSesion_Click;

            // ── Botón Cancelar ───────────────────────────────────────────────────
            btnCancelar = new ModernButton
            {
                Text     = "Cancelar",
                Size     = new Size(320, 36),
                Location = new Point(30, 402),
            };
            btnCancelar.SetSecondary();
            btnCancelar.Click += btnCancelar_Click;

            // ── Agregar controles al card ───────────────────────────────────────
            pnlCard.Controls.AddRange(new Control[] {
                lblTitle, lblSubtitle,
                lblUsuarioLbl, txtUsuario,
                lblContrasenaLbl, txtContrasena,
                lblHint, lblError,
                btnIniciarSesion, btnCancelar
            });

            this.Controls.Add(pnlCard);
            this.ResumeLayout(false);
        }
    }
}
