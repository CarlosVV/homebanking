namespace Api {
    export class AuthApi {
        baseUrl: string;
        config: angular.IRequestShortcutConfig;

        private static authorizationData(username: string, password: string, scope: AuthScope) {
            return {
                grant_type: 'password',
                username: username,
                password: password,
                scope: scope
            };
        }

        constructor(private $http: angular.IHttpService, private configuracion: any, private $httpParamSerializerJQLike: angular.IHttpParamSerializer) {
            this.baseUrl = this.configuracion.urlAuthServicioApi;
            this.config = {
                headers: { 'Content-Type': 'application/x-www-form-urlencoded; charset=UTF-8' }
            };
        }

        tokenClaveDigital(usuario: IUsuarioClaveDigital, scope: AuthScope): angular.IHttpPromise<IClaveDigitalAccessToken | IAccessTokenError> {
            return this.$http.post(this.baseUrl + '/token-clave-digital',
                this.$httpParamSerializerJQLike(AuthApi.authorizationData(usuario.usuarioLogin, usuario.claveDigital, scope)),
                this.config);
        }

        tokenTarjeta(tarjeta: ITarjetaAuth): angular.IHttpPromise<ITarjetaAccessToken | IAccessTokenError> {
            return this.$http.post(this.baseUrl + '/token-tarjeta',
                this.$httpParamSerializerJQLike(AuthApi.authorizationData(tarjeta.numeroDocumentoIdentidad, `${tarjeta.ultimos4digitos},${tarjeta.cvv2}`, 'registro')),
                this.config);
        }
    }

    export interface IUsuarioClaveDigital {
        usuarioLogin: string;
        claveDigital: string;
    }

    export interface ITarjetaAuth {
        ultimos4digitos: string;
        cvv2: string;
        numeroDocumentoIdentidad: string;
    }

    export interface IAccessToken {
        access_token: string;
        token_type: string;
        expires_in: number;
        scope: string;
        '.issued': string;
        '.expires': string;
    }

    export interface ITarjetaAccessToken extends IAccessToken {
        tieneClaveDigital: string;
    }

    export interface IClaveDigitalAccessToken extends IAccessToken {
    }

    export interface IAccessTokenError {
        error: string;
        error_description: string;
    }

    export type AuthScope = 'registro' | 'consultas' | 'operaciones';
}