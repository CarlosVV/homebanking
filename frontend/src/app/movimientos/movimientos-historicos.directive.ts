module Movimientos {
    export class MovimientoHistoricoDirective implements angular.IDirective {
        restrict: string = 'E';
        templateUrl: string = 'app/movimientos/movimientos-historicos.html';
        controller: string = 'Movimientos';
        controllerAs: string = 'movimientosHistoricoCtrl';
        // scope = {
        //     idTarjeta: '=idtarjeta'
        // };
    }
}