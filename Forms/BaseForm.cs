using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using GestionSueldos.Forms;
using GestionSueldos.UI;

namespace GestionSueldos.Forms
{
    public class BaseForm : Form
    {
        protected Panel pnlHeaderGradient;
        protected Label lblTitle;
        protected Label lblSubtitle;
        protected ModernButton btnLogout;
        protected StatusStrip statusStrip;
        protected ToolStripStatusLabel lblStatus;

        public BaseForm()
        {
            InitializeBase();
        }

        private void InitializeBase()
        {
            // Form base settings
            this.Font = AppTheme.FontLabel;
            this.BackColor = AppTheme.BgDark;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Size = new Size(1000, 700);

            // Gradient header
            pnlHeaderGradient = new Panel
            {
                Height = 80,
                Dock = DockStyle.Top,
                BackColor = Color.Transparent
            };
            pnlHeaderGradient.Paint += PnlHeaderGradient_Paint;

            lblTitle = new Label
            {
                Location = new Point(24, 20),
                AutoSize = true,
                Font = AppTheme.FontHeader,
                ForeColor = AppTheme.TextPrimary,
                Text = "Sueldo Pro"
            };

            lblSubtitle = new Label
            {
                Location = new Point(24, 48),
                AutoSize = true,
                Font = AppTheme.FontSubtitle,
                ForeColor = AppTheme.TextMuted,
                Text = "Sistema de gestión de sueldos"
            };

            btnLogout = new ModernButton
            {
                Text = "Cerrar Sesión",
                Location = new Point(this.ClientSize.Width - 140, 25),
                Size = new Size(120, 32)
            };
            btnLogout.SetSecondary();
            btnLogout.Click += BtnLogout_Click;

            pnlHeaderGradient.Controls.AddRange(new Control[] { lblTitle, lblSubtitle, btnLogout });

            // Status bar
            statusStrip = new StatusStrip { SizingGrip = false };
            lblStatus = new ToolStripStatusLabel("Listo") { Spring = true };
            statusStrip.Items.Add(lblStatus);

            this.Controls.Add(statusStrip);
            this.Controls.Add(pnlHeaderGradient);
        }

        private void PnlHeaderGradient_Paint(object sender, PaintEventArgs e)
        {
            using (var brush = new LinearGradientBrush(
                pnlHeaderGradient.ClientRectangle,
                AppTheme.BgCard,
                Color.FromArgb(45, 55, 72),
                90f))
            {
                e.Graphics.FillRectangle(brush, pnlHeaderGradient.ClientRectangle);
            }

            // Border bottom glow
            using (var pen = new Pen(Color.FromArgb(80, AppTheme.AccentPrimary.R, AppTheme.AccentPrimary.G, AppTheme.AccentPrimary.B), 1))
            {
                e.Graphics.DrawLine(pen, 0, pnlHeaderGradient.Height - 1, pnlHeaderGradient.Width, pnlHeaderGradient.Height - 1);
            }
        }

        private void BtnLogout_Click(object sender, EventArgs e)
        {
            var login = new FrmLogin();
            login.FormClosed += (s, args) => this.Close();
            login.Show();
            this.Hide();
        }

        protected virtual void UpdateStatus(string message)
        {
            lblStatus.Text = message;
            lblStatus.ForeColor = Color.FromArgb(100, 116, 139);
        }

        // Shadow effect helper
        protected void AddShadow(Panel panel)
        {
            panel.Paint += (s, e) =>
            {
                // Drop shadow simple
                using (var shadowBrush = new SolidBrush(Color.FromArgb(20, 0, 0, 0)))
                {
                    e.Graphics.FillRectangle(shadowBrush, 0, 0, panel.Width, panel.Height);
                }
            };
        }

        protected override void OnResize(EventArgs e)
        {
            btnLogout.Location = new Point(this.ClientSize.Width - 160, 24);
            base.OnResize(e);
        }
    }
}
