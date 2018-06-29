namespace Validation {
    export class AlfaNumericoOnlyDirective {
        require = 'ngModel';

        link(scope: any, element: JQuery, attr: angular.IAttributes, ngModelCtrl: angular.INgModelController) {
            const fromUser: angular.IModelParser = (text) => {
                if (text) {
                    var transformedInput = text.toUpperCase().replace(/[^A-Za-z0-9]*$/, '');

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

     export class AlfaNumericoConEspecialesDirective {
        require = 'ngModel';

        link(scope: any, element: JQuery, attr: angular.IAttributes, ngModelCtrl: angular.INgModelController) {
            const fromUser: angular.IModelParser = (text) => {
                if (text) {
                    var transformedInput = text.replace(/[&']*$/, '');

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