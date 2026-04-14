using System;
using System.Collections.Generic;
using GestionSueldos.Models;

namespace GestionSueldos.Services
{
    /// <summary>
    /// Servicio de cálculo de remuneraciones Chile 2024.
    /// +Cotiz obligatoria 7%, impuesto renta simplificado.
    /// </summary>
    public class SalaryCalculatorService
    {
        private static readonly Dictionary<string, decimal> TasasAFP =
            new Dictionary<string, decimal>(StringComparer.OrdinalIgnoreCase)
        {
            { "CUPRUM",  0.07m },
            { "MODELO",  0.09m },
            { "CAPITAL", 0.12m },
            { "PROVIDA", 0.13m },
        };

        private static readonly Dictionary<string, decimal> TasasSalud =
            new Dictionary<string, decimal>(StringComparer.OrdinalIgnoreCase)
        {
            { "FONASA",   0.12m },
            { "CONSALUD", 0.13m },
            { "MASVIDA",  0.14m },
            { "BANMEDICA",0.15m },
        };

        private const decimal VALOR_HORA_DEFAULT  = 5_000m;
        private const decimal VALOR_EXTRA_DEFAULT = 7_000m;

        public IEnumerable<string> ObtenerNombresAFP() => TasasAFP.Keys;
        public IEnumerable<string> ObtenerNombresSalud() => TasasSalud.Keys;

        public ResultadoSueldo Calcular(
            decimal horasTrabajadas,
            decimal horasExtras,
            string afp,
            string salud,
            decimal valorHora = 0,
            decimal valorExtra = 0)
        {
            if (horasTrabajadas < 0) throw new ArgumentException("Horas trabajadas inválidas.");
            if (horasExtras < 0) throw new ArgumentException("Horas extras inválidas.");
            TasasAFP.TryGetValue(afp, out decimal tasaAFP);
            TasasSalud.TryGetValue(salud, out decimal tasaSalud);

            decimal vh = valorHora > 0 ? valorHora : VALOR_HORA_DEFAULT;
            decimal vhe = valorExtra > 0 ? valorExtra : VALOR_EXTRA_DEFAULT;

            decimal sueldoBase = (horasTrabajadas * vh) + (horasExtras * vhe);
            decimal cotizOblig = sueldoBase * 0.07m; // AFP obligatoria
            decimal imponible = sueldoBase - cotizOblig;
            decimal renta = Math.Max(0, imponible * 0.04m - 100_000m); // Tramo 1 simplificado
            decimal afpDesc = sueldoBase * tasaAFP;
            decimal saludDesc = sueldoBase * tasaSalud;

            return new ResultadoSueldo
            {
                SueldoBruto      = sueldoBase,
                CotizObligatoria = cotizOblig,
                ImpuestoRenta    = renta,
                DescuentoAFP     = afpDesc,
                DescuentoSalud   = saludDesc,
                AfpNombre        = afp.ToUpper(),
                SaludNombre      = salud.ToUpper(),
            };
        }
    }
}

