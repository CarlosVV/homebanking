namespace Api {
    export class AlertaApi extends BaseApi {
        constructor(private $http: angular.IHttpService,
            private configuracion: any,
            sessionFactory: Common.SessionFactory) {
            super(configuracion.urlServicioApi, sessionFactory);
        }

        obtenerAlertasTodas(): angular.IHttpPromise<Alerta.IAlerta[]> {
            return this.$http.get(this.baseUrl + '/alertas', { headers: this.getAuthorizationHeader() });
        }

        obtenerAlertasTarjeta(idTarjeta: string): angular.IHttpPromise<Alerta.IAlertaTarjetaUsuario> {
            return this.$http.get(`${this.baseUrl}/tarjetas/${idTarjeta}/alertas`, { headers: this.getAuthorizationHeader() });
        }

        guardarAlertaTarjeta(modelo: Alerta.IAlertaTarjetaUsuario): angular.IHttpPromise<boolean> {
            return this.$http.put(this.baseUrl + '/alertas', modelo, { headers: this.getAuthorizationHeader });
        }
    }
}