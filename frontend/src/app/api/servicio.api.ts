namespace Api {
    export class ServicioApi extends BaseApi {
        constructor(private $http: ng.IHttpService, private configuracion: any, sessionFactory: Common.SessionFactory) {
            super(configuracion.urlServicioApi, sessionFactory);
        }

        obtenerServicios(idEmpresa: string): angular.IHttpPromise<IServicio[]> {
            return this.$http.get(this.baseUrl + '/servicios', { params: { idEmpresa: idEmpresa } });
        }
    }

    export interface IServicio {
        idServicio: string;
        nombreServicio: string;
        parametroServicio: string;
    }
}