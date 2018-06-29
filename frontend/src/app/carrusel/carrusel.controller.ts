namespace Carrusel {
    export class CarruselController {
        constructor(private $rootScope: angular.IRootScopeService) { }

        mostrarDatosTarjeta(idTarjeta: string, tieneAdicionales: boolean) {
            this.$rootScope.$broadcast('obtenerUltimosMovimientos', { message: 'called from carruse.controller', idTarjeta: idTarjeta, tieneAdicionales: tieneAdicionales});
            this.$rootScope.$broadcast('obtenerMovimientosHistoricos', { message: 'called from carruse.controller', idTarjeta: idTarjeta });
            this.$rootScope.$broadcast('obtenerEstadoDeCuenta', { message: 'called from carruse.controller', idTarjeta: idTarjeta });
        }
    }
} 