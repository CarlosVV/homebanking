namespace Api {
    export interface ISolicitudResponse {
        codigo: string;
    }

    export class ConfirmacionApi extends BaseApi {
        constructor(private $http: angular.IHttpService, private configuracion: any, sessionFactory: Common.SessionFactory) {
            super(configuracion.urlServicioApi, sessionFactory);
        }

        /** model: jsonFormat */
        send(endPoint: string, model: any, method: HttpVerb = 'GET', scope: Api.AuthScope = 'consultas'): angular.IPromise<any> {
            var authorization: IAuthorizationHeader;

            switch (scope) {
                case 'consultas':
                    authorization = this.getAuthorizationHeader();
                    break;
                case 'registro':
                    authorization = this.getAuthorizationByTarjetaHeader();
                    break;
                case 'operaciones':
                    authorization = this.getOperacionesAuthorization();
                    break;
            }

            return this.$http({
                method: method,
                url: this.baseUrl + endPoint,
                headers: authorization,
                data: model
            });
        }
    }

    export type HttpVerb = 'GET' | 'POST' | 'PUT' | 'DELETE' | 'PATCH';
}
