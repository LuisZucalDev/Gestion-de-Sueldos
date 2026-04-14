using System.Drawing;

namespace GestionSueldos.UI
{
    /// <summary>
    /// Paleta de colores y fuentes centralizada para toda la aplicación.
    /// </summary>
    public static class AppTheme
    {
        // ── Colores de Fondo ────────────────────────────────────
        public static readonly Color BgDark   = Color.FromArgb(13, 17, 35);
        public static readonly Color BgCard   = Color.FromArgb(26, 32, 58);
        public static readonly Color BgInput  = Color.FromArgb(15, 23, 42);
        public static readonly Color BgHover  = Color.FromArgb(30, 41, 59);

        // ── Colores de Acento ────────────────────────────────────
        public static readonly Color AccentPrimary = Color.FromArgb(99, 102, 241);   // indigo
        public static readonly Color AccentHover   = Color.FromArgb(79, 70, 229);
        public static readonly Color AccentPressed = Color.FromArgb(67, 56, 202);
        public static readonly Color AccentGreen   = Color.FromArgb(16, 185, 129);
        public static readonly Color AccentRed     = Color.FromArgb(244, 63, 94);
        public static readonly Color AccentGray    = Color.FromArgb(51, 65, 85);

        // ── Colores de Texto ─────────────────────────────────────
        public static readonly Color TextPrimary = Color.FromArgb(241, 245, 249);
        public static readonly Color TextMuted   = Color.FromArgb(100, 116, 139);
        public static readonly Color TextError   = Color.FromArgb(248, 113, 113);
        public static readonly Color ShadowDark  = Color.FromArgb(0, 0, 0, 32);
        public static readonly Color ShadowLight = Color.FromArgb(0, 0, 0, 16);
        public static readonly Color GlowPrimary = Color.FromArgb(120, 99, 102, 241);

        // ── Bordes ───────────────────────────────────────────────
        public static readonly Color Border      = Color.FromArgb(51, 65, 85);

        // ── Fuentes ──────────────────────────────────────────────
        public static readonly Font FontTitle    = new Font("Segoe UI", 22f, FontStyle.Bold);
        public static readonly Font FontSubtitle = new Font("Segoe UI", 10f, FontStyle.Regular);
        public static readonly Font FontLabel    = new Font("Segoe UI", 9f,  FontStyle.Regular);
        public static readonly Font FontLabelBold= new Font("Segoe UI", 9f,  FontStyle.Bold);
        public static readonly Font FontInput    = new Font("Segoe UI", 10f, FontStyle.Regular);
        public static readonly Font FontButton   = new Font("Segoe UI", 9.5f,FontStyle.Bold);
        public static readonly Font FontResult   = new Font("Segoe UI", 13f, FontStyle.Bold);
        public static readonly Font FontResultBig= new Font("Segoe UI", 20f, FontStyle.Bold);
        public static readonly Font FontHeader   = new Font("Segoe UI", 14f, FontStyle.Bold);
    }
}
