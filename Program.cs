using System;
using System.Linq;
using System.Windows.Forms;
using GestionSueldos.Forms;

namespace GestionSueldos
{
    static class Program
    {
        /// <summary>
        /// Punto de entrada. --admin-session inicia admin sin login.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("es-CL");
            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("es-CL");
            
            Form form;
            if (args.Contains("--admin-session"))
            {
                form = new FrmAdministrador();
            }
            else
            {
                form = new FrmLogin();
            }
            Application.Run(form);
        }
    }
}
