using System.IO;
using GestionSueldos.Models;

namespace GestionSueldos.Repositories
{
    /// <summary>
    /// Implementación del repositorio de sueldos calculados usando un archivo de texto plano.
    /// </summary>
    public class SueldoFileRepository : ISueldoRepository
    {
        private readonly string _filePath;

        public SueldoFileRepository(string filePath = "informacion_sueldo.txt")
        {
            _filePath = filePath;
        }

        public void Guardar(ResultadoSueldo resultado)
        {
            File.AppendAllText(_filePath, resultado.ToFileLine() + System.Environment.NewLine);
        }

        public ResultadoSueldo ObtenerUltimo()
        {
            if (!File.Exists(_filePath)) return null;

            string[] lineas = File.ReadAllLines(_filePath);
            if (lineas.Length == 0) return null;

            string ultima = lineas[lineas.Length - 1];
            string[] partes = ultima.Split('|');
            if (partes.Length < 4) return null;

            if (!decimal.TryParse(partes[0].Trim(), out decimal bruto))    return null;
            if (!decimal.TryParse(partes[1].Trim(), out decimal desAFP))   return null;
            if (!decimal.TryParse(partes[2].Trim(), out decimal desSalud)) return null;

            return new ResultadoSueldo
            {
                SueldoBruto    = bruto,
                DescuentoAFP   = desAFP,
                DescuentoSalud = desSalud
            };
        }
    }
}
