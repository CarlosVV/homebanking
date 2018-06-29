namespace EstadoCuenta {
    export class EstadoCuentaFactory {
        urlBase: string;

        constructor(private $http: angular.IHttpService, private configuracion: any, private $httpParamSerializerJQLike: angular.IHttpParamSerializer,
         private sessionFactory: Common.SessionFactory) {
            this.urlBase = this.configuracion.urlServicioApi + '/tarjetas';
        }

        obtenerResumenEstadoCuenta(idTarjeta: string): angular.IHttpPromise<{}> {
            let tokenType = this.sessionFactory.claveDigitalTokenType;
            let accessToken = this.sessionFactory.claveDigitalAccessToken;
            let authToken = tokenType + ' ' + accessToken;

            let urlEstadocuenta = this.urlBase + '/' + idTarjeta + '/estadoCuenta';
            return this.$http.get(urlEstadocuenta, { headers: { 'Authorization': authToken } });
        }

        obtenerLinksDescargaParaEstadoCuenta(idTarjeta: string): angular.IHttpPromise<{}> {
            let tokenType = this.sessionFactory.claveDigitalTokenType;
            let accessToken = this.sessionFactory.claveDigitalAccessToken;
            let authToken = tokenType + ' ' + accessToken;

            let urlEstadocuenta = this.urlBase + '/' + idTarjeta + '/estadoCuentaDescargar';
            return this.$http.get(urlEstadocuenta, { headers: { 'Authorization': authToken } });
        }

        descargarEstadoCuenta(idDescarga: string) {
            let urlDescarga = this.configuracion.urlDescargarPdf + '/' + idDescarga;
            window.open(urlDescarga, '_blank');
            // var blob = new Blob([urlDescarga], { type: 'application/pdf' });
            // var fileURL = URL.createObjectURL(blob);
            // window.open(fileURL, '_blank');
        }
    }
}