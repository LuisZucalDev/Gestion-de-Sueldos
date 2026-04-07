using System;
using System.Collections.Generic;
using GestionSueldos.Models;
using GestionSueldos.Repositories;

namespace GestionSueldos.Services
{
    /// <summary>
    /// Servicio de autenticación. Valida credenciales de usuarios y administradores.
    /// Patrón: Service Layer.
    /// </summary>
    public class AuthService
    {
        private const string ADMIN_USER     = "admin";
        private const string ADMIN_PASSWORD = "admin";

        private readonly IEmpleadoRepository _empleadoRepo;

        public AuthService(IEmpleadoRepository empleadoRepo)
        {
            _empleadoRepo = empleadoRepo ?? throw new ArgumentNullException(nameof(empleadoRepo));
        }

        public enum TipoUsuario { Desconocido, UsuarioNormal, Administrador }

        /// <summary>
        /// Valida las credenciales e indica qué tipo de usuario corresponde.
        /// </summary>
        public TipoUsuario ValidarCredenciales(string usuario, string contrasena)
        {
            if (string.IsNullOrWhiteSpace(usuario) || string.IsNullOrWhiteSpace(contrasena))
                return TipoUsuario.Desconocido;

            // ── Administrador ──────────────────────────────────────────────────
            if (usuario.Equals(ADMIN_USER, StringComparison.OrdinalIgnoreCase) &&
                contrasena.Equals(ADMIN_PASSWORD, StringComparison.OrdinalIgnoreCase))
                return TipoUsuario.Administrador;

            // ── Usuario Normal: RUT como usuario, primeros 4 dígitos como clave ─
            IEnumerable<Empleado> empleados = _empleadoRepo.ObtenerTodos();
            foreach (Empleado emp in empleados)
            {
                if (emp.Rut.Equals(usuario, StringComparison.OrdinalIgnoreCase))
                {
                    // Extrae solo dígitos del RUT para la contraseña
                    string soloDigitos = System.Text.RegularExpressions.Regex.Replace(emp.Rut, @"\D", "");
                    if (soloDigitos.Length >= 4 && contrasena.Length >= 4 &&
                        contrasena.Substring(0, 4) == soloDigitos.Substring(0, 4))
                        return TipoUsuario.UsuarioNormal;
                }
            }

            return TipoUsuario.Desconocido;
        }
    }
}
