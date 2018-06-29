$(function () {
    $('#btnCrearClave').hide();

    // $(document).on('click', '.modal-ayuda', function () {
    //     $('#modal-ayuda').modal();
    // });

    $(document).on('keyup', '.numeros', function () {
        validateNumbers();
    });

    $(document).on('keydown', '.numeros', function () {
        validateNumbers();
    });

    $('[data-toggle="tooltip"]').tooltip();

    $(document).on('click', '.ojo-action2', function () {
        $(this).prev().attr('type', 'text');
        $(this).prev().addClass('class-tipo');

        $('.eye1').hide();
        $('.eye2').fadeIn();

        setTimeout(function () {
            $('.input-campo #txtClaveDigital').attr('type', 'password');
            $(this).prev().removeClass('class-tipo');

            $('.eye1').fadeIn();
            $('.eye2').hide();
        }, 3000);
    });

    $(document).on('keyup', '#txtClaveDigital', function () {
        // validarClavesUsuario();
    });

    $('#txtClaveDigital').focusout(function () {
        if ($('#txtClaveDigital').val().length < 6) {
            $('.clave-incorrecta').html('Debe ingresar entre 6 y 10 dígitos');
            $('#txtClaveDigital').addClass('errorInputsClaves');
        } else {
            // validarClavesUsuario();
        }
    });

    // $(document).on('click', '.cerrar-teclado', function () {
    //     inHabilitarInputClave();
    // });

    $(document).on('click', '.ojo-action', function () {
        $(this).prev().attr('type', 'text');
        $(this).prev().addClass('class-tipo');

        $('.eye3').hide();
        $('.eye4').fadeIn();

        setTimeout(function () {
            $('.input-campo .teclado').attr('type', 'password');
            $(this).prev().removeClass('class-tipo');

            $('.eye3').fadeIn();
            $('.eye4').hide();
        }, 3000);
    });

    $(document).on('keyup', '.hide-number', function () {
        $(this).data('input').trigger('keyup');
    });

    $(document).on('keyup', '#txtClaveDigitalReingreso', function () {
        if ($('#txtClaveDigitalReingreso').val() === $('#txtClaveDigital').val() && $('#txtClaveDigitalReingreso').val().length >= 6) {
            $('.clave-incorrecta').html('');
            $('.teclado-visible-input, .container-teclado').removeClass('error-fondo');
            mostrarBtnSiguiente();
        } else {
            $('.clave-incorrecta').html('La claves ingresadas no coinciden');
            $('.teclado-visible-input, .container-teclado').addClass('error-fondo');
            ocultarBtnSiguiente();
        }
    });

    $(document).on('click', '.paso-siguiente', function () {
        // muestra la pantall de datos adicionales
        $('#myModal').modal();
    });
    $('a.btn-aceptar-ingresar').click(function (e: any) {
        if ($(this).hasClass('disabled-btn')) {
            e.preventDefault();
            return false;
        }
    });

    $(document).on('click', '.terminos-modal', function () {
        $('#modal-terminos').modal();
    });

    $(document).on('click', '.modal-teclado-seguro', function () {
        $('#modal-teclado-seguro').modal();
    });

    let emailValido = false;
    let operadorTelefonicoValido = false;
    let imagenPerfilValido = false;

    let validarDatosAdicionales = function () {
        if (emailValido && operadorTelefonicoValido && imagenPerfilValido) {
            $('#btnCrearClave').show();
            $('[data-submit="modal"]').hide();
        } else {
            $('[data-submit="modal"]').show();
            $('#btnCrearClave').hide();
        }
    };

    var validarEmail = function () {
        var re_email = /^[a-z]+[a-z0-9._-]+@[a-z]+\.[a-z.]{2,5}$/;

        if ($('[data-required="email"]').val() !== '' && re_email.test($('[data-required="email"]').val())) {
            $('[data-required="email"]').removeClass('errorInputsClaves');
            emailValido = true;
        } else {
            $('[data-required="email"]').addClass('errorInputsClaves');
            emailValido = false;
        }
        validarDatosAdicionales();
    };

    var validarOperadorTelefonico = function () {
        if ($('[data-required="operator"]').val() !== '' || $('[data-required="operator"]').val() === 'opcion-5') {
            operadorTelefonicoValido = true;
        }

        if ($('[data-required="operator"]').val() !== 'opcion-5') {
            if ($('[data-required="phone"]').val() !== '' && $('[data-required="phone"]').val().length === 9) {
                $('[data-required="phone"]').removeClass('errorInputsClaves');
                operadorTelefonicoValido = true;
            } else {
                $('[data-required="phone"]').addClass('errorInputsClaves');
                operadorTelefonicoValido = false;
            }
        } else {
            $('[data-required="phone"]').removeClass('errorInputsClaves');
            operadorTelefonicoValido = true;
        }

        validarDatosAdicionales();
    };

    var validarImagenPerfil = function () {
        $('.errorInputs').html('');
        $('#hdImagenId').val($('.image-picker').val());
        if ($('.image-picker').val() !== '' || $('input[name="facebookId"]').val() !== '') {
            $('.image_picker_selector').removeClass('errorUl');
            imagenPerfilValido = true;
        } else {
            imagenPerfilValido = false;
            $('.image_picker_selector').addClass('errorUl');
            $('.errorInputs').html('Debes seleccionar una imagen de seguridad.');
        }
        $('#imgPerfil').trigger('change');
        angular.element($('#imgPerfil')).trigger('change');
        angular.element($('#hdImagenId')).trigger('change');
        validarDatosAdicionales();
    };
    $(document).on('click', '.image_picker_selector', function () {
        validarImagenPerfil();
    });
    $('#imgPerfil').on('click', validarImagenPerfil);
    $(document).on('keyup', '#emailUsuario', validarEmail);
    $(document).on('focusout', '#emailUsuario', validarImagenPerfil);
    $(document).on('change', '#operadorTelefonico', validarOperadorTelefonico);
    $(document).on('focusout', '#numeroTelefonoClaveDigital', validarOperadorTelefonico);

    $('#hrefFacebook').click(function () {
        $('.image_picker_selector').removeClass('errorUl');
        $('.errorInputs').html('');
        $('.image-picker').trigger('input');
        validarImagenPerfil();
    });

    function validateAvatar() {
        if ($('.image-picker').val() === '' && $('input[name="facebookId"]').val() === '') {
            $('.image_picker_selector').addClass('errorUl');
            $('.errorInputs').html('Debes seleccionar una imagen de seguridad.');
            imagenPerfilValido = false;
        }

        $('.image-picker').change(function () {
            if ($(this).val() !== '') { // or this.value === 'volvo'
                $('.image_picker_selector').removeClass('errorUl');
                $('.errorInputs').html('');
            }
        });
    }

    $('.select select').change(function () {
        if ($(this).val() === 'opcion-5') {
            $('.telefono').attr('disabled', 'true');
            $('.telefono').val('');
        } else {
            $('.telefono').removeAttr('disabled');
        }
    });

    jQuery('select.image-picker').imagepicker({
        hide_select: false
    });

    jQuery('select.image-picker.show-labels').imagepicker({
        hide_select: false,
        show_label: true
    });

    jQuery('select.image-picker.limit_callback').imagepicker({
        limit_reached: function () { alert('We are full!'); },
        hide_select: false
    });

    function ValidacionTecladoVirtualPaginaMenorA1024() {
        $('#txtClaveDigital, #txtClaveDigitalReingreso').removeAttr('readonly');
    }

    function ValidacionTecladoVirtualPaginaMayorA1024() {
        $('#txtClaveDigital, #txtClaveDigitalReingreso').prop('readonly', true);
    }

    function performKeyBoard() {
        if ($(window).width() <= 1024) {
            ValidacionTecladoVirtualPaginaMenorA1024();
        } else {
            ValidacionTecladoVirtualPaginaMayorA1024();
        }
    }

    $(window).resize(function () {
        performKeyBoard();
    });

    $(document).on('click', '#txtClaveDigital, #txtClaveDigitalReingreso', function () {
        performKeyBoard();
    });

    function validateRepetedCharacter() {
        var regExpRepetidos = /^(?!.*([A-Za-z0-9])\1{1})[A-Za-z0-9]+$/;
        if ($('#txtClaveDigital').val() !== '' && regExpRepetidos.test($('#txtClaveDigital').val())) {
            $('#txtClaveDigital').removeClass('errorInputsClaves');
            $('.clave-incorrecta').html('');
            return true;
        } else {
            if ($('#txtClaveDigital').val() !== '') {
                $('#txtClaveDigital').addClass('errorInputsClaves');
                $('.clave-incorrecta').html('No debe ingresar caracteres repetidos');
            }

            return false;
        }
    }

    function checkForAscendantConsecutive(password: string) {
        if (password == null) {
            return false;
        }
        var charToCheck = password.charCodeAt(0);
        var repeatCount = 0;
        for (var i = 1; i < password.length; i++) {
            var nuevoCharacter = charToCheck + 1;
            if (password.charCodeAt(i) !== nuevoCharacter) {
                repeatCount = 0;
            }
            repeatCount++;
            charToCheck = password.charCodeAt(i);
            if (repeatCount >= 4) {
                return true;
            }
        }
        return false;
    }

    function checkForDescendantConsecutive(password: string) {
        if (password == null) {
            return false;
        }
        var charToCheck = password.charCodeAt(0);
        var repeatCount = 0;
        for (var i = 1; i < password.length; i++) {
            var nuevoCharacter = charToCheck - 1;
            if (password.charCodeAt(i) !== nuevoCharacter) {
                repeatCount = 0;
            }
            repeatCount++;
            charToCheck = password.charCodeAt(i);
            if (repeatCount >= 4) {
                return true;
            }
        }
        return false;
    }

    function validarClavesUsuario() {
        $('.clave-incorrecta').html('');
        $('#txtClaveDigital').removeClass('errorInputsClaves');
        if (!validateRepetedCharacter()) {
            ocultarBtnSiguiente();
            return false;
        }
        if (checkForAscendantConsecutive($('#txtClaveDigital').val()) || checkForDescendantConsecutive($('#txtClaveDigital').val())) {
            $('#txtClaveDigital').addClass('errorInputsClaves');
            $('.clave-incorrecta').html('No debe ingresar caracteres consecutivos');
            ocultarBtnSiguiente();
            return false;
        }
        return true;
    }

    function mostrarBtnSiguiente() {
        $('.campo_valido').fadeIn();
        $('.paso-siguiente').show();
        $('.btn-aceptar-terminos').hide();
    }

    function ocultarBtnSiguiente() {
        $('.campo_valido').hide();
        $('.paso-siguiente').hide();
        $('.btn-aceptar-terminos').show();
    }

    function validateNumbers() {
        $('.numeros').validCampoFranz('0123456789');
    }
});