namespace Validation {
    export class EmailValidacionDirective {
        require = 'ngModel';

        link(scope: any, element: JQuery, attr: angular.IAttributes, ngModelCtrl: angular.INgModelController) {
            const fromUser: angular.IModelParser = (text) => {
                if (text) {
                    var transformedInput = text.replace(/[^A-Za-z0-9._@-]*$/, '');

                    if (transformedInput !== text) {
                        scope.esAlfanumerico = false;
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