namespace GenerarClaveDigital {
    export class IdentificacionController {
        tarjeta: Api.ITarjetaAuth = {
            ultimos4digitos: '',
            cvv2: '',
            numeroDocumentoIdentidad: ''
        };
        mensajeError: string = 'Lo sentimos la aplicación Diners Club Online se detuvo.';
        errorSistema: boolean = false;
        tarjetaIncorrecta: boolean = false;
        usuarioTieneClaveDigital: boolean = false;

        constructor(private sessionFactory: Common.SessionFactory, private authApi: any, private $location: angular.ILocationService) {
        }

        validarTarjeta(): void {
            this.authApi.tokenTarjeta(this.tarjeta)
                .then((response: angular.IHttpPromiseCallbackArg<Api.ITarjetaAccessToken>) => {
                    this.sessionFactory.tarjetaTokenType = response.data.token_type;
                    this.sessionFactory.tarjetaAccessToken = response.data.access_token;

                    if (response.data.tieneClaveDigital === 'True') {
                        this.usuarioTieneClaveDigital = true;
                        return false;
                    }

                    this.sessionFactory.documentoIdentidad = this.tarjeta.numeroDocumentoIdentidad;
                    this.$location.path('/generar-clave-digital/crear-clave');
                    return true;
                },
                (response: angular.IHttpPromiseCallbackArg<Api.IAccessTokenError>) => {
                    if (response.status === 500) {
                        console.log('Error en el servicio validarTarjeta');
                        this.errorSistema = true;
                    } else if (response.status === 400) {
                        switch (response.data.error) {
                            case 'invalid_scope':
                                console.log('Error en el sistema scope invalido');
                                this.errorSistema = true;
                                break;
                            case 'invalid_grant':
                                console.log('Tarjeta o Clave incorrecto');
                                this.tarjetaIncorrecta = true;
                                break;
                            default:
                                console.log('Error en el sistema');
                                this.errorSistema = true;
                        }
                    }
                });
        }
    }
}