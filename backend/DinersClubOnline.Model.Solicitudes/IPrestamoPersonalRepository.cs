using System.Threading.Tasks;

namespace DinersClubOnline.Model.Solicitudes
{
    public interface IPrestamoPersonalRepository
    {
        Task<PrestamoPersonal> ObtenerAsync(string id);

        Task<SolicitudResult> GuardarAsync(PrestamoPersonal prestamoPersonal);
    }
}
