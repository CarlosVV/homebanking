namespace Api {
    export interface IOperadorTelefonico {
        id: string;
        nombre: string;
    }

    export class OperadoresTelefonicosApi extends BaseApi {
        constructor(private $http: angular.IHttpService, private configuracion: any, sessionFactory: Common.SessionFactory) {
            super(configuracion.urlServicioApi + '/operadores-telefonicos', sessionFactory);
        }

        get(): angular.IHttpPromise<IOperadorTelefonico[]> {
            return this.$http.get(this.baseUrl);
        }
    }
}