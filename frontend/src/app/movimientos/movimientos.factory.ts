module Movimientos {
    export class MovimientosFactory extends Api.BaseApi {
        constructor(private $http: angular.IHttpService, private configuracion: any, sessionFactory: Common.SessionFactory) {
            super(configuracion.urlServicioApi + '/tarjetas', sessionFactory);
        }

        obtenerMovimientos(movimientosFilter: any) {
            var urlBase = this.configuracion.urlServicioApi + '/tarjetas';
            var tokenType = this.sessionFactory.claveDigitalTokenType;
            var accessToken = this.sessionFactory.claveDigitalAccessToken;
            var authToken = tokenType + ' ' + accessToken;

            var urlmovimientos = urlBase + '/' + movimientosFilter.tarjetaId + '/movimientos';
            return this.$http.get(urlmovimientos, { headers: { 'Authorization': authToken }, params: movimientosFilter });
        }

        obtenerUltimosMovimientos(ultimosMovimientosFilter: any) {
            var urlBase = this.configuracion.urlServicioApi + '/tarjetas';
            var tokenType = this.sessionFactory.claveDigitalTokenType;
            var accessToken = this.sessionFactory.claveDigitalAccessToken;
            var authToken = tokenType + ' ' + accessToken;

            var urlUltimosMovimientos = urlBase + '/' + ultimosMovimientosFilter.tarjetaId + '/movimientos/ultimos';
            return this.$http.get(urlUltimosMovimientos, { headers: { 'Authorization': authToken }, params: { Movimientos: ultimosMovimientosFilter.cantidadMovimientos } });
        }

        obtenerTarjetaUltimosMovimientos(ultimosMovimientosFilter: any) {
            var urlBase = this.configuracion.urlServicioApi + '/tarjetas';
            var tokenType = this.sessionFactory.claveDigitalTokenType;
            var accessToken = this.sessionFactory.claveDigitalAccessToken;
            var authToken = tokenType + ' ' + accessToken;

            var urlUltimosMovimientos = urlBase + '/' + ultimosMovimientosFilter.tarjetaId;
            return this.$http.get(urlUltimosMovimientos, { headers: { 'Authorization': authToken } });
        }

        descargarMovimientosHistoricos(movimientosFilter: any) {
            var urlBase = this.configuracion.urlServicioApi + '/tarjetas';
            var tokenType = this.sessionFactory.claveDigitalTokenType;
            var accessToken = this.sessionFactory.claveDigitalAccessToken;
            var authToken = tokenType + ' ' + accessToken;

            var urlmovimientos = urlBase + '/' + movimientosFilter.tarjetaId + '/descargarMovimientosHistoricos';

            return this.$http.get(urlmovimientos, { headers: { 'Authorization': authToken, 'Content-type': 'application/json' }, params: movimientosFilter, responseType: 'arraybuffer' });
        }

        descargarPdfMovimientosHistoricos(movimientosFilter: any) {
            var urlBase = this.configuracion.urlServicioApi + '/tarjetas';
            var tokenType = this.sessionFactory.claveDigitalTokenType;
            var accessToken = this.sessionFactory.claveDigitalAccessToken;
            var authToken = tokenType + ' ' + accessToken;

            var urlmovimientos = urlBase + '/' + movimientosFilter.tarjetaId + '/descargarPdfMovimientosHistoricos';

            return this.$http.get(urlmovimientos, { headers: { 'Authorization': authToken, 'Content-type': 'application/json' }, params: movimientosFilter, responseType: 'arraybuffer' });
        }

        enviarCorreo(model: any) {
            var urlBase = this.configuracion.urlServicioApi + '/tarjetas';
            var urlmovimientos = urlBase + '/' + model.id + '/enviar-correo';

            return this.$http.post(urlmovimientos, model, { headers: this.getAuthorizationHeader()});
        }
    }
}