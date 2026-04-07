namespace GestionSueldos.Models
{
    /// <summary>
    /// Representa a un empleado registrado en el sistema.
    /// </summary>
    public class Empleado
    {
        public string Rut       { get; set; }
        public string Nombre    { get; set; }
        public string Direccion { get; set; }
        public string Telefono  { get; set; }
        public decimal ValorHora  { get; set; }
        public decimal ValorExtra { get; set; }

        public Empleado() { }

        public Empleado(string rut, string nombre, string direccion,
                        string telefono, decimal valorHora, decimal valorExtra)
        {
            Rut        = rut;
            Nombre     = nombre;
            Direccion  = direccion;
            Telefono   = telefono;
            ValorHora  = valorHora;
            ValorExtra = valorExtra;
        }

        /// <summary>
        /// Serializa el empleado al formato pipe-separated para archivos .txt
        /// </summary>
        public string ToFileLine() =>
            $"{Rut} | {Nombre} | {Direccion} | {Telefono} | {ValorHora} | {ValorExtra}";

        /// <summary>
        /// Deserializa una línea del archivo .txt a un objeto Empleado.
        /// Devuelve null si la línea es inválida.
        /// </summary>
        public static Empleado FromFileLine(string line)
        {
            if (string.IsNullOrWhiteSpace(line)) return null;
            string[] parts = line.Split('|');
            if (parts.Length < 3) return null;

            decimal valHora  = 5000m;
            decimal valExtra = 7000m;

            if (parts.Length >= 5) decimal.TryParse(parts[4].Trim(), out valHora);
            if (parts.Length >= 6) decimal.TryParse(parts[5].Trim(), out valExtra);

            return new Empleado(
                parts[0].Trim(),
                parts.Length > 1 ? parts[1].Trim() : "",
                parts.Length > 2 ? parts[2].Trim() : "",
                parts.Length > 3 ? parts[3].Trim() : "",
                valHora,
                valExtra
            );
        }
    }
}
