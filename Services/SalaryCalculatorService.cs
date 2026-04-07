using System;
using System.Collections.Generic;
using GestionSueldos.Models;

namespace GestionSueldos.Services
{
    /// <summary>
    /// Servicio de cálculo de remuneraciones.
    /// Toda la lógica de negocio reside aquí, separada de los formularios.
    /// Patrón: Service Layer.
    /// </summary>
    public class SalaryCalculatorService
    {
        // ── Tasas AFP ──────────────────────────────────────────────────────────
        private static readonly Dictionary<string, decimal> TasasAFP =
            new Dictionary<string, decimal>(StringComparer.OrdinalIgnoreCase)
        {
            { "CUPRUM",  0.07m },
            { "MODELO",  0.09m },
            { "CAPITAL", 0.12m },
            { "PROVIDA", 0.13m },
        };

        // ── Tasas de Salud ─────────────────────────────────────────────────────
        private static readonly Dictionary<string, decimal> TasasSalud =
            new Dictionary<string, decimal>(StringComparer.OrdinalIgnoreCase)
        {
            { "FONASA",   0.12m },
            { "CONSALUD", 0.13m },
            { "MASVIDA",  0.14m },
            { "BANMEDICA",0.15m },
        };

        // ── Valores por defecto ────────────────────────────────────────────────
        private const decimal VALOR_HORA_DEFAULT  = 5_000m;
        private const decimal VALOR_EXTRA_DEFAULT = 7_000m;

        /// <summary>
        /// Retorna los nombres de las AFP disponibles.
        /// </summary>
        public IEnumerable<string> ObtenerNombresAFP()  => TasasAFP.Keys;

        /// <summary>
        /// Retorna los nombres de los sistemas de salud disponibles.
        /// </summary>
        public IEnumerable<string> ObtenerNombresSalud() => TasasSalud.Keys;

        /// <summary>
        /// Calcula el sueldo completo de un empleado.
        /// </summary>
        /// <param name="horasTrabajadas">Horas normales trabajadas en el mes.</param>
        /// <param name="horasExtras">Horas extras trabajadas.</param>
        /// <param name="afp">Nombre de la AFP seleccionada.</param>
        /// <param name="salud">Nombre del sistema de salud seleccionado.</param>
        /// <param name="valorHora">Valor por hora (usa default si es 0).</param>
        /// <param name="valorExtra">Valor por hora extra (usa default si es 0).</param>
        /// <returns>ResultadoSueldo con todos los cálculos detallados.</returns>
        public ResultadoSueldo Calcular(
            decimal horasTrabajadas,
            decimal horasExtras,
            string  afp,
            string  salud,
            decimal valorHora  = 0,
            decimal valorExtra = 0)
        {
            if (horasTrabajadas < 0) throw new ArgumentException("Las horas trabajadas no pueden ser negativas.");
            if (horasExtras < 0)    throw new ArgumentException("Las horas extras no pueden ser negativas.");
            if (!TasasAFP.ContainsKey(afp))    throw new ArgumentException($"AFP no reconocida: {afp}");
            if (!TasasSalud.ContainsKey(salud)) throw new ArgumentException($"Salud no reconocida: {salud}");

            decimal vh  = valorHora  > 0 ? valorHora  : VALOR_HORA_DEFAULT;
            decimal vhe = valorExtra > 0 ? valorExtra : VALOR_EXTRA_DEFAULT;

            decimal sueldoBruto    = (horasTrabajadas * vh) + (horasExtras * vhe);
            decimal descuentoAFP   = sueldoBruto * TasasAFP[afp];
            decimal descuentoSalud = sueldoBruto * TasasSalud[salud];

            return new ResultadoSueldo
            {
                SueldoBruto    = sueldoBruto,
                DescuentoAFP   = descuentoAFP,
                DescuentoSalud = descuentoSalud,
                AfpNombre      = afp.ToUpper(),
                SaludNombre    = salud.ToUpper(),
            };
        }
    }
}
