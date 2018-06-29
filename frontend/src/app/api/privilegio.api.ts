namespace Api {
    export interface IPrivilegio {
        nombre: string;
        numeroCuotasSinIntereses: number;
    }

    export interface IPrivilegiosGet {
        totalPrivilegios: number;
        resultados: IPrivilegio[];
        url: string;
    }

    export class PrivilegioApi extends BaseApi {
        constructor(private $http: angular.IHttpService, configuracion: any, sessionFactory: Common.SessionFactory) {
            super(configuracion.urlServicioApi + '/privilegios', sessionFactory);
        }

        get(latitud: number, longitud: number, distanciaMaxima: number, numeroPrivilegios?: number): angular.IHttpPromise<IPrivilegiosGet> {
            return this.$http.get(this.baseUrl, {
                params: {
                    latitud: latitud,
                    longitud: longitud,
                    distanciaMaxima: distanciaMaxima,
                    numeroPrivilegios: numeroPrivilegios
                }
            });
        }
    }
}