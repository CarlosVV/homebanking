namespace Validation {
    export class LettersOnlyDirective {
        require = 'ngModel';

        link(scope: any, element: JQuery, attr: angular.IAttributes, ngModelCtrl: angular.INgModelController) {
            const fromUser: angular.IModelParser = (text) => {
                if (text) {
                    var transformedInput = text.toUpperCase().replace(/[^A-Za-zñÑáéíóúÁÉÍÓÚüÜ]*$/, '');

                    if (transformedInput !== text) {
                        ngModelCtrl.$setViewValue(transformedInput);
                        ngModelCtrl.$render();
                    }
                    return transformedInput;
                }
                return undefined;
            };
            ngModelCtrl.$parsers.push(fromUser);
        }
    }
}