namespace Api {
    export class WeatherApi {
        constructor(private $http: angular.IHttpService, private configuracion: any) {
        }

        get(lat: number, lon: number): angular.IHttpPromise<any> {
            let baseUrl: string = this.configuracion.urlServicioApi + '/open-weather-map';
            return this.$http.get(baseUrl,  { params: { latitud: lat, longitud: lon}});
        }
    }
}