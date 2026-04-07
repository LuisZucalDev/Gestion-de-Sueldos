namespace GestionSueldos.Models
{
    /// <summary>
    /// Contiene el resultado detallado del cálculo de un sueldo.
    /// </summary>
    public class ResultadoSueldo
    {
        public decimal SueldoBruto     { get; set; }
        public decimal DescuentoAFP    { get; set; }
        public decimal DescuentoSalud  { get; set; }
        public string  AfpNombre       { get; set; }
        public string  SaludNombre     { get; set; }

        /// <summary>
        /// El sueldo líquido es calculado automáticamente.
        /// </summary>
        public decimal SueldoLiquido => SueldoBruto - DescuentoAFP - DescuentoSalud;

        /// <summary>
        /// Serializa el resultado al formato pipe-separated para archivos .txt
        /// </summary>
        public string ToFileLine() =>
            $"{SueldoBruto:F2} | {DescuentoAFP:F2} | {DescuentoSalud:F2} | {SueldoLiquido:F2}";
    }
}
