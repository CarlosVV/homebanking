namespace SolucionesFinancieras {
    interface IRouteParams extends ng.route.IRouteParamsService {
        idTipoOferta: string;
        tab: string;
    }
    export class SolucionesFinancierasController {
        bannerByCountry: string;
        tabSeleccionado = 'tab-1';

        constructor(private sessionFactory: Common.SessionFactory,
        private $routeParams: IRouteParams) {
            if (this.$routeParams.tab) {
                this.activarMenu(this.$routeParams.tab);
            }
        }

        inicializar() {
            this.processBannerByCountry();
            switch (this.$routeParams.idTipoOferta) {
                case ('1'):
                    this.tabSeleccionado = 'tab-1';
                    break;
                case ('2'): break;
                default: break;
            }
        }

        processBannerByCountry() {
            var isLocal = this.sessionFactory.geolocation.isLocal;
            this.bannerByCountry = Common.Utility.processBannerByCountry(isLocal);
        }

        activarMenu(menuItem: string): void {
            this.tabSeleccionado = menuItem;
        }

        menuSeleccionado(menuItem: string): boolean {
            return this.tabSeleccionado === menuItem;
        }
    }
}