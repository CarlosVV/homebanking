using Xunit;
using Autofac;
using DinersClubOnline.Model.Auth;

namespace DinersClubOnline.Repositories.Tests
{
    public class LoginServiceTests
    {
        //[Theory(DisplayName="Clave Correcta")]
        //[InlineData("36233300108169", "1234")]
        //public async void ValidarTarjetaAsync(string numeroTarjeta, string claveAtm){
        //    var loginService = SetupIoC.AutofacContainer.Resolve<ILoginService>();
        //    var result = await loginService.ValidarTarjetaAsync(numeroTarjeta, claveAtm);
            
        //    Assert.NotNull(result);
        //    Assert.True(result.EsValido);            
        //}

        [Theory(DisplayName = "Clave Correcta")]
        [InlineData("root", "qwerty")]
        public async void LoginAsync(string username, string claveDigital)
        {
            var loginService = SetupIoC.AutofacContainer.Resolve<ILoginService>();
            var result = await loginService.LoginAsync(username, claveDigital);

            Assert.NotNull(result);
            Assert.True(result.EsValido);  
        }
    }
}
