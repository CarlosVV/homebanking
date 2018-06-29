using System;

namespace DinersClubOnline.Model.Solicitudes
{
    public abstract class Solicitud
    {
        public string IdSolicitud { get; set; }
        
        public string IdTipoOferta { get; set; }

        public int NumeroSolicitud { get; set; }

        /// <summary>
        /// Enviado, No Enviado
        /// TODO: podria ser un int y definir mas de 2 estados
        /// </summary>
        public bool Enviado { get; set; }
                
        public DateTime FechaCreacion { get; set; }

        public DateTime? FechaActualizacion { get; set; }

        /// <summary>
        /// Inicializa una nueva instancia de Solicitud con los siguientes valores
        /// IdSolicitud : GUID
        /// DispositivoCreacion : Dispositivo conectado
        /// UsuarioCreacion : Usuario Logueado
        /// FechaCreacion : Fecha actual
        /// </summary>
        protected Solicitud()
        {
            IdSolicitud = Guid.NewGuid().ToString();        
            FechaCreacion = DateTime.Now;
        }
    }
}
