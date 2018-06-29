namespace Api {
    export class TimeZoneApi {
        constructor(private $http: angular.IHttpService, private configuracion: any) {
        }

        get(lat: number, lon: number): angular.IHttpPromise<any> {
            let baseUrl: string = this.configuracion.urlServicioApi + '/time-zone';
            return this.$http.get(baseUrl,  { params: { latitud: lat, longitud: lon}});
        }
    }
}