namespace Validation {
    export class NumbersOnlyDirective {
        require = 'ngModel';

        link(scope: angular.IScope, element: JQuery, attr: angular.IAttributes, ngModelCtrl: angular.INgModelController) {
            const fromUser: angular.IModelParser = (text) => {
                if (text) {
                    var transformedInput = text.replace(/[^0-9]/g, '');

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

     export class DecimalOnlyDirective {
        require = 'ngModel';

        link(scope: angular.IScope, element: JQuery, attr: angular.IAttributes, ngModelCtrl: angular.INgModelController) {
            const fromUser: angular.IModelParser = (text) => {
                if (text) {
                    var transformedInput = text.replace(/[^0-9\.]/g, '');
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