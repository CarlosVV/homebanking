using System;

namespace DinersClubOnline.Model
{
    public class RegistrarClaveDigitalResult
    {
        private RegistrarClaveDigitalResult()
        {
        }

        public static RegistrarClaveDigitalResult CreateTarjetaNoExisteResult()
        {
            return new RegistrarClaveDigitalResult
            {
                Resultado = false,
                Error = ErrorRegistrarClaveDigital.TarjetaNoExiste
            };
        }

        public static RegistrarClaveDigitalResult CreateYaTieneClaveResult()
        {
            return new RegistrarClaveDigitalResult
            {
                Resultado = false,
                Error = ErrorRegistrarClaveDigital.YaTieneClaveDigital
            };
        }

        public static RegistrarClaveDigitalResult CreateUsuarioRegistradoResult(Usuario usuarioCreado)
        {
            if (usuarioCreado == null) throw new ArgumentNullException("usuarioCreado");

            return new RegistrarClaveDigitalResult
            {
                Resultado = true,
                UsuarioCreado = usuarioCreado
            };
        }

        public static RegistrarClaveDigitalResult CreateErrorGenericoResult()
        {
            return new RegistrarClaveDigitalResult
            {
                Resultado = false,
                Error = ErrorRegistrarClaveDigital.ErrorGenerico
            };
        }

        public bool Resultado { get; private set; }
        public ErrorRegistrarClaveDigital? Error { get; private set; }
        public Usuario UsuarioCreado { get; private set; }
    }
}