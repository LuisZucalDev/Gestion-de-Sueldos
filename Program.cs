using System;
using System.Linq;
using System.Windows.Forms;
using GestionSueldos.Forms;

namespace GestionSueldos
{
    static class Program
    {
        /// <summary>
        /// Punto de entrada principal de la aplicación.
        /// Usa --admin-session para iniciar sesión de admin directamente.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("es-CL");
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("es-CL");
            
            Form formToRun;
            if (args != null && args.Contains("--admin-session"))
            {
                formToRun = new FrmAdministrador();
            }
            else
            {
                formToRun = new FrmLogin();
            }
            Application.Run(formToRun);
        }
    }
}
