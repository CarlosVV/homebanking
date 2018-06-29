namespace Api {
    export interface IAuthorizationHeader {
        Authorization: string;
    }

    export abstract class BaseApi {
        constructor(protected baseUrl: string, protected sessionFactory: Common.SessionFactory) {
        }

        getAuthorizationHeader() : IAuthorizationHeader {
            return {
                Authorization: `${this.sessionFactory.claveDigitalTokenType} ${this.sessionFactory.claveDigitalAccessToken}`
            };
        }

        getAuthorizationByTarjetaHeader() : IAuthorizationHeader {
            return {
                Authorization: `${this.sessionFactory.tarjetaTokenType} ${this.sessionFactory.tarjetaAccessToken}`
            };
        }

        getOperacionesAuthorization() : IAuthorizationHeader {
            return {
                Authorization: `${this.sessionFactory.claveDigitalOperacionesTokenType} ${this.sessionFactory.claveDigitalOperacionesAccessToken}`
            };
        }
    }
}