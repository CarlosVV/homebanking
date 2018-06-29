using DinersClubOnline.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DinersClubOnline.FakeRepositories
{
    public class AlertaFakeRepository : IAlertaRepository
    {
        public Task<AlertaTarjetaUsuario> ObtenerAlertasPorTarjetaAsync(string idUsuario, string idTarjeta)
        {
            var data = Data.TarjetasTitulares.FirstOrDefault(m => m.Id == idTarjeta);
            if (data == null)
            {
                return Task.FromResult(new AlertaTarjetaUsuario());
            }

            return Task.FromResult(data.Alertas);
        }

        public Task<bool> ActualizarAlertasAsync(AlertaTarjetaUsuario alertas)
        {
            Data.TarjetasTitulares.ForEach(tt =>
            {
                if (tt.Id == alertas.IdTarjeta)
                {
                    tt.Alertas = alertas;
                }
            });

            return Task.FromResult(true);
        }

        public Task<List<Alerta>> ObtenerTodasAlertas()
        {
            var data = Data.Alertas;
            
            return Task.FromResult(data);
        }
    }
}
