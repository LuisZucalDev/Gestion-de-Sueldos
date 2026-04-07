using GestionSueldos.Models;

namespace GestionSueldos.Repositories
{
    /// <summary>
    /// Define el contrato para el acceso a datos de Sueldos calculados.
    /// </summary>
    public interface ISueldoRepository
    {
        void Guardar(ResultadoSueldo resultado);
        ResultadoSueldo ObtenerUltimo();
    }
}
