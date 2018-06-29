namespace Common {
    export class TipoDocumento {
        idTipoDocumento: string;
        nombreTipoDocumento: string;
    }
    export class TipoDocumentoFactory {
        constructor(private $http: ng.IHttpService, private configuracion: any, private sessionFactory: Common.SessionFactory) {
        }

        get(): ng.IHttpPromise<TipoDocumento[]> {
            let tokenType = this.sessionFactory.claveDigitalTokenType;
            let accessToken = this.sessionFactory.claveDigitalAccessToken;
            let authToken = tokenType + ' ' + accessToken;

            let urlTipoDocumento = this.configuracion.urlServicioApi + '/tipos-documentos';
            return this.$http.get(urlTipoDocumento, { headers: { 'Authorization': authToken } });
        }
    }

}