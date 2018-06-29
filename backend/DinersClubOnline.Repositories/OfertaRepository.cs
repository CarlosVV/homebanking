using DinersClubOnline.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DinersClubOnline.Repositories
{
    public class OfertaRepository : IOfertaRepository
    {
        public Task<List<Oferta>> ListarOfertasPorUsuarioAsync(string idUsuario)
        {
            throw new NotImplementedException();
        }
    }
}