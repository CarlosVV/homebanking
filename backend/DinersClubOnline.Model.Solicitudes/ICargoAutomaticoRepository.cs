using System.Threading.Tasks;

namespace DinersClubOnline.Model.Solicitudes
{
    public interface ICargoAutomaticoRepository
    {
        Task<CargoAutomatico> ObtenerAsync(string id);

        Task<SolicitudResult> GuardarAsync(CargoAutomatico modelo);
    }
}