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
    private static readonly string ADMIN_USER_HASH = SimpleHash("admin123"); // Default pass

        private static string SimpleHash(string pass)
        {
            uint hash = 0;
            foreach (char c in pass)
            {
                hash = ((hash << 5) + hash) + (uint)c;
            }
            return hash.ToString("X8");
        }

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
            if (usuario.Equals("admin", StringComparison.OrdinalIgnoreCase) &&
                SimpleHash(contrasena) == ADMIN_USER_HASH)
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
