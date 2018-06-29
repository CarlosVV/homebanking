namespace DinersClubOnline.Model.Auth
{
    public class LoginResult
    {
        public bool EsValido { get; private set; }
        public string IdUsuario { get; private set; }

        private LoginResult()
        {
        }

        public static LoginResult CreateInvalido()
        {
            return new LoginResult
            {
                EsValido = false
            };
        }

        public static LoginResult CreateValido(string idUsuario)
        {
            return new LoginResult
            {
                EsValido = true,
                IdUsuario = idUsuario
            };
        }

    }
}