using DinersClubOnline.Api.Models.Solicitudes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DinersClubOnline.Api.Models
{
    public class AlertaTarjetaUsuarioViewModel
    {
        public string IdUsuario { get; set; }

        public string IdTarjeta { get; set; }

        public bool Activo { get; set; }
        public int EmailSeleccionado { get; set; }
        public string EmailAdicional { get; set; }
        public string TelefonoAdicional { get; set; }
        public List<Diccionario> DatosCorreo { get; set; }
        public List<AlertaTarjetaViewModel> AlertasActivas { get; set; }
    }

    public class AlertaTarjetaViewModel
    {
        public string IdAlerta { get; set; }
        public bool Celular1Activo { get; set; }
        public bool Celular2Activo { get; set; }
        public bool Email1Activo { get; set; }
        public bool Email2Activo { get; set; }
        public string IdCondicionAdicional { get; set; }
    }
}
