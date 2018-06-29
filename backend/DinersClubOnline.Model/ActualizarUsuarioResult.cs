using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DinersClubOnline.Model
{
    public class ActualizarUsuarioResult
    {
        private ActualizarUsuarioResult()
        {

        }
        public static ActualizarUsuarioResult CreateEmailIncorrectoResult()
        {
            return new ActualizarUsuarioResult
            {
                Resultado = false,
                Error = ErrorActualizarUsuario.EmailIncorrecto
            };
        }
        public static ActualizarUsuarioResult CreateOperadorIncorrectoResult()
        {
            return new ActualizarUsuarioResult
            {
                Resultado = false,
                Error = ErrorActualizarUsuario.OperadorIncorrecto
            };
        }

        public static ActualizarUsuarioResult CreateUsarioNoExisteResult()
        {

            return new ActualizarUsuarioResult
            {
                Resultado = false,
                Error = ErrorActualizarUsuario.UsuarioNoExiste
            };
        }

        public static ActualizarUsuarioResult CreatNumeroIncorrectoResult()
        {
            return new ActualizarUsuarioResult
            {
                Resultado = false,
                Error = ErrorActualizarUsuario.NumeroIncorrecto
            };
        }

        public static ActualizarUsuarioResult CreateErrorGenericoResult()
        {
            return new ActualizarUsuarioResult
            {
                Resultado = false,
                Error = ErrorActualizarUsuario.ErrorGenerico
            };
        }

        public static ActualizarUsuarioResult CreatUsuarioActualizarResult()
        {
            return new ActualizarUsuarioResult
            {
                Resultado = false,
                Error = ErrorActualizarUsuario.NumeroIncorrecto
            };
        }

        public static ActualizarUsuarioResult CreateUsuarioActualizadoResult(Usuario usuarioActualizado)
        {
            if (usuarioActualizado == null) throw new ArgumentNullException("usuarioActualizado");

            return new ActualizarUsuarioResult
            {
                Resultado = true,
                UsuarioActualizado = usuarioActualizado
            };
        }

        public bool Resultado { get; private set; }
        public ErrorActualizarUsuario? Error { get; private set; }
        public Usuario UsuarioActualizado { get; private set; }


    }
}
