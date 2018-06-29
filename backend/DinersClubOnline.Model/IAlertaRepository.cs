using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DinersClubOnline.Model
{
    public interface IAlertaRepository
    {
        Task<AlertaTarjetaUsuario> ObtenerAlertasPorTarjetaAsync(string idUsuario, string idTarjeta);

        Task<bool> ActualizarAlertasAsync(AlertaTarjetaUsuario alertas);

        Task<List<Alerta>> ObtenerTodasAlertas();
    }
}
