angular.module('appDinersClubOnline.tarjetaAdicional', [])
    .controller('tarjetaAdicional', ['$rootScope', 'tarjetasApi', 'sessionFactory', '$scope', '$filter', 'tipoDocumentoFactory', TarjetaAdicional.TarjetaAdicionalController])
    .directive('tarjetaAdicional', () => new TarjetaAdicional.TarjetaAdicionalDirective());