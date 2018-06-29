$(document).ready(function () {

    function agregarEmail() {
        // agregar email
        $(document).on('click', '.alertas__agregar-email', function () {
            $('.alertas__agregar-email-modal, .overlay-diners').fadeIn('fast');
        });

        // editar email ingresado
        $(document).on('click', '.alertas-table__edit', function () {
            $('.alertas__agregar-email-title').html('Modificar email');
            $('.alertas__agregar-email-modal, .overlay-diners').fadeIn('fast');
        });

        // click en overlay background
        $(document).on('click', '.overlay-diners', function () {
            $(this).fadeOut('fast');
            $('.alertas__agregar-email-modal').fadeOut('fast');
        });

        // clonar email ingresado en input a TD
        // $(document).on('click', '.alertas__agregar-email-input', function () {
        //     var emailIngresado = $(this).val();
        //     $('.alertas-table__adicional-email').html(emailIngresado);
        // });

        // click en Guardar email ingresado
        $(document).on('click', '.guardar-email', function () {
            $('.alertas__agregar-email').hide();
            $('.alertas__agregar-email, .overlay-diners, .alertas__agregar-email-modal').fadeOut('fast');
            $('.alertas-table__adicional').removeClass('bloque');
        });

        // validar email
        // $('.guardar-email').click(function() {

        //     $('.formValido').validate();

        //     if ($('.formValido').valid()) {

        //         $('.alertas__agregar-email').hide();

        //         $('.alertas__agregar-email, .overlay-diners, .alertas__agregar-email-modal').fadeOut('fast');

        //         $('.alertas-table__adicional').removeClass('bloque');

        //     }

        // });
        $(document).on('click', '.alertas__agregar-email-anular', function () {

            $('.overlay-diners, .alertas__agregar-email-modal').fadeOut('fast');

            $('.alertas-table__adicional').addClass('bloque');

            $('.alertas__agregar-email').fadeIn('fast');

            $('.alertas__agregar-email-input').val('');

            // $('.alertas__agregar-email-title').html('Agregar email')

        });
    }

    agregarEmail();

    function agregarTelefono() {
        // agregar telefono
        $(document).on('click', '.alertas__agregar-telefono', function () {
            $('.alertas__agregar-telefono-modal, .overlay-diners').fadeIn('fast');
        });

        // editar telefono ingresado
        $(document).on('click', '.alertas-table__edit-telefono', function () {
            $('.alertas__agregar-telefono-title').html('Modificar contacto');
            $('.alertas__agregar-telefono-modal, .overlay-diners').fadeIn('fast');
        });

        // click en overlay background
        $(document).on('click', '.overlay-diners', function () {
            $(this).fadeOut('fast');
            $('.alertas__agregar-telefono-modal').fadeOut('fast');
        });

        // clonar telefono ingresado en input a TD
        // $(document).on('click', '.alertas__agregar-telefono-input', function () {
        //     var telefonoIngresado = $(this).val();
        //     $('.alertas-table__adicional-telefono').html(telefonoIngresado);
        // });

        // click en Guardar telefono ingresado
        $(document).on('click', '.guardar-telefono', function () {
            $('.alertas__agregar-telefono').hide();
            $('.alertas__agregar-telefono, .overlay-diners, .alertas__agregar-telefono-modal').fadeOut('fast');
            $('.alertas-table__adicionalTelefono').removeClass('bloque');
        });

        // click en Anular telefono ingresado
        // $(document).on('click', '.alertas__agregar-telefono-anular', function () {
        //     $('.overlay-diners, .alertas__agregar-telefono-modal').fadeOut('fast');
        // $('.alertas-table__adicionalTelefono').addClass('bloque');
        // $('.alertas__agregar-telefono').fadeIn('fast');
        // $('.alertas__agregar-telefono-input').val('');
        // $('.alertas__agregar-telefono-title').html('Agregar contacto');
        // });

        $(document).on('click', '.alertas__agregar-telefono-anular', function () {

            $('.overlay-diners, .alertas__agregar-telefono-modal').fadeOut('fast');

            $('.alertas-table__adicionalTelefono').addClass('bloque');

            $('.alertas__agregar-telefono').fadeIn('fast');

            // $('.alertas__agregar-telefono-input').val('');

            // $('.alertas__agregar-telefono-title').html('Agregar contacto')

        });

        // validar telefono
        // $('.guardar-telefono').click(function() {

        //     $('.formValido2').validate();

        //     if ($('.formValido2').valid()) {

        //         $('.alertas__agregar-telefono').hide();

        //         $('.alertas__agregar-telefono, .overlay-diners, .alertas__agregar-telefono-modal').fadeOut('fast');

        //         $('.alertas-table__adicionalTelefono').removeClass('bloque');

        //     }

        // });
    }

    agregarTelefono();

});