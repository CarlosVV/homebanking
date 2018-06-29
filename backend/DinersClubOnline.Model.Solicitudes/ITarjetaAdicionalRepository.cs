using System.Threading.Tasks;

namespace DinersClubOnline.Model.Solicitudes
{
    public interface ITarjetaAdicionalRepository
    {
        Task<TarjetaAdicional> ObtenerAsync(string id);

        Task<SolicitudResult> GuardarAsync(TarjetaAdicional modelo);
    }
}
