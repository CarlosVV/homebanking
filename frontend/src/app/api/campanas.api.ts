namespace Api {
    export interface ICampana {
        id: string;
        imagen: string;
        banner: string;
        prioridad: string;
        nombre: string;
        descripcion: string;
        fechaInicio: string;
        fechaFin: string;
        creadoPor: string;
        fechaCreacion: string;
        actualizadoPor: string;
        fechaActualizacion: string;
        activo: string;
    }

    export class CampanasApi extends BaseApi {
        constructor(private $http: ng.IHttpService, private configuracion: any, sessionFactory: Common.SessionFactory) {
            super(configuracion.urlServicioApi, sessionFactory);
        }

        getCampanas(): angular.IHttpPromise<ICampana[]> {
            return this.$http.get(this.baseUrl + '/campanas');
        }
    }
}
