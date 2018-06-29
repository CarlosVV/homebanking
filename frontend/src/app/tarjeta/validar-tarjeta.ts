/* tslint:disable */

declare var keyCode: any;
$(document).ready(function () {
    const $inputs = $('.container-input input');
    $inputs.focusin(function () {
        $(this).attr('placeholder', '');
    }).focusout(function () {
        $inputs.attr('placeholder', 'xxxx');
        $('.numero-tarjeta2').attr('placeholder', 'xxxxxx');

    });

    $(document).on('keyup', '#txtNumtarjetaUno', function (e: JQueryKeyEventObject) {
        if ($('#txtNumtarjetaUno').val() !== '') {
            if ($('#txtNumtarjetaUno').val().length >= 4) {
                $('#txtNumtarjetaDos').focus();
            }
        }
    });

    $(document).on('keyup', '#txtNumtarjetaDos', function (e: JQueryKeyEventObject) {
        if ($('#txtNumtarjetaDos').val() !== '') {
            if ($('#txtNumtarjetaDos').val().length >= 6) {
                $('#txtNumtarjetaTres').focus();
            }
        }
    });

    $('[data-toggle="tooltip"]').tooltip();

    $(document).on('click', '.cerrar-teclado', function () {
        $('.container-teclado').removeClass('teclado-visible');
        $('.teclado-visible-input').removeClass('fondo__clave');
    });


    $(document).on('click', '.modal-tarjetaNumero', function () {
        $('#modal-tarjeta-numero').modal({});
    });

    $(document).on('click', '.modal-tarjetaCVV2', function () {
        $('#modal-tarjetaCVV2-numero').modal({});
    });

    $(".validateForm").validate();

    $(document).on("keyup", '.validateForm', function () {
        if ($(".validateForm").valid()) {
            $(".disabled-btn-continuar").hide();
            $(".exito-pass").show();
        } else {
            $(".disabled-btn-continuar").show();
            $(".exito-pass").hide();
        }
    })

    $(document).on('click', '.modal-ayuda', function () {
        $('#modal-ayuda').modal({});
    });

    $(document).on('keyup', 'div.container-input input', function (e: any) {
        var $self = $(this);
        keyCode = e.which || e.keyCode();
        var index = $self.index('input');
        //alert(index);
        if (keyCode != 8) {
            if ($self.val().length > 3 && index == 2) {
                $(".numero-tarjeta2").focus();
            }
            else if ($self.val().length > 5 && index == 3) {
                $(".numero-tarjeta3").focus();
            }
            else if ($self.val().length > 3 && index == 4) {
                $(".cod-cvv2").focus();
            }
        } else {
            if ($self.val().length == 0 && index == 4) {
                $(".numero-tarjeta2").focus();
            }
            else if ($self.val().length == 0 && index == 3) {
                $(".numero-tarjeta1").focus();
            }
            else if ($self.val().length == 0 && index == 2) {
                $(".numero-tarjeta1").focus();
            }
        }

    });
});
