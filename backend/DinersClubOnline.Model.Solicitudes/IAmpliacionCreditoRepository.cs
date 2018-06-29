using System.Threading.Tasks;

namespace DinersClubOnline.Model.Solicitudes
{
    public interface IAmpliacionCreditoRepository
    {
        Task<AmpliacionCredito> ObtenerAsync(string id);

        Task<SolicitudResult> GuardarAsync(AmpliacionCredito modelo);
    }
}