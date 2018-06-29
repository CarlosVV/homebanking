namespace Confirmacion {
    export class ConfirmacionDirective {
        restrict: string = 'EA';
        templateUrl: string = 'app/confirmacion/confirmacion.html';
        controller: string = 'Confirmacion';
        controllerAs: string = 'ConfirmacionCtrl';
        replace: true;
        scope = {
            content: '=content'
        };
    }
}