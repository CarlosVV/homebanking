namespace Common {
    export function Ready($parse: any): angular.IDirective {
        return {
            restrict: 'A',
            link: function (scope: angular.IScope, element: angular.IAugmentedJQuery, attrs: any) {
                $(element).ready(function () {
                    if (!scope.$$phase) {
                        scope.$apply(function () {
                            var func = $parse(attrs.elemReady);
                            func(scope);
                        });
                    } else {
                        var func = $parse(attrs.elemReady);
                        func(scope);
                    }
                });
            }
        };
    }
}