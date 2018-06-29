using DinersClubOnline.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DinersClubOnline.FakeRepositories
{
    public class TipoDocumentoFakeRepository : ITipoDocumentoRepository
    {
        public Task<List<TipoDocumento>> ListarTiposDocumentosAsync()
        {
            return Task.FromResult(Data.TiposDocumentos);
        }
    }
}