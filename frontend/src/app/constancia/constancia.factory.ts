namespace Constancia {
     export class ConstanciaFactory {
        constructor(private $http: angular.IHttpService, private configuracion : any, private sessionFactory: Common.SessionFactory) {
        }

         descargarPdfSolicitud(constancia : IConstanciaModel) {
            var urlBase = this.configuracion.urlServicioApi + '/solicitudes/pdf';
            var tokenType = this.sessionFactory.claveDigitalTokenType;
            var accessToken = this.sessionFactory.claveDigitalAccessToken;
            var authToken = tokenType + ' ' + accessToken;

            return this.$http.post(urlBase, constancia, { headers: { 'Authorization': authToken, 'Content-type': 'application/json' }, responseType: 'arraybuffer' });
        }
     }
}