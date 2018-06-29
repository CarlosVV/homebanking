using System;
using System.Linq;
using System.Threading.Tasks;
using DinersClubOnline.Model;

namespace DinersClubOnline.FakeRepositories
{
    public class UsuarioFakeRepository : IUsuarioRepository
    {
        public Task<Usuario> ObtenerUsuarioAsync(string idUsuario)
        {
            return Task.FromResult(Data.Usuarios.SingleOrDefault(x => x.Id == idUsuario));
        }

        public Task<RegistrarClaveDigitalResult> RegistrarClaveDigitalAsync(string idTarjeta, string claveDigital, string email, string idOperadorTelefonico, string numeroCelular, string idImagen, string idFacebook)
        {
            var tarjeta = Data.TarjetasTitulares.SingleOrDefault(x => x.Id == idTarjeta);

            if (tarjeta == null)
            {
                return Task.FromResult(RegistrarClaveDigitalResult.CreateTarjetaNoExisteResult());
            }

            var socio = tarjeta.Socio;

            if (socio.TieneClaveDigital)
            {
                return Task.FromResult(RegistrarClaveDigitalResult.CreateYaTieneClaveResult());
            }

            var usuario = new Usuario
            {
                Id = (Data.Usuarios.Select(x => Int32.Parse(x.Id)).Max() + 1).ToString(),

                Alias = "",
                NumeroCelular = numeroCelular,
                IdImagen = idImagen,
                IdFacebook = idFacebook,

                //Alertas
                EmailPrincipal = email,
                EmailAlternativo = "",
                EmailSeleccionado = "",

                //Seguridad
                ClaveDigital = claveDigital,

                //Relaciones
                Socio = socio,
                OperadorTelefonico = Data.OperadoresTelefonicos.FirstOrDefault(x => x.Id == idOperadorTelefonico)
            };

            Data.Usuarios.Add(usuario);

            return Task.FromResult(RegistrarClaveDigitalResult.CreateUsuarioRegistradoResult(usuario));
        }

        public Task<bool> ActualizarClaveDigitalAsync(string idUsuario, string claveDigital)
        {
            var usuario = Data.Usuarios.SingleOrDefault(x => x.Id == idUsuario);

            if (usuario == null)
            {
                return Task.FromResult(false);
            }
            usuario.ClaveDigital = claveDigital;
            return Task.FromResult(true);
        }

        public Task<ActualizarUsuarioResult> ActualizarUsuarioAsync(string idUsuario, string emailPrincipal, string emailAlternativo, string idOperadorTelefonico, string numeroCelular)
        {
            var usuario = Data.Usuarios.SingleOrDefault(x => x.Id == idUsuario);

            if (usuario == null)
            {
                return Task.FromResult(ActualizarUsuarioResult.CreateUsarioNoExisteResult());
            }

            if (emailPrincipal == null || !emailPrincipal.Contains("@"))
            {
                return Task.FromResult(ActualizarUsuarioResult.CreateEmailIncorrectoResult());
            }

            if (emailAlternativo == null || !emailAlternativo.Contains("@"))
            {
                return Task.FromResult(ActualizarUsuarioResult.CreateEmailIncorrectoResult());
            }

            if (idOperadorTelefonico == null || !Data.OperadoresTelefonicos.Any(x => x.Id == idOperadorTelefonico))
            {
                return Task.FromResult(ActualizarUsuarioResult.CreateOperadorIncorrectoResult());
            }

            if (numeroCelular == null)
            {
                return Task.FromResult(ActualizarUsuarioResult.CreatNumeroIncorrectoResult());
            }

            usuario.EmailPrincipal = emailPrincipal;
            usuario.EmailAlternativo = emailAlternativo;
            usuario.OperadorTelefonico = Data.OperadoresTelefonicos.FirstOrDefault(x => x.Id == idOperadorTelefonico);
            usuario.NumeroCelular = numeroCelular;

            //Update Usuario

            return Task.FromResult(ActualizarUsuarioResult.CreateUsuarioActualizadoResult(usuario));
        }

        public Task<bool> ActualizarAliasAsync(string idUsuario, string alias)
        {
            var usuario = Data.Usuarios.SingleOrDefault(x => x.Id == idUsuario);

            if (usuario == null)
            {
                return Task.FromResult(false);
            }

            usuario.Alias = alias;

            return Task.FromResult(true);
        }

        public Task<bool> ActualizarImagenPerfil(string idUsuario, string idFacebook, string idImagen)
        {
            var usuario = Data.Usuarios.SingleOrDefault(x => x.Id == idUsuario);

            if(usuario == null) { return Task.FromResult(false); }

            if (!string.IsNullOrEmpty(idFacebook))
            {
                usuario.IdFacebook = idFacebook;
                usuario.IdImagen = "0";
                return Task.FromResult(true);
            }
            else 
            {
                usuario.IdFacebook = "";
                usuario.IdImagen = idImagen;
                return Task.FromResult(true);
            }
        }
    }
}