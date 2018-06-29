namespace Common {
    export class BancoAfiliado {
        idBanco: string;
        nombreBanco: string;
        pagoVentanilla: boolean;
        pagoInternet: boolean;
        pagoCargoEnCuenta: boolean;
        urlArchivo: string;
    }
    export class BancoAfiliadoFactory {
        constructor(private $http: ng.IHttpService, private configuracion: any, private sessionFactory: Common.SessionFactory) {
        }

        get(): ng.IHttpPromise<BancoAfiliado[]> {
            let tokenType = this.sessionFactory.claveDigitalTokenType;
            let accessToken = this.sessionFactory.claveDigitalAccessToken;
            let authToken = tokenType + ' ' + accessToken;

            let urlBanco = this.configuracion.urlServicioApi + '/formas-pago';
            return this.$http.get(urlBanco, { headers: { 'Authorization': authToken } });
        }
    }
}