using System;
using System.Collections.Generic;

namespace DinersClubOnline.Model
{
    /// <summary>Socio es aquel que posee una tarjeta. Puede tener o no clave digital</summary>
    public class Socio
    {
        public Socio()
        {
            Tarjetas = new List<Tarjeta>();
            Ofertas = new List<Oferta>();
        }

        public string Nombre { get; set; }
        public string SegundoNombre { get; set; }
        public string ApellidoPaterno { get; set; }
        public string ApellidoMaterno { get; set; }
        public string Sexo { get; set; }
        public string EstadoCivil { get; set; }
        public string Procedencia { get; set; }

        //Seguridad
        public string NumeroDocumentoIdentidad { get; set; }
        public string TipoDocumentoIdentidad { get; set; }
        public DateTime FechaNacimiento { get; set; }

        public bool TieneClaveDigital { get; set; }

        public List<Tarjeta> Tarjetas { get; set; }

        public List<Oferta> Ofertas { get; set; }

        public string AbreviaturaNombre
        {
            get { return NombreCompleto.Substring(0, 5).ToUpper(); }
        }

        public string NombreCompleto
        {
            get { return Nombre + (String.IsNullOrEmpty(SegundoNombre) ? " " + SegundoNombre : "") + " " + ApellidoPaterno + " " + ApellidoMaterno; }
        }
    }
}