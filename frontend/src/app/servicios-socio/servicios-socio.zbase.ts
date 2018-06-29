namespace ServiciosSocio {
    export abstract class BaseServiciosSocio {
        tabSeleccionado = 'tab-1';
        constructor(private _tabSeleccionado: string) {
            this.tabSeleccionado = this._tabSeleccionado;
        }

        activarMenu(menuItem: string): void {
            this.tabSeleccionado = menuItem;
        }

        menuSeleccionado(menuItem: string): boolean {
            return this.tabSeleccionado === menuItem;
        }
    }
}