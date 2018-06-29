(function () {
    $(document).ready(function () {
        $(document).on('blur', '#dineroEfectivoForm #montoPrestamo', function () {
            var $this = $(this);
            var monto = parseInt($this.val(), 10);
            var montoOferta = parseInt($('#montoOferta').val(), 10);

            if ($this.val() !== '' && monto < 1500) {
                $('.error-monto-menor').removeClass('ng-hide');
                $('.error-monto-mayor').addClass('ng-hide');

                $this.val(1500);
                angular.element($('#montoPrestamo')).trigger('change');
            }else if (monto > montoOferta) {
                $('.error-monto-menor').addClass('ng-hide');
                $('.error-monto-mayor').removeClass('ng-hide');

                $this.val($('#montoOferta').val());
                angular.element($('#montoPrestamo')).trigger('change');
            } else {
                $('.error-monto-menor').addClass('ng-hide');
                $('.error-monto-mayor').addClass('ng-hide');
            }
            angular.element($('#btnCalcularMontoCuota')).trigger('click');
        });
    });
})();