angular.module('appDinersClubOnline.tarjeta', [])
    .controller('tarjeta', ['tarjetasApi', 'authApi', '$location', '$rootScope', '$scope', Tarjeta.TarjetaController])
    .service('tarjetaService', [Tarjeta.TarjetaService])
    .directive('validarTarjeta', () => new Tarjeta.ValidarTarjetaDirective())
    .directive('tarjetasCarrusel', () => new Tarjeta.TarjetasCarruselDirective())
    .directive('ultimosMovimientos', () => new Tarjeta.UltimosMovimientosDirective());