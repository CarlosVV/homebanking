using System.Collections.Generic;

namespace DinersClubOnline.Auth.Models.Auth
{
    public static class Scopes
    {
        public const string Registro = "registro";
        public const string Consultas = "consultas";
        public const string Operaciones = "operaciones";

        public static readonly IList<string> ValidTarjetaScopes = new List<string> { Registro };
        public static readonly IList<string> ValidClaveDigitalScopes = new List<string> { Consultas, Operaciones};
    }
}