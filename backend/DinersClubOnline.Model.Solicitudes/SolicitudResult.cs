using System;

namespace DinersClubOnline.Model.Solicitudes
{
    public class SolicitudResult
    {
        public bool Resultado { get; private set; }
        public int? NumeroSolicitud { get; private set; }
        public DateTime? FechaRegistro { get; private set; }

        private SolicitudResult()
        {
        }

        public static SolicitudResult CreateSolicitudRegistradaResult(int numeroSolicitud, DateTime fechaRegistro)
        {
            return new SolicitudResult
            {
                Resultado = true,
                NumeroSolicitud = numeroSolicitud,
                FechaRegistro = fechaRegistro
            };
        }

        public static SolicitudResult CreateErrorResult()
        {
            return new SolicitudResult
            {
                Resultado = false
            };
        }
    }
}