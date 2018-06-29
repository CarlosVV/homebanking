namespace EstadoCuenta {
     export class EstadodeCuentaDirective implements angular.IDirective {
        restrict: string = 'E';
        templateUrl: string = 'app/estado-cuenta/estado-cuenta.html';
        controller: string = 'EstadoCuenta';
        controllerAs: string = 'estadoCuentaCtrl';
        scope = {
            idTarjeta: '=idtarjeta'
        };
    }
}