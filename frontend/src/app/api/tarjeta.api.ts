namespace Api {
    export interface ISocio {
        nombre: string;
        segundoNombre: string;
        apellidoPaterno: string;
        apellidoMaterno: string;
    }

    export class TarjetaApi extends BaseApi {
        constructor(private $http: angular.IHttpService, private configuracion: any, sessionFactory: Common.SessionFactory) {
            super(configuracion.urlServicioApi + '/tarjeta/socio', sessionFactory);
        }

        getSocio(): angular.IHttpPromise<ISocio> {
            return this.$http.get(this.baseUrl, {
                headers: this.getAuthorizationByTarjetaHeader()
            });
        }
    }
}