namespace Layout {
    export class MenuLateralController {
        tieneOfertas: boolean;
        constructor(private ofertasApi: Api.OfertaApi,
            private $window: angular.IWindowService) {
            this.verificarOfertas();
        }

        verificarOfertas() {
            this.ofertasApi.obtenerOfertas().then((item) => {
                this.tieneOfertas = item.data.length > 0;
            });
        }

        redireccionar(ruta: string) {
            this.$window.open(ruta);
        }
    }
}