using System.Collections.Generic;
using GestionSueldos.Models;

namespace GestionSueldos.Repositories
{
    /// <summary>
    /// Define el contrato para el acceso a datos de Empleados.
    /// Permite cambiar la implementación (archivo, base de datos, API) sin modificar el resto del sistema.
    /// </summary>
    public interface IEmpleadoRepository
    {
        IEnumerable<Empleado> ObtenerTodos();
        void Agregar(Empleado empleado);
        void Eliminar(string rut);
    }
}
