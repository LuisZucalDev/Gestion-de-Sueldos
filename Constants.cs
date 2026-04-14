using System.Configuration;
using System.IO;
using System.Text.RegularExpressions;

namespace GestionSueldos
{
    public static class Constants
    {
        // ── Archivos ─────────────────────────────────────────────────────────
        public const string EMPLEADO_FILE = "informacion_empleado.txt";
        public const string SUELDO_FILE   = "informacion_sueldo.txt";

        public static string EmpleadoPath => Path.Combine(AppDomain.CurrentDomain.BaseDirectory, GetAppSetting("EmpleadoFile", EMPLEADO_FILE));
        public static string SueldoPath   => Path.Combine(AppDomain.CurrentDomain.BaseDirectory, GetAppSetting("SueldoFile", SUELDO_FILE));

        // ── Autenticación ────────────────────────────────────────────────────
        public const string ADMIN_USER     = "admin";
        public const string ADMIN_PASSWORD = "admin123";  // Cambiar en prod

        // ── Mensajes Comunes ─────────────────────────────────────────────────
        public const string MSG_ERROR_RUT     = "El RUT es requerido.";
        public const string MSG_ERROR_NOMBRE  = "El nombre es requerido.";
        public const string MSG_ERROR_HORAS   = "Ingresa un número válido de horas trabajadas.";
        public const string MSG_ERROR_EXTRAS  = "Ingresa un número válido de horas extras.";
        public const string MSG_ERROR_AFP     = "Selecciona un sistema AFP.";
        public const string MSG_ERROR_SALUD   = "Selecciona un sistema de salud.";
        public const string MSG_CALC_ERROR    = "Primero debes calcular el sueldo.";
        public const string MSG_SAVED         = "Sueldo guardado exitosamente.";
        public const string MSG_ADDED         = "Empleado '{0}' agregado exitosamente.";
        public const string MSG_LOGIN_FAIL    = "Credenciales incorrectas. Intenta nuevamente.";
        public const string MSG_EMPTY_CREDS   = "Por favor ingresa usuario y contraseña.";

        // ── Validación RUT Chile ─────────────────────────────────────────────────
        public static bool ValidarRutChile(string rut)
        {
            if (string.IsNullOrWhiteSpace(rut)) return false;
            rut = Regex.Replace(rut, @"[.-]", "").ToUpper().Trim();

            if (rut.Length != 8 && rut.Length != 9) return false;
            if (!Regex.IsMatch(rut, @"^[0-9]+[0-9K]$")) return false;

            string dv = rut[^1];
            string numero = rut[..^1];
            int sum = 0;
            int mul = 2;

            for (int i = numero.Length - 1; i >= 0; i--)
            {
                int n = int.Parse(numero[i].ToString()) * mul;
                sum += n / 10 + n % 10;
                mul = mul == 7 ? 2 : mul + 1;
            }

            int resto = sum % 11;
            string dvEsperado = resto == 0 ? "0" : resto == 1 ? "K" : (11 - resto).ToString();

            return dv == dvEsperado;
        }

        // ── Config Helper ────────────────────────────────────────────────────
        private static string GetAppSetting(string key, string defaultValue)
        {
            return ConfigurationManager.AppSettings[key] ?? defaultValue;
        }
    }
}
