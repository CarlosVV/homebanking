using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DinersClubOnline.Model
{
    public class AlertaTarjetaUsuario
    {
        public string IdUsuario { get; set; }

        public string IdTarjeta { get; set; }

        public bool Activo { get; set; }
        public int EmailSeleccionado { get; set; }
        public string EmailAdicional { get; set; }
        public string TelefonoAdicional { get; set; }
        public List<AlertaTarjeta> AlertasActivas { get; set; }
    }
}
