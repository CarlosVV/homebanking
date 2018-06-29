using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DinersClubOnline.Model;

namespace DinersClubOnline.FakeRepositories
{
    public class TarjetaFakeRepository : ITarjetaRepository
    {
        public Task<Tarjeta> ObtenerTarjetaAsync(string idTarjeta)
        {
            return Task.FromResult(Data.TarjetasTitulares.SingleOrDefault(x => x.Id == idTarjeta));
        }

        public Task<List<Tarjeta>> ObtenerTarjetasPorUsuarioAsync(string idUsuario)
        {
            var usuario = Data.Usuarios.FirstOrDefault(x => x.Id == idUsuario);

            if (usuario == null)
                throw new ArgumentException("El usuario con id: " + idUsuario + " no existe", idUsuario);

            return Task.FromResult(usuario.Socio.Tarjetas);
        }
        
        public Task<bool> CambiarAliasAsync(string idUsuario, string idTarjetaTitular, string nuevoAlias)
        {
            var tarjeta = Data.TarjetasTitulares.FirstOrDefault(x => x.Id == idTarjetaTitular);
   
            if (tarjeta == null)
                throw new ArgumentException("La tarjeta: " + idTarjetaTitular + " no existe");

            tarjeta.Alias = nuevoAlias;

            return Task.FromResult(true);
        }

        public Task<bool> BloquearAsync(string idTarjeta, string idMotivo)
        {
            var tarjetasTitulares = Data.TarjetasTitulares;
            var tarjetasAdicionales = Data.TarjetasTitulares.SelectMany(x=>x.Adicionales);
            var todasTarjetas = tarjetasTitulares.Concat(tarjetasAdicionales);

            var tarjeta = todasTarjetas.FirstOrDefault(t => t.Id == idTarjeta);
            tarjeta.EstadoTarjeta = EstadoTarjeta.Bloqueado;

            return Task.FromResult(true);
        }
    }
}