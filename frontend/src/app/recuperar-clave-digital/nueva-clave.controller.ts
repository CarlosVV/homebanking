namespace RecuperarClaveDigital {
    export class NuevaClaveController {
        nombreSocio: string;
        claveDigital: string;
        mensajeError: string;

        constructor(
            private tarjetaApi: Api.TarjetaApi,
            private usuarioApi: Api.UsuarioApi,
            private authApi: Api.AuthApi,
            private sessionFactory: Common.SessionFactory,
            private $location: angular.ILocationService,
            private $q: angular.IQService
        ) {
        }

        init() {
            this.initializeSocio();
        }

        initializeSocio() {
            this.tarjetaApi.getSocio().then((response) => {
                this.nombreSocio = response.data.nombre;
            }, (reason) => {
                console.log(reason);
            });
        }

        cambiarClave() {
            this.usuarioApi.putClaveDigital({
                claveDigital: this.claveDigital
            }, 'registro').then(() => {
                console.log('Clave digital actualizada');
                return this.authApi.tokenClaveDigital({
                    usuarioLogin: this.sessionFactory.documentoIdentidad,
                    claveDigital: this.claveDigital
                }, 'consultas');
            }, error => {
                console.log(error);
                this.mensajeError = error;
            }).then((response: angular.IHttpPromiseCallbackArg<Api.IClaveDigitalAccessToken>) => {
                this.sessionFactory.claveDigitalTokenType = response.data.token_type;
                this.sessionFactory.claveDigitalAccessToken = response.data.access_token;
                this.sessionFactory.setPrimerIngreso();
                this.$location.path('/posicion-consolidada');
            }, (response: angular.IHttpPromiseCallbackArg<Api.IAccessTokenError>) => {
                console.log(response.data.error_description);
                switch (response.status) {
                    case 400:
                        this.mensajeError = 'Usuario o Clave Digital incorrecta. Por favor vuelva a intentarlo';
                        return;
                    default:
                        this.mensajeError = 'Lo sentimos la aplicación Diners Club Online se detuvo';
                        return;
                }
            });
        };
    }
}