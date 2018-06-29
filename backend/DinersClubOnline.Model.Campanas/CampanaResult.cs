using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DinersClubOnline.Model.Campanas
{
    public class CampanaResult
    {
        public bool Resultado { get; private set; }
        public string Id { get; private set; }
        public DateTime FechaCreacion { get; private set; }

        private CampanaResult()
        {
        }

        public static CampanaResult CreateCampanaResult(string id, DateTime fechaCreacion)
        {
            return new CampanaResult
            {
                Resultado = true,
                Id = id,
                FechaCreacion = fechaCreacion
            };
        }

        public static CampanaResult CreateErrorResult()
        {
            return new CampanaResult
            {
                Resultado = false
            };
        }

    }
}
