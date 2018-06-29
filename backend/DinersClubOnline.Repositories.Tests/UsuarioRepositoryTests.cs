using Autofac;
using DinersClubOnline.Model;
using Xunit;

namespace DinersClubOnline.Repositories.Tests
{
    public class UsuarioRepositoryTests
    {
        //[Theory(DisplayName="ObtieneUsuario")]
        //[InlineData("10001")]
        //public void ObtenerUsuario(string idUsuario)
        //{
        //    var usuarioRepository = SetupIoC.AutofacContainer.Resolve<IUsuarioRepository>();
        //    var data = usuarioRepository.ObtenerUsuario(idUsuario);

        //    Assert.NotNull(data);
        //    Assert.NotNull(data.Result);
        //}

        //[Theory(DisplayName = "CambiaClaveDigital")]
        //[InlineData("root", "45646")]
        //public async void  ActualizarClaveDigital(string usuario, string claveDigital)
        //{
        //    var usuarioRepository = SetupIoC.AutofacContainer.Resolve<IUsuarioRepository>();
        //    var data = await usuarioRepository.ActualizarClaveDigital(usuario, claveDigital);

        //    Assert.True(data);
        //}

        //[Theory(DisplayName = "RegistraUsuario")]
        //[InlineData("8836669", "10001", "pass123", "mymail@com.com", "Entel", "879787987", "img001", "asdfasdf4564646654")]
        //public async void RegistrarClaveDigital(string idTarjeta, string usuario, string claveDigital, string email, string operadorTelefonico, string numeroCelular, string idImagen, string idFacebook)
        //{
        //    var usuarioRepository = SetupIoC.AutofacContainer.Resolve<IUsuarioRepository>();
        //    var data = await usuarioRepository.RegistrarClaveDigital(idTarjeta, usuario, claveDigital, email, operadorTelefonico, numeroCelular, idImagen, idFacebook);

        //    Assert.True(data);
        //}
    }
}
