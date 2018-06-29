namespace RecuperarClaveDigital {
    export class IdentificacionController {
        tarjeta: Api.ITarjetaAuth = {
            ultimos4digitos: '',
            cvv2: '',
            numeroDocumentoIdentidad: ''
        };
        mensajeError: string = 'Lo sentimos la aplicación Diners Club Online se detuvo.';
        errorSistema: boolean = false;
        tarjetaIncorrecta: boolean = false;
        usuarioTieneClaveDigital: boolean = true;

        constructor(private authApi: Api.AuthApi, private sessionFactory: Common.SessionFactory, private $location: angular.ILocationService) {
        }

        validarTarjeta(): void {
            this.authApi.tokenTarjeta(this.tarjeta)
                .then((response) => {
                    if (!response) {
                        this.errorSistema = true;
                        return false;
                    }
                    this.tarjetaIncorrecta = false;
                    this.errorSistema = false;
                    this.usuarioTieneClaveDigital = true;
                    var tarjetaResponse: any = response.data;
                    if (tarjetaResponse.error) {
                        switch (tarjetaResponse.error) {
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

                        return false;
                    }
                    this.sessionFactory.tarjetaTokenType = tarjetaResponse.token_type;
                    this.sessionFactory.tarjetaAccessToken = tarjetaResponse.access_token;

                    if (tarjetaResponse.tieneClaveDigital === 'True') {
                        this.usuarioTieneClaveDigital = true;
                    } else {
                        this.usuarioTieneClaveDigital = false;
                        return false;
                    }

                    this.sessionFactory.documentoIdentidad = this.tarjeta.numeroDocumentoIdentidad;
                    this.$location.path('/recuperar-clave-digital/nueva-clave');
                    return true;
                },
                (error) => {
                    // todo: manejo de excepciones
                    if (error != null) {
                        if (error.status === 500) {
                            console.log('Error en el servicio validarTarjeta');
                            this.errorSistema = true;
                        } else if (error.status === 400) {
                            switch (error.data.error) {
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
                    }
                });
        }
    }
}