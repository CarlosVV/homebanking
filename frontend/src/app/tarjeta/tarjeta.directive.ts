namespace Tarjeta {
    export class ValidarTarjetaDirective {
        restrict: string = 'E';
        templateUrl: string = 'app/tarjeta/validar-tarjeta.html';
        controller: string = 'Tarjeta';
        controllerAs: string = 'tarjetaCtrl';
        scope: {
            name: '@',
            email: '@',
            rate: '=',
            mostrarvalidartarjeta: '=',
            click: '&'
        };
    }
}