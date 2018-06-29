using System.Collections.Generic;
using System.Threading.Tasks;

namespace DinersClubOnline.Model
{
    public interface ITarjetaRepository
    {
        Task<Tarjeta> ObtenerTarjetaAsync(string idTarjeta);

        Task<List<Tarjeta>> ObtenerTarjetasPorUsuarioAsync(string idUsuario);

        Task<bool> CambiarAliasAsync(string idUsuario, string idTarjetaTitular, string nuevoAlias);

        Task<bool> BloquearAsync(string idTarjeta, string idMotivo);
    }
}