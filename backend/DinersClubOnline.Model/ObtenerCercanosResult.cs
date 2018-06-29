using System;
using System.Collections.Generic;
using System.Linq;

namespace DinersClubOnline.Model
{
    public class ObtenerCercanosResult
    {
        public int TotalPrivilegios { get; set; }
        public List<Privilegio> Resultados { get; private set; }
        public string Url { get; set; }

        private ObtenerCercanosResult()
        {
            Resultados = new List<Privilegio>();
        }

        public static ObtenerCercanosResult CreateObtenerCercanosResult(int totalPrivilegios, IEnumerable<Privilegio> resultados, string url)
        {
            if (resultados == null) throw new ArgumentNullException("resultados");

            return new ObtenerCercanosResult
            {
                TotalPrivilegios = totalPrivilegios,
                Resultados = resultados.ToList(),
                Url = url
            };
        }
    }
}