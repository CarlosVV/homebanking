using Autofac;
using DinersClubOnline.Model;
using Xunit;

namespace DinersClubOnline.Repositories.Tests
{
    public class TarjetaRepositoryTests
    {
        //[Theory(DisplayName="ObtieneTarjetas")]
        //[InlineData("10001")]
        //public void ObtenerTarjetas(string idUsuario)
        //{
        //    var tarjetaRepository = SetupIoC.AutofacContainer.Resolve<ITarjetaRepository>();
        //    var data = tarjetaRepository.ObtenerTarjetas(idUsuario);
            
        //    Assert.NotNull(data);
        //    Assert.NotNull(data.Result);
        //    Assert.True(data.Result.Count > 0);
        //}

        //[Theory(DisplayName="CambiaAlias")]
        //[InlineData("10001", "8836669", "MiAlias")]        
        //public void CambiarAlias(string idUsuario, string idTarjeta, string nuevoAlias)
        //{
        //    var tarjetaRepository = SetupIoC.AutofacContainer.Resolve<ITarjetaRepository>();
        //    var data = tarjetaRepository.CambiarAlias(idUsuario, idTarjeta, nuevoAlias);

        //    Assert.NotNull(data);
        //}

        //[Theory(DisplayName="ObtenieneUltimosMovimientos")]
        //[InlineData("8836669", 5)]
        //public void UltimosMovimientos(string idTarjeta, int cantidadMovimientos)
        //{
        //    var movimientoRepository = SetupIoC.AutofacContainer.Resolve<IMovimientoRepository>();
        //    var data = movimientoRepository.UltimosMovimientos(idTarjeta, cantidadMovimientos);

        //    Assert.NotNull(data);
        //    Assert.NotNull(data.Result);
        //    Assert.True(data.Result.Count > 0);
        //}

        ////EstadodeCuenta
        //[Theory(DisplayName="ObtieneEstadoDeCuenta")]
        //[InlineData("8836669")]
        //public void EstadodeCuenta(string idTarjeta)
        //{
        //    var tarjetaRepository = SetupIoC.AutofacContainer.Resolve<ITarjetaRepository>();
        //    var data = tarjetaRepository.ObtenerEstadoDeCuenta(idTarjeta);

        //    Assert.NotNull(data);
        //    Assert.NotNull(data.Result);
        //}
    }
}