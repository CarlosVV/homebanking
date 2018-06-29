/* tslint:disable */
$(function () {

    function showKeyboard($keyboard: JQuery) {
        const keys = '';
        $keyboard.find('[data-number]').remove();
        var classes: any = { 1: 'border-left', 2: '', 3: 'border-right', 4: 'border-left', 5: 'border-left', 6: '', 7: 'border-right', 8: 'border-left', 9: 'border-right', 0: '' };
        var num = parseInt('' + Math.floor((Math.random() * 9) + 0));

        for (var i = 0; i <= 9; i++) {
            var borderClass = classes[i];
            $keyboard.prepend('<li class="symbol" data-number><span class="off ' + borderClass + '">' + num + '</span></li>');
            num = (num >= 9) ? 0 : (num + 1);
        }
    };

    function mostrarKeyBoardClaveDigital(keyboardElement: JQuery) {
        if ($(window).width() <= 1024) {
            // todo responsive modo
            // $('#txtClaveDigital').attr('readonly', 'false');
        } else {
            $('.container-teclado').addClass('teclado-visible-campos');
            $('.teclado-visible-input').addClass('fondo__clave');
            $('.container-teclado2').removeClass('teclado-visible-campos');
            $('.teclado-visible-input2').removeClass('fondo__clave2');
            showKeyboard(keyboardElement);
        }
    }

    function mostrarKeyBoardClaveDigitalReingreso(keyboardElement: JQuery) {
        if ($(window).width() <= 1024) {
            // todo responsive modo
            // $('#txtClaveDigitalReingreso').attr('readonly', 'false');
        } else {
            $('.container-teclado').removeClass('teclado-visible-campos');
            $('.teclado-visible-input').removeClass('fondo__clave');
            $('.container-teclado2').addClass('teclado-visible-campos');
            $('.teclado-visible-input2').addClass('fondo__clave2');
            showKeyboard(keyboardElement);
        }
    }

    function mostrarKeyboardCambioClaveDigitalAntiguaClave(keyboardElement: JQuery) {
        if ($(window).width() <= 1024) {
            // todo responsive modo
            // $('#txtClaveDigitalReingreso').attr('readonly', 'false');
        } else {
            $('.container-teclado').addClass('contenido-teclado-visible');
            $('.teclado-visible-input').addClass('fondo__clave');
            $('.container-teclado2').removeClass('contenido-teclado-visible');
            $('.teclado-visible-input2').removeClass('fondo__clave');
            $('.container-teclado3').removeClass('contenido-teclado-visible');
            $('.teclado-visible-input3').removeClass('fondo__clave');
            showKeyboard(keyboardElement);
        }
    }

    function mostrarKeyboardCambioClaveDigitalNuevaClave(keyboardElement: JQuery) {
        if ($(window).width() <= 1024) {
            // todo responsive modo
            // $('#txtClaveDigitalReingreso').attr('readonly', 'false');
        } else {
            $('.container-teclado').removeClass('contenido-teclado-visible');
            $('.teclado-visible-input').removeClass('fondo__clave');
            $('.container-teclado2').addClass('contenido-teclado-visible');
            $('.teclado-visible-input2').addClass('fondo__clave');
            $('.container-teclado3').removeClass('contenido-teclado-visible');
            $('.teclado-visible-input3').removeClass('fondo__clave');
            showKeyboard(keyboardElement);
        }
    }

    function mostrarKeyboardCambioClaveDigitalNuevaClaveConfirmacion(keyboardElement: JQuery) {
        if ($(window).width() <= 1024) {
            // todo responsive modo
            // $('#txtClaveDigitalReingreso').attr('readonly', 'false');
        } else {
            $('.container-teclado').removeClass('contenido-teclado-visible');
            $('.teclado-visible-input').removeClass('fondo__clave');
            $('.container-teclado2').removeClass('contenido-teclado-visible');
            $('.teclado-visible-input2').removeClass('fondo__clave');
            $('.container-teclado3').addClass('contenido-teclado-visible');
            $('.teclado-visible-input3').addClass('fondo__clave');
            showKeyboard(keyboardElement);
        }
    }

    function limpiarCLaveDigital(elemtnALimpiar: JQuery) {
        var $elemtnALimpiar = $(elemtnALimpiar);
        $elemtnALimpiar.val('');
        $elemtnALimpiar.trigger('input');
        angular.element(jQuery($elemtnALimpiar)).triggerHandler('input');
        $('.exito-pass').hide();
        $('.disabled-btn-continuar').show();
        $('.paso-siguiente').hide();
        $('.btn-aceptar-terminos').show();
        angular.element(jQuery($elemtnALimpiar)).triggerHandler('keyup');
        angular.element(document.querySelectorAll('#txtClaveDigital')).trigger('change');
        return false;
    }

    function agregarDigito(digito: JQuery, elementDestino: JQuery) {
        var $elementDestino = $(elementDestino);
        var elementId = elementDestino.attr('id');
        var $digito = $(digito);
        var character = $digito.html();

        // Special characters
        if ($digito.hasClass('symbol')) character = $('span:visible', $digito).html();
        if ($digito.hasClass('space')) character = ' ';
        if ($digito.hasClass('tab')) character = '\t';
        if ($digito.hasClass('return')) character = '\n';

        // Add the character
        if ($elementDestino.val().length < $elementDestino.attr('maxlength')) {
            $elementDestino.val($elementDestino.val() + character);
            $elementDestino.trigger('input');
            angular.element(jQuery($elementDestino)).triggerHandler('input');
            angular.element(jQuery($elementDestino)).triggerHandler('keyup');
            angular.element(document.querySelectorAll('#txtClaveDigital')).trigger('change');
            angular.element(document.querySelectorAll(elementId)).trigger('keyup');
            angular.element(document.querySelectorAll(elementId)).trigger('change');
            $elementDestino.trigger('keyup');
        }
    }

    function mostrarKeyboard(containerInput: JQuery, containerKeyBoard: JQuery, keyboard: JQuery) {
        if ($(window).width() <= 1024) {
            // todo responsive modo
            // $('#txtClaveDigitalReingreso').attr('readonly', 'false');
        } else {
            containerInput.addClass('fondo__clave');
            containerKeyBoard.addClass('contenido-teclado-visible');
            showKeyboard(keyboard);
        }
    }

    function ocultarKeyboard(containerInput: JQuery, containerKeyBoard: JQuery) {
        if ($(window).width() <= 1024) {
            // todo responsive modo
            // $('#txtClaveDigitalReingreso').attr('readonly', 'false');
        } else {
            containerInput.removeClass('fondo__clave');
            containerKeyBoard.removeClass('contenido-teclado-visible');
        }
    }

    function agregarCaracter(digito: JQuery, elementDestino: JQuery) {
        var elementId = elementDestino.attr('id');
        var $digito = $(digito);
        var character = $digito.html();

        // Special characters
        if ($digito.hasClass('symbol')) character = $('span:visible', $digito).html();
        if ($digito.hasClass('space')) character = ' ';
        if ($digito.hasClass('tab')) character = '\t';
        if ($digito.hasClass('return')) character = '\n';

        // Add the character
        if (elementDestino.val().length < elementDestino.attr('maxlength')) {
            elementDestino.val(elementDestino.val() + character);
            elementDestino.trigger('change');
        }
    }

    function limpiarinput(elemtnALimpiar: JQuery) {
        elemtnALimpiar.val('');
        elemtnALimpiar.trigger('change');
    }

    $(document)
        .on('focusin', '.cambio-clave-digital #claveAntigua', function () {
            mostrarKeyboard($('.cambio-clave-digital .teclado-visible-input'), $('.cambio-clave-digital .container-teclado'), $('.cambio-clave-digital #keyboard'));
        }).on('focusin', '.cambio-clave-digital #claveNueva', function () {
            mostrarKeyboard($('.cambio-clave-digital .teclado-visible-input2'), $('.cambio-clave-digital .container-teclado2'), $('.cambio-clave-digital #keyboard2'));
        }).on('focusin', '.cambio-clave-digital #claveNuevaConfirmacion', function () {
            mostrarKeyboard($('.cambio-clave-digital .teclado-visible-input3'), $('.cambio-clave-digital .container-teclado3'), $('.cambio-clave-digital #keyboard3'));
        }).on('click', function (event) {
            // icon1-clave-ojo2
            if (!($(event.target).closest('.container-teclado').length || $(event.target).closest('.cambio-clave-digital #claveAntigua').length || $(event.target).hasClass('vista2'))) {
                ocultarKeyboard($('.cambio-clave-digital .teclado-visible-input'), $('.cambio-clave-digital .container-teclado'));
            }
        }).on('click', function (event) {
            if (!($(event.target).closest('.container-teclado2').length || $(event.target).closest('.cambio-clave-digital #claveNueva').length || $(event.target).hasClass('vista4'))) {
                ocultarKeyboard($('.cambio-clave-digital .teclado-visible-input2'), $('.cambio-clave-digital .container-teclado2'));
            }
        }).on('click', function (event) {
            if (!($(event.target).closest('.container-teclado3').length || $(event.target).closest('.cambio-clave-digital #claveNuevaConfirmacion').length || $(event.target).hasClass('vista6'))) {
                ocultarKeyboard($('.cambio-clave-digital .teclado-visible-input3'), $('.cambio-clave-digital .container-teclado3'));
            }
        }).on('click', '.cambio-clave-digital #keyboard li:not(.delete)', function () {
            agregarCaracter($(this), $('.cambio-clave-digital #claveAntigua'));
        }).on('click', '.cambio-clave-digital #keyboard2 li:not(.delete)', function () {
            agregarCaracter($(this), $('.cambio-clave-digital #claveNueva'));
        }).on('click', '.cambio-clave-digital #keyboard3 li:not(.delete)', function () {
            agregarCaracter($(this), $('.cambio-clave-digital #claveNuevaConfirmacion'));
        }).on('click', '.cambio-clave-digital #keyboard li.delete', function () {
            limpiarinput($('.cambio-clave-digital #claveAntigua'));
        }).on('click', '.cambio-clave-digital #keyboard2 li.delete', function () {
            limpiarinput($('.cambio-clave-digital #claveNueva'));
        }).on('click', '.cambio-clave-digital #keyboard3 li.delete', function () {
            limpiarinput($('.cambio-clave-digital #claveNuevaConfirmacion'));
        });

    $(document).on('click', '.vista2', function () {
        $('.teclado1').attr('type', 'text');
        $('.vista2').addClass('on');
        $('.vista1').removeClass('on');

        setTimeout(function () {
            $('.teclado1').attr('type', 'password');
            $('.vista2').removeClass('on');
            $('.vista1').addClass('on');
        }, 3000);
    });

    $(document).on('click', '.vista4', function () {
        $('.teclado2').attr('type', 'text');
        $('.vista4').addClass('on');
        $('.vista3').removeClass('on');

        setTimeout(function () {
            $('.teclado2').attr('type', 'password');
            $('.vista4').removeClass('on');
            $('.vista3').addClass('on');
        }, 3000);
    });

    $(document).on('click', '.vista6', function () {
        $('.teclado3').attr('type', 'text');
        $('.vista6').addClass('on');
        $('.vista5').removeClass('on');

        setTimeout(function () {
            $('.teclado3').attr('type', 'password');
            $('.vista6').removeClass('on');
            $('.vista5').addClass('on');
        }, 3000);
    });


    $(document).on('click', '#generarClaveDigital #txtClaveDigital, #recuperarClaveDigital #txtClaveDigital', function () {
        mostrarKeyBoardClaveDigital($('#keyboard'));
    });

    $(document).on('focusin', '#generarClaveDigital #txtClaveDigital, #recuperarClaveDigital #txtClaveDigital', function () {
        mostrarKeyBoardClaveDigital($('#keyboard'));
    });

    $(document).on('click', '#generarClaveDigital #keyboard li.delete, #recuperarClaveDigital #keyboard li.delete', function () {
        limpiarCLaveDigital($('#txtClaveDigital'));
    });

    $(document).on('click', '#generarClaveDigital #keyboard li:not(.delete), #recuperarClaveDigital #keyboard li:not(.delete)', function () {
        agregarDigito($(this), $('#txtClaveDigital'));
    });

    $(document).on('click', '#generarClaveDigital #txtClaveDigitalReingreso, #recuperarClaveDigital #txtClaveDigitalReingreso', function () {
        mostrarKeyBoardClaveDigitalReingreso($('#keyboard2'));
    });

    $(document).on('focusin', '#generarClaveDigital #txtClaveDigitalReingreso, #recuperarClaveDigital #txtClaveDigitalReingreso', function () {
        mostrarKeyBoardClaveDigitalReingreso($('#keyboard2'));
    });

    $(document).on('click', '#generarClaveDigital #keyboard2 li.delete, #recuperarClaveDigital #keyboard2 li.delete', function () {
        limpiarCLaveDigital($('#txtClaveDigitalReingreso'));
    });

    $(document).on('click', '#generarClaveDigital #keyboard2 li:not(.delete), #recuperarClaveDigital #keyboard2 li:not(.delete)', function () {
        agregarDigito($(this), $('#txtClaveDigitalReingreso'));
    });

    $(document).on('click', '#login #txtLoginClaveDigital', function () {
        mostrarKeyBoardClaveDigitalReingreso($('#keyboard2'));
    });

    $(document).on('click', '#login #keyboard li.delete', function () {
        limpiarCLaveDigital($('#txtLoginClaveDigital'));
    });

    $(document).on('click', '#login #keyboard li:not(.delete)', function () {
        agregarDigito($(this), $('#txtLoginClaveDigital'));
    });

});

//funcion solo numeros y letras
(function (a: JQueryStatic) { a.fn.validCampoFranz = function (b: string) { return a(this).on({ keypress: function (a: JQueryKeyEventObject) { var c = a.which, d = a.keyCode, e = String.fromCharCode(c).toLowerCase(), f = b; (-1 != f.indexOf(e) || 9 == d || 37 != c && 37 == d || 39 == d && 39 != c || 8 == d || 46 == d && 46 != c) && 161 != c || a.preventDefault() } }) } })(jQuery);