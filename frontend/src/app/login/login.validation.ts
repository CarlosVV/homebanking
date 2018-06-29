declare var showKeyboard: any;

(function () {
    $(document).ready(function () {

        function ValidacionTecladoVirtualPaginaMenorA1024() {
            $('.container-teclado').removeClass('teclado-visible-campos');
            $('.teclado-visible-input').removeClass('fondo__clave');
            $('#loginForm #txtLoginClaveDigital').removeAttr('readonly');
        }

        function ValidacionTecladoVirtualPaginaMayorA1024() {
            $('.container-teclado').fadeIn('fast');
            $('.container-teclado').addClass('teclado-visible-campos');
            $('.teclado-visible-input').addClass('fondo__clave');
            $('#loginForm #txtLoginClaveDigital').prop('readonly', true);
        }

        $(window).resize(function () {
            if ($(window).width() <= 1024) {
                ValidacionTecladoVirtualPaginaMenorA1024();
            } else {
                $('#loginForm #txtLoginClaveDigital').prop('readonly', true);
            }
        });

        $(document).on('click', '#txtLoginClaveDigital', function () {
            if ($(window).width() <= 1024) {
                ValidacionTecladoVirtualPaginaMenorA1024();
            } else {
                ValidacionTecladoVirtualPaginaMayorA1024();
            }
        });

        $(document).on('click', '.cerrar-teclado', function () {
            $('.container-teclado').fadeOut('fast');
            $('.container-teclado').removeClass('teclado-visible-campos');
            $('.teclado-visible-input').removeClass('fondo__clave');
        });

        $(document).on('click', '#loginSubmit', function () {
            validarFormLoginUsuarioClaveDigital();
            if (!validarCaracteresRepetidosConsecutivos()) { return; }
            if (!validarCaracteresRepetidosConsecutivosAscendentes()) { return; }
            if (!validarCaracteresRepetidosConsecutivosDescendentes()) { return; }
        });

        $(document).on('keyup', '#txtUsuario', function () {
            validarFormLoginUsuarioKeyUp();
        });

        $(document).on('keyup', '#txtLoginClaveDigital', function () {
            validarFormLoginPasswordKeyUp();
        });

        $(document).on('click', 'li.delete', function () {
            validarFormLoginPasswordLengthCero();
        });

        function validarFormLoginUsuarioKeyUp() {
            validarFormLoginUsuarioLengthCero();

            $('.message-bad-request').addClass('ng-hide');
            $('.message-inputs-empty').addClass('ng-hide');
            $('.message-repeated-consecutive-characters').addClass('ng-hide');
            $('.message-repeated-ascending-consecutive-characters').addClass('ng-hide');
            $('.message-repeated-descending-consecutive-characters').addClass('ng-hide');

            if ($('#txtUsuario').val().length >= 1 && $('#txtUsuario').val().length < 6) {
                $('.usuario-error').removeClass('ng-hide');
            } else {
                $('.usuario-error').addClass('ng-hide');
            }
        };

        function validarFormLoginPasswordKeyUp() {
            validarFormLoginPasswordLengthCero();

            $('.message-bad-request').addClass('ng-hide');
            $('.message-inputs-empty').addClass('ng-hide');
            $('.message-repeated-consecutive-characters').addClass('ng-hide');
            $('.message-repeated-ascending-consecutive-characters').addClass('ng-hide');
            $('.message-repeated-descending-consecutive-characters').addClass('ng-hide');

            if ($('#txtLoginClaveDigital').val().length >= 1 && $('#txtLoginClaveDigital').val().length < 6) {
                $('#txtLoginClaveDigital').addClass('error-campos-login-publico');
                $('.password-error').removeClass('ng-hide');
            } else {
                $('.password-error').addClass('ng-hide');
            }
        };

        function validarFormLoginUsuarioLengthCero() {
            if ($('#txtUsuario').val().length === 0) {
                $('#txtUsuario').removeClass('error-campos-login-publico');
                $('.usuario-error').addClass('ng-hide');
            }
        };

        function validarFormLoginPasswordLengthCero() {
            if ($('#txtLoginClaveDigital').val() != null && $('#txtLoginClaveDigital').val().length === 0) {
                $('#txtLoginClaveDigital').removeClass('error-campos-login-publico');
                $('.password-error').addClass('ng-hide');
                return true;
            } else {
                return false;
            }
        };

        function validarFormLoginUsuarioClaveDigital() {
            $('.message-bad-request').addClass('ng-hide');

            if ($('#txtUsuario').val() === '' || $('#txtLoginClaveDigital').val() === '') {
                $('.message-inputs-empty').removeClass('ng-hide');
                $('.message-inputs-empty').html('Usuario o Clave Digital incorrecta. Por favor vuelva a intentarlo');
            } else {
                $('.message-inputs-empty').addClass('ng-hide');
            }
        };

        function validarCaracteresRepetidosConsecutivos() {
            var regExpRepetidos = /^(?!.*([A-Za-z0-9])\1{1})[A-Za-z0-9]+$/;

            if ($('#txtUsuario').val().length === 0 && $('#txtLoginClaveDigital').val().length === 0) {
                $('#txtIsValid').val('false');
                angular.element(jQuery($('#txtIsValid'))).triggerHandler('input');
                angular.element(document.querySelectorAll('#txtIsValid')).trigger('change');
                $('.message-repeated-consecutive-characters').addClass('ng-hide');
                return false;
            }

            if ($('#txtLoginClaveDigital').val().length === 0) {
                $('#txtIsValid').val('false');
                angular.element(jQuery($('#txtIsValid'))).triggerHandler('input');
                angular.element(document.querySelectorAll('#txtIsValid')).trigger('change');
                return false;
            }

            if ($('.teclado').val() !== '' && regExpRepetidos.test($('.teclado').val())) {
                $('#txtIsValid').val('true');
                angular.element(jQuery($('#txtIsValid'))).triggerHandler('input');
                angular.element(document.querySelectorAll('#txtIsValid')).trigger('change');
                $('.message-repeated-consecutive-characters').addClass('ng-hide');
                return true;
            } else {
                $('#txtIsValid').val('false');
                angular.element(jQuery($('#txtIsValid'))).triggerHandler('input');
                angular.element(document.querySelectorAll('#txtIsValid')).trigger('change');
                $('.message-repeated-consecutive-characters').removeClass('ng-hide');
                return false;
            }
        };

        function validarCaracteresRepetidosConsecutivosAscendentes() {
            var $password = $('#txtLoginClaveDigital');
            var $usuario = $('#txtUsuario');

            if ($usuario.val().length === 0 && $password.val().length === 0) {
                $('#txtIsValid').val('false');
                angular.element(jQuery($('#txtIsValid'))).triggerHandler('input');
                angular.element(document.querySelectorAll('#txtIsValid')).trigger('change');
                $('.message-repeated-ascending-consecutive-characters').addClass('ng-hide');
                return false;
            }

            if ($password.val().length === 0) {
                $('#txtIsValid').val('false');
                angular.element(jQuery($('#txtIsValid'))).triggerHandler('input');
                angular.element(document.querySelectorAll('#txtIsValid')).trigger('change');
                return false;
            }

            if (validateRepeatedAscendingConsecutiveCharacters($password.val())) {
                $('#txtIsValid').val('false');
                angular.element(jQuery($('#txtIsValid'))).triggerHandler('input');
                angular.element(document.querySelectorAll('#txtIsValid')).trigger('change');
                $('.message-repeated-ascending-consecutive-characters').removeClass('ng-hide');
                return false;
            } else {
                $('#txtIsValid').val('true');
                angular.element(jQuery($('#txtIsValid'))).triggerHandler('input');
                angular.element(document.querySelectorAll('#txtIsValid')).trigger('change');
                $('.message-repeated-ascending-consecutive-characters').addClass('ng-hide');
                return true;
            }
        }

        function validarCaracteresRepetidosConsecutivosDescendentes() {
            var $password = $('#txtLoginClaveDigital');
            var $usuario = $('#txtUsuario');

            if ($usuario.val().length === 0 && $password.val().length === 0) {
                $('#txtIsValid').val('false');
                angular.element(jQuery($('#txtIsValid'))).triggerHandler('input');
                angular.element(document.querySelectorAll('#txtIsValid')).trigger('change');
                $('.message-repeated-descending-consecutive-characters').addClass('ng-hide');
                return false;
            }

            if ($password.val().length === 0) {
                $('#txtIsValid').val('false');
                angular.element(jQuery($('#txtIsValid'))).triggerHandler('input');
                angular.element(document.querySelectorAll('#txtIsValid')).trigger('change');
                return false;
            }

            if (validateRepeatedDescendingConsecutiveCharacters($password.val())) {
                $('#txtIsValid').val('false');
                angular.element(jQuery($('#txtIsValid'))).triggerHandler('input');
                angular.element(document.querySelectorAll('#txtIsValid')).trigger('change');
                $('.message-repeated-descending-consecutive-characters').removeClass('ng-hide');
                return false;
            } else {
                $('#txtIsValid').val('true');
                angular.element(jQuery($('#txtIsValid'))).triggerHandler('input');
                angular.element(document.querySelectorAll('#txtIsValid')).trigger('change');
                $('.message-repeated-descending-consecutive-characters').addClass('ng-hide');
                return true;
            }
        }

        function validateRepeatedAscendingConsecutiveCharacters(password: string) {
            var charToCheck = password.charCodeAt(0);
            var repeatCount = 0;
            var limit = 4;

            for (var i = 1; i < password.length; i++) {
                var nuevoCharacter = charToCheck + 1;
                if (password.charCodeAt(i) !== nuevoCharacter) {
                    repeatCount = 0;
                }
                repeatCount++;
                charToCheck = password.charCodeAt(i);
                if (repeatCount >= limit) {
                    return true;
                }
            }
            return false;
        }

        function validateRepeatedDescendingConsecutiveCharacters(password: string) {
            var charToCheck = password.charCodeAt(0);
            var repeatCount = 0;
            var limit = 4;

            for (var i = 1; i < password.length; i++) {
                var nuevoCharacter = charToCheck - 1;
                if (password.charCodeAt(i) !== nuevoCharacter) {
                    repeatCount = 0;
                }
                repeatCount++;
                charToCheck = password.charCodeAt(i);
                if (repeatCount >= limit) {
                    return true;
                }
            }
            return false;
        }
    });
})();