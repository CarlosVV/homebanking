namespace Login {
    export class LoginController {
        usuarioLogin: string = '';
        claveDigital: string = '';
        imagenSeguridad: any = {};
        request = {
            isValid: false
        };

        responseValidation = {
            isValid: true,
            message: ''
        };

        constructor(private authApi: Api.AuthApi,
            private sessionFactory: Common.SessionFactory,
            private geoApi: Geolocalizacion.GeoAPi,
            private $location: angular.ILocationService,
            private $rootScope: angular.IRootScopeService,
            private usuarioApi: Api.UsuarioApi,
            private facebookPictureFactory: Usuario.FacebookPictureFactory) {
            this.geoApi.forceInitialize();
        }

        login() {
            var isValid: boolean = (this.request.isValid.toString().toLowerCase() === 'true' ? true : false);

            if (!isValid) { return; }

            this.authApi.tokenClaveDigital({
                usuarioLogin: this.usuarioLogin,
                claveDigital: this.claveDigital
            }, 'consultas').then((response: angular.IHttpPromiseCallbackArg<Api.IClaveDigitalAccessToken>) => {
                this.sessionFactory.claveDigitalTokenType = response.data.token_type;
                this.sessionFactory.claveDigitalAccessToken = response.data.access_token;

                this.usuarioApi.get().then((responseUsuario) => {
                    this.facebookPictureFactory.asyncInit();
                    this.facebookPictureFactory.verifyConnectionStatus().then((responseFb: any) => {
                        this.validarImagenUsuario(responseUsuario, responseFb);
                        this.$rootScope.$broadcast('startSessionTimer', { message: 'start timer' });
                        this.$location.path('/posicion-consolidada');
                    }, (reason) => {
                        console.log(reason);
                    });
                }, (error) => {
                    // 
                });

            }, (response: angular.IHttpPromiseCallbackArg<Api.IAccessTokenError>) => {
                if (response.status === 400) {
                    this.responseValidation.message = 'Usuario o Clave Digital incorrecta. Por favor vuelva a intentarlo';
                    this.responseValidation.isValid = false;
                    $('.message-bad-request').removeClass('ng-hide');
                    return;
                }

                if (response.status === 500) {
                    this.responseValidation.message = 'Lo sentimos la aplicación Diners Club Online se detuvo';
                    $('.message-bad-request').removeClass('ng-hide');
                }
                this.responseValidation.isValid = false;
            });
        }

        validarImagenUsuario(usuario: any, facebook: any) {
            if (usuario.data.idImagen === '0') {

                // si el usuario usa imagen de fb y a la vez esta desconectado de la aplicacion, 
                // se mostrará un modal para que seleccione una nueva imagen de seguridad
                if (facebook.status !== 'connected' && usuario.data.idFacebook !== '') {
                    this.imagenSeguridad = {
                        tieneImagen: false,
                        urlImagen: ''
                    };
                } else {
                    this.imagenSeguridad = {
                        tieneImagen: true,
                        urlImagen: this.obtenerUrlImagenSeguridad(usuario.data.idImagen, usuario.data.idFacebook)
                    };
                }
            } else {
                this.imagenSeguridad = {
                    tieneImagen: true,
                    urlImagen: this.obtenerUrlImagenSeguridad(usuario.data.idImagen, usuario.data.idFacebook)
                };
            }
            this.sessionFactory.imagenSeguridad = this.imagenSeguridad;
        }

        obtenerUrlImagenSeguridad(idImagen: string, idFacebook: string): string {
            if (idImagen !== '0') {
                return this.generarUrlImagenSeguridad(idImagen);
            } else {
                return this.generarUrlImagenFacebook(idFacebook);
            }
        }

        generarUrlImagenSeguridad(idImagen: string): string {
            return Common.Utility.generarUrlImagenSeguridad(idImagen);
        }

        generarUrlImagenFacebook(idFacebook: string) {
            return Common.Utility.generarUrlImagenFacebook(idFacebook);
        }
    }
}
