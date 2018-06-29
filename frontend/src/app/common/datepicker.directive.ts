namespace Common {
    export class DatePicker implements angular.IDirective {
        restrict: string = 'A';
        require: string = 'ngModel';
        link = function (scope: angular.IScope, element: angular.IAugmentedJQuery, attrs: angular.IAttributes, ngModelCtrl: angular.INgModelController) {
            // $(element).on('click', () => {
            //     $('.datepicker').fadeOut();
            // });

            $(element).next('img, span').on('click', () => {
                this.bindElement(element);
            });
        };

        bindElement(element: angular.IAugmentedJQuery) {
            $(element).datepicker({
                format: 'dd/mm/yyyy',
                language: 'es',
                orientation: 'bottom auto',
                autoclose: true
            });

            $(element).datepicker('show');
        };
    }
}