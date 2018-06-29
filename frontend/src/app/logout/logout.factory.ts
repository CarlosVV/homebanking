namespace Logout {
    export class LogoutFactory {
        constructor(private $http: angular.IHttpService, private configuracion: any, private sessionFactory: Common.SessionFactory) {
        }

        logout(usuario: any) {
            var urlApiBase = this.configuracion.urlServicioApi + '/usuario/logout';
            var tokenType = this.sessionFactory.claveDigitalTokenType;
            var accessToken = this.sessionFactory.claveDigitalAccessToken;
            var authToken = tokenType + ' ' + accessToken;

            return this.$http.post(urlApiBase, usuario, { headers: { 'Authorization': authToken } });
        }
    }
}