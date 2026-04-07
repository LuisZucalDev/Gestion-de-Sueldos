using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace GestionSueldos.UI
{
    /// <summary>
    /// Botón personalizado con efecto hover, color configurable y esquinas redondeadas.
    /// </summary>
    public class ModernButton : Button
    {
        private Color _normalColor;
        private Color _hoverColor;
        private Color _pressedColor;
        private bool   _isHovering;
        private int    _radius = 8;

        public ModernButton()
        {
            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.DoubleBuffer, true);
            FlatStyle = FlatStyle.Flat;
            FlatAppearance.BorderSize = 0;
            Cursor    = Cursors.Hand;
            Font      = AppTheme.FontButton;
            ForeColor = Color.White;
            Height    = 42;
            SetColors(AppTheme.AccentPrimary, AppTheme.AccentHover, AppTheme.AccentPressed);
        }

        public void SetColors(Color normal, Color hover, Color pressed)
        {
            _normalColor  = normal;
            _hoverColor   = hover;
            _pressedColor = pressed;
            BackColor     = normal;
            Invalidate();
        }

        public void SetSecondary()
        {
            SetColors(AppTheme.AccentGray, AppTheme.BgHover, Color.FromArgb(44, 55, 72));
        }

        public void SetDanger()
        {
            SetColors(Color.FromArgb(50, 20, 30), AppTheme.AccentRed, Color.FromArgb(180, 40, 70));
            ForeColor = AppTheme.AccentRed;
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            _isHovering = true;
            BackColor = _hoverColor;
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            _isHovering = false;
            BackColor = _normalColor;
        }

        protected override void OnMouseDown(MouseEventArgs mevent)
        {
            base.OnMouseDown(mevent);
            BackColor = _pressedColor;
        }

        protected override void OnMouseUp(MouseEventArgs mevent)
        {
            base.OnMouseUp(mevent);
            BackColor = _isHovering ? _hoverColor : _normalColor;
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            Graphics g = pevent.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            // Background rounded rect
            using (GraphicsPath path = RoundedRect(ClientRectangle, _radius))
            using (SolidBrush brush = new SolidBrush(BackColor))
            {
                g.FillPath(brush, path);
            }

            // Text
            TextRenderer.DrawText(g, Text, Font, ClientRectangle, ForeColor,
                TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
        }

        private static GraphicsPath RoundedRect(Rectangle bounds, int radius)
        {
            int d = radius * 2;
            GraphicsPath path = new GraphicsPath();
            path.AddArc(bounds.X, bounds.Y, d, d, 180, 90);
            path.AddArc(bounds.Right - d, bounds.Y, d, d, 270, 90);
            path.AddArc(bounds.Right - d, bounds.Bottom - d, d, d, 0, 90);
            path.AddArc(bounds.X, bounds.Bottom - d, d, d, 90, 90);
            path.CloseFigure();
            return path;
        }
    }
}
