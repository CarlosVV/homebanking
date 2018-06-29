namespace Api {
    export class EmpresaApi extends BaseApi {
        constructor(private $http: ng.IHttpService, private configuracion: any, sessionFactory: Common.SessionFactory) {
            super(configuracion.urlServicioApi, sessionFactory);
        }

        obtenerEmpresas(idCategoria: string): angular.IHttpPromise<IEmpresa[]> {
            return this.$http.get(this.baseUrl + '/empresas', { params: { idCategoria: idCategoria } });
        }
    }

    export interface IEmpresa {
        idEmpresa: string;
        nombreEmpresa: string;
    }
}