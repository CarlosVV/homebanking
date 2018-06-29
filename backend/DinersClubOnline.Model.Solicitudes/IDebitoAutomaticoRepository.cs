using System.Threading.Tasks;

namespace DinersClubOnline.Model.Solicitudes
{
    public interface IDebitoAutomaticoRepository
    {
        Task<DebitoAutomatico> ObtenerAsync(string id);

        Task<SolicitudResult> GuardarAsync(DebitoAutomatico modelo);
    }
}