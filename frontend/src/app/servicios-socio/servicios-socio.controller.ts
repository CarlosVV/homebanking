namespace ServiciosSocio {
    interface IRouteParams extends angular.route.IRouteParamsService {
        tab: string;
    }

    export class ServiciosSocioController {
        tabSeleccionado = 'tab-1';
        bannerByCountry: string;

        constructor(private sessionFactory: Common.SessionFactory,
            private $routeParams: IRouteParams
        ) {
            if (this.$routeParams.tab) {
                this.activarMenu(this.$routeParams.tab);
            }
        }

        inicializar() {
            this.processBannerByCountry();
        }

        activarMenu(menuItem: string): void {
            this.tabSeleccionado = menuItem;
        }

        menuSeleccionado(menuItem: string): boolean {
            return this.tabSeleccionado === menuItem;
        }

        processBannerByCountry() {
            var isLocal = this.sessionFactory.geolocation.isLocal;
            this.bannerByCountry = Common.Utility.processBannerByCountry(isLocal);
        }
    }
}