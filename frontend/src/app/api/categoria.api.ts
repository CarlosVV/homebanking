namespace Api {
    export class CategoriaApi extends BaseApi {
        constructor(private $http: ng.IHttpService, private configuracion: any, sessionFactory: Common.SessionFactory) {
            super(configuracion.urlServicioApi, sessionFactory);
        }

        obtenerCategorias(): angular.IHttpPromise<ICategoria[]> {
            return this.$http.get(this.baseUrl + '/categorias');
        }
    }

    export interface ICategoria {
        idCategoria: string;
        nombreCategoria: string;
    }
}