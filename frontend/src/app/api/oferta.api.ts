namespace Api {
    export class OfertaApi extends BaseApi {
        constructor(private $http: angular.IHttpService,
            private configuracion: any,
            sessionFactory: Common.SessionFactory) {
            super(configuracion.urlServicioApi + '/oferta', sessionFactory);
        }

        obtenerOfertas(): angular.IHttpPromise<IOfertaModel[]> {
            return this.$http.get(this.baseUrl, { headers: this.getAuthorizationHeader() });
        }
    }

    export interface IOfertaModel {
        idTipoOferta: string;
        idTarjeta: string;
        montoLineaNueva: number;
        fechaInicio: Date;
        fechaFin: Date;
        montoOferta: number;
        tea: number;
        tcea: number;
    }
}