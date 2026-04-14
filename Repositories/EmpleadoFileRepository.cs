using System;
using System.Collections.Generic;
using System.IO;
using GestionSueldos.Models;

namespace GestionSueldos.Repositories
{
    /// <summary>
    /// Implementación del repositorio de empleados usando un archivo de texto plano.
    /// Reemplaza la lectura/escritura manual dispersa en los formularios del código original.
    /// </summary>
    public class EmpleadoFileRepository : IEmpleadoRepository
    {
        private readonly string _filePath;

        public EmpleadoFileRepository(string filePath = "informacion_empleado.txt")
        {
            _filePath = filePath;
        }

        public IEnumerable<Empleado> ObtenerTodos()
        {
            var lista = new List<Empleado>();
            if (!File.Exists(_filePath)) return lista;

            foreach (string linea in File.ReadAllLines(_filePath))
            {
                Empleado emp = Empleado.FromFileLine(linea);
                if (emp != null) lista.Add(emp);
            }
            return lista;
        }

        public void Agregar(Empleado empleado)
        {
            if (empleado == null) throw new ArgumentNullException(nameof(empleado));
            using (StreamWriter writer = File.AppendText(_filePath))
            {
                writer.WriteLine(empleado.ToFileLine());
            }
        }

        public void Eliminar(string rut)
        {
            if (!File.Exists(_filePath)) return;

            var lineas = new List<string>(File.ReadAllLines(_filePath));
            lineas.RemoveAll(l => l.StartsWith(rut));
            File.WriteAllLines(_filePath, lineas);
        }

        public Empleado BuscarPorRut(string rut)
        {
            if (!File.Exists(_filePath)) return null;

            foreach (string linea in File.ReadAllLines(_filePath))
            {
                Empleado emp = Empleado.FromFileLine(linea);
                if (emp != null && emp.Rut.Trim().Equals(rut.Trim(), StringComparison.OrdinalIgnoreCase))
                    return emp;
            }
            return null;
        }
    }
}
