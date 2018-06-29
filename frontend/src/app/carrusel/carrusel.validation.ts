(function () {
    $(document).ready(function () {
        $(document).on('click', '#Carousel-Diners a.right', function () {
            mostrarSiguienteTarjeta();
        });

        $(document).on('click', '#Carousel-Diners a.left', function () {
            mostrarSiguienteTarjeta();
        });

        function mostrarSiguienteTarjeta() {
            $('#Carousel-Diners ol li').each(function (index: number, obj: any) {
                var nextClick: boolean = false;

                if ($(this).hasClass('active')) { nextClick = true; }
                if (nextClick) {
                    angular.element(jQuery($(this))).triggerHandler('click');
                    angular.element(document.querySelectorAll('#txtxPassword')).trigger('click');
                }
            });
        }
    });
})();