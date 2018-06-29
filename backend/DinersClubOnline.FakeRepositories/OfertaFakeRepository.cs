using DinersClubOnline.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DinersClubOnline.FakeRepositories
{
    public class OfertaFakeRepository : IOfertaRepository
    {
        public Task<List<Oferta>> ListarOfertasPorUsuarioAsync(string idUsuario)
        {
            var usuario = Data.Usuarios.FirstOrDefault(x => x.Id == idUsuario);

            if (usuario == null)
            { // throw new ArgumentException("El usuario con id: " + idUsuario + " no existe", idUsuario); 
                return Task.FromResult(new List<Oferta>());
            }

            return Task.FromResult(usuario.Socio.Ofertas);
        }
    }
}