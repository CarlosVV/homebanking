namespace Common {
    export class Banco {
        id: string;
        nombre: string;
    }
    export class BancoFactory {
        constructor(private $http: ng.IHttpService, private configuracion: any, private sessionFactory: Common.SessionFactory) {
        }

        get(): ng.IHttpPromise<Banco[]> {
            let tokenType = this.sessionFactory.claveDigitalTokenType;
            let accessToken = this.sessionFactory.claveDigitalAccessToken;
            let authToken = tokenType + ' ' + accessToken;

            let urlBanco = this.configuracion.urlServicioApi + '/bancos';
            return this.$http.get(urlBanco, { headers: { 'Authorization': authToken } });
        }
    }

}