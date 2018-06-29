namespace Common {
    export class TipoPago {
        id: string;
        nombre: string;
    }
    export class TipoPagoFactory {
        constructor(private $http: ng.IHttpService, private configuracion: any, private sessionFactory: Common.SessionFactory) {
        }

        get(): ng.IHttpPromise<TipoPago[]> {
            let tokenType = this.sessionFactory.claveDigitalTokenType;
            let accessToken = this.sessionFactory.claveDigitalAccessToken;
            let authToken = tokenType + ' ' + accessToken;

            let urlBanco = this.configuracion.urlServicioApi + '/tiposPago';
            return this.$http.get(urlBanco, { headers: { 'Authorization': authToken } });
        }
    }

}