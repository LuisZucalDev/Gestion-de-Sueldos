namespace GestionSueldos.Models
{
    /// <summary>
    /// Contiene el resultado detallado del cálculo de un sueldo.
    /// Mejorado con cotiz/impuesto Chile 2024.
    /// </summary>
    public class ResultadoSueldo
    {
        public decimal SueldoBruto      { get; set; }
        public decimal CotizObligatoria { get; set; }
        public decimal ImpuestoRenta    { get; set; }
        public decimal DescuentoAFP     { get; set; }
        public decimal DescuentoSalud   { get; set; }
        public string  AfpNombre        { get; set; }
        public string  SaludNombre      { get; set; }

        /// <summary>
        /// Sueldo líquido final.
        /// </summary>
        public decimal SueldoLiquido => SueldoBruto - CotizObligatoria - ImpuestoRenta - DescuentoAFP - DescuentoSalud;

        /// <summary>
        /// Serializa para archivo (compatible old + new).
        /// </summary>
        public string ToFileLine() =>
            $"{SueldoBruto:F2} | {DescuentoAFP:F2} | {DescuentoSalud:F2} | {SueldoLiquido:F2} | {CotizObligatoria:F2} | {ImpuestoRenta:F2}";

        /// <summary>
        /// Parsea línea archivo (old format OK).
        /// </summary>
        public static ResultadoSueldo FromFileLine(string line)
        {
            if (string.IsNullOrWhiteSpace(line)) return null;
            string[] parts = line.Split('|');
            if (parts.Length < 4) return null;

            decimal bruto = 0, afp = 0, salud = 0, liquido = 0, cotiz = 0, renta = 0;
            decimal.TryParse(parts[0].Trim(), out bruto);
            decimal.TryParse(parts[1].Trim(), out afp);
            decimal.TryParse(parts[2].Trim(), out salud);
            decimal.TryParse(parts[3].Trim(), out liquido);
            if (parts.Length > 4) decimal.TryParse(parts[4].Trim(), out cotiz);
            if (parts.Length > 5) decimal.TryParse(parts[5].Trim(), out renta);

            return new ResultadoSueldo
            {
                SueldoBruto      = bruto,
                DescuentoAFP     = afp,
                DescuentoSalud   = salud,
                CotizObligatoria = cotiz,
                ImpuestoRenta    = renta,
                SueldoLiquido    = liquido
            };
        }
    }
}
