namespace MiPerfil {
    export class MiPerfilController {
        bannerByCountry: string;
        tabSeleccionado = 'tab-1';

        constructor(private sessionFactory: Common.SessionFactory) {
            //
        }

        inicializar() {
            this.processBannerByCountry();
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
