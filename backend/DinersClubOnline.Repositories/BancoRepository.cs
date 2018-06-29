using DinersClubOnline.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DinersClubOnline.Repositories
{
    public class BancoRepository : IBancoRepository
    {
        public Task<List<Banco>> ListarBancosAsync()
        {
            throw new NotImplementedException();
        }
    }
}