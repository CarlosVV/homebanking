namespace Constancia {
    export class ConstanciaDirective {
        restrict: string = 'E';
        templateUrl: string = 'app/constancia/constancia.html';
        replace: true;
        scope = {
            content: '=content'
        };
    }
}