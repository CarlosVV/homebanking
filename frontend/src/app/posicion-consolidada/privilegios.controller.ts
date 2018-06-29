namespace PosicionConsolidada {
    export class PrivilegiosController {
        resultado: Api.IPrivilegiosGet = {
            totalPrivilegios: 0,
            resultados: [],
            url: ''
        };

        constructor(private geolocationApi: Geolocalizacion.GeoAPi, private privilegioApi: Api.PrivilegioApi) {
        }

        init() {
            this.geolocationApi.getCurrentPosition().then(
                position => this.privilegioApi.get(position.coords.latitude, position.coords.longitude, 5, 3),
                error => console.log(error)
            ).then(result => {
                this.resultado = result.data;
            }, error => console.log(error));
        }

        irAPrivilegios() {
            window.open(this.resultado.url);
        }
    }
}