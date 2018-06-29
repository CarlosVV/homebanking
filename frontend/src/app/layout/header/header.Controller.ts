namespace Layout {
    export class HeaderController {
        tabSeleccionado = 'tab-1';
        imagenSeguridad: string;

        constructor(private $location: angular.ILocationService,
            private sessionFactory: Common.SessionFactory,
            private $rootScope: angular.IRootScopeService) {
                this.verificarRuta(this.$location.path());
                this.$rootScope.$on('refrescarImagenSeguridad', (() => {
                    this.refrescarImagenSeguridad();
                }));
        }

        inicializar() {
            this.obtenerImagen();
        }

        activarMenuHeader(menuItem: string): void {
            this.tabSeleccionado = menuItem;
        }

        menuHeaderSeleccionado(menuItem: string): boolean {
            return this.tabSeleccionado === menuItem;
        }

        verificarRuta(ruta: string): void {
            this.tabSeleccionado = 'tab-1';
            if (ruta.indexOf('posicion-consolidada') >= 0) {
                this.tabSeleccionado = 'tab-1';
            } else if (ruta.indexOf('soluciones-financieras') >= 0) {
                this.tabSeleccionado = 'tab-2';
            } else if (ruta.indexOf('servicios-socio') >= 0) {
                this.tabSeleccionado = 'tab-3';
            }
            this.activarMenuHeader(this.tabSeleccionado);
        }

        redireccionar(ruta: string) {
            this.$location.path(ruta);
        }

        obtenerImagen() {
            let imagen = this.sessionFactory.imagenSeguridad.urlImagen;
            this.imagenSeguridad = imagen;
        }

        refrescarImagenSeguridad() {
            this.obtenerImagen();
        }
    }
}