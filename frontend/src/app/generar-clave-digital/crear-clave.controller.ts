namespace GenerarClaveDigital {
    export class CrearClaveController {
        nombreSocio: string;
        operadoresTelefonicos: Api.IOperadorTelefonico[];
        conectadoAFacebook: boolean = false;
        mensajeError: string = '';
        usuario: Usuario = new Usuario();

        get facebookButtonName() {
            return this.conectadoAFacebook ? 'Desconectar a Facebook' : 'Conectar a Facebook';
        }

        constructor(
            private tarjetaApi: Api.TarjetaApi,
            private usuarioApi: Api.UsuarioApi,
            private authApi: Api.AuthApi,
            private facebookPictureFactory: Usuario.FacebookPictureFactory,
            private operadoresTelefonicosApi: Api.OperadoresTelefonicosApi,
            private sessionFactory: Common.SessionFactory,
            private $location: angular.ILocationService,
            private $q: angular.IQService
        ) {
        }

        init() {
            this.initializeSocio();
            this.initializeOperadoresTelefonicos();
            this.initializeFacebook();
        }

        initializeSocio() {
            this.tarjetaApi.getSocio().then((response) => {
                this.nombreSocio = response.data.nombre;
            }, (reason) => {
                console.log(reason);
            });
        }

        initializeFacebook() {
            this.facebookPictureFactory.asyncInit();
        }

        initializeOperadoresTelefonicos() {
            this.operadoresTelefonicosApi.get().then((response) => {
                this.operadoresTelefonicos = response.data;
            }, (reason) => {
                console.log(reason);
            });
        }

        getFacebookPicture() {
            if (this.usuario.facebookId !== '') {
                this.conectadoAFacebook = false;
                this.usuario.facebookId = '';
                return;
            }

            this.facebookPictureFactory.checkStatus().then((response: any) => {
                this.conectadoAFacebook = true;
                this.usuario.facebookId = response.id;
            }, (reason) => {
                console.log(reason);
            });
        }

        crearClave() {
            this.usuarioApi.post({
                claveDigital: this.usuario.claveDigital,
                idImagen: this.usuario.imagenId,
                idFacebook: this.usuario.facebookId,
                email: this.usuario.email,
                idOperadorTelefonico: this.usuario.operadorTelefonico.id,
                numeroCelular: this.usuario.numeroCelular
            }).then((response: angular.IHttpPromiseCallbackArg<string>) => {
                console.log(`Usuario creado con id: ${response.data}`);
                return this.authApi.tokenClaveDigital({
                    usuarioLogin: this.sessionFactory.documentoIdentidad,
                    claveDigital: this.usuario.claveDigital
                }, 'registro');
            }, error => {
                console.log(error);
                this.mensajeError = error;
            }).then((response: angular.IHttpPromiseCallbackArg<Api.IClaveDigitalAccessToken>) => {
                this.sessionFactory.claveDigitalAccessToken = response.data.access_token;
                this.sessionFactory.claveDigitalTokenType = response.data.token_type;
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
        }

        // setearImagenPerfil(idImagen: string) {
        //     this.imagenSeleccionada = idImagen;
        // }
    }
}