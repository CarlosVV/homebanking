namespace Api {
    export interface IUsuarioGet {
        idUsuario: string;
        numeroDocumentoIdentidad: string;
        tipoDocumentoIdentidad: string;
        nombre: string;
        segundoNombre: string;
        apellidoPaterno: string;
        apellidoMaterno: string;
        fechaNacimiento: string;
        sexo: string;
        estadoCivil: string;
        procedencia: string;
        emailPrincipal: string;
        emailAlternativo: string;
        emailSeleccionado: string;
        idOperadorTelefonico: string;
        numeroCelular: string;
        idImagen: string;
        idFacebook: string;
        alias: string;
    }

    export interface IUsuarioPost {
        /** Clave digital a crear */
        claveDigital: string;

        /** Id de la imagen a registrar. Debe ser un número entre 0 y 8, si es 0 corresponde a la imagen de Facebook */
        idImagen: string;

        /** Id de facebook del usuario. (Opcional) */
        idFacebook: string;

        /** Email del usuario a registrar */
        email: string;

        /** Id del operador telefónico del usuario a registrar */
        idOperadorTelefonico: string;

        /** Numero de celular del usuario a registrar */
        numeroCelular: string;
    }

    export interface IUsuarioPatchActualizar {
        /** Email del usuario a registrar */
        email: string;

        /** Id del operador telefónico del usuario a registrar */
        idOperadorTelefonico: string;

        /** Numero de celular del usuario a registrar */
        numeroCelular: string;
    }

    export interface IUsuarioPutClaveDigital {
        claveDigital: string;
    }

    export interface IUsuarioPutClaveDigitalResponse {
        fechaOperacion: string;
    }

    export interface IUsuarioPatchActualizarImagenPerfil {
        idFacebook: string;
        idImagen: string;
    }

    export class UsuarioApi extends BaseApi {
        constructor(private $http: angular.IHttpService, private configuracion: any, sessionFactory: Common.SessionFactory) {
            super(configuracion.urlServicioApi + '/usuario', sessionFactory);
        }

        /** Obtiene información del usuario */
        get(): angular.IHttpPromise<IUsuarioGet> {
            return this.$http.get(this.baseUrl, {
                headers: this.getAuthorizationHeader()
            });
        }

        /** Registra un usuario */
        post(usuario: IUsuarioPost): angular.IHttpPromise<string> {
            return this.$http.post(this.baseUrl, usuario, {
                headers: this.getAuthorizationByTarjetaHeader()
            });
        }

        /** Actualiza Usuario */
        patchUsuarioActualizar(usuarioActualizar: IUsuarioPatchActualizar, scope: AuthScope): angular.IHttpPromise<IUsuarioPutClaveDigitalResponse> {
            let headers: IAuthorizationHeader;
            switch (scope) {
                case 'registro': headers = this.getAuthorizationByTarjetaHeader();
                    break;
                case 'operaciones': headers = this.getOperacionesAuthorization();
                    break;
                default:
                    throw 'Invalid scope';
            }

            return this.$http.patch(`${this.baseUrl}/usuario`, usuarioActualizar, {
                headers: headers
            });
        }

        patchActualizarImagenPerfil(usuarioPatchActualizarImagenPerfil: IUsuarioPatchActualizarImagenPerfil): angular.IHttpPromise<boolean> {
            let urlUsuario: string = `${this.baseUrl}/imagenPerfil`;
            return this.$http.patch(
                urlUsuario,
                usuarioPatchActualizarImagenPerfil,
                { headers: this.getAuthorizationHeader() }
            );
        }

         /** Actualiza la clave digital */
        putClaveDigital(usuarioClaveDigital: IUsuarioPutClaveDigital, scope: AuthScope): angular.IHttpPromise<string> {
            let headers: IAuthorizationHeader;
            switch (scope) {
                case 'registro': headers = this.getAuthorizationByTarjetaHeader();
                    break;
                case 'operaciones': headers = this.getOperacionesAuthorization();
                    break;
                default:
                    throw 'Invalid scope';
            }

            return this.$http.put(`${this.baseUrl}/clave-digital`, usuarioClaveDigital, {
                headers: headers
            });
        }

           /** Obtiene información del usuario */
        actualizarAliasUsuario(alias: string): angular.IHttpPromise<boolean> {
            let urlUsuario = `${this.baseUrl}/cambiar-alias/?alias=${alias}`;
            return this.$http.patch(urlUsuario, {params: {'alias': alias}}, {
                headers: this.getAuthorizationHeader()
            });
        }
    }
}
