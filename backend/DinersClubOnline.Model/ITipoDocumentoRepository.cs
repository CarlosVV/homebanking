using System.Collections.Generic;
using System.Threading.Tasks;

namespace DinersClubOnline.Model
{
    public interface ITipoDocumentoRepository
    {
        Task<List<TipoDocumento>> ListarTiposDocumentosAsync();
    }
}
