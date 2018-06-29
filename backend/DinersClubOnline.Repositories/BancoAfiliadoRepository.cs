using DinersClubOnline.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DinersClubOnline.Repositories
{
    public class BancoAfiliadoRepository : IBancoAfiliadoRepository
    {
        public Task<List<BancoAfiliado>> ListarBancosAfiliadosAsync()
        {
            throw new NotImplementedException();
        }
    }
}