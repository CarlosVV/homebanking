angular.module('appDinersClubOnline.solicitudDineroEfectivo', [])
    .controller('solicitudDineroEfectivo', ['tarjetasApi', '$location', 'bancoFactory', 'sessionFactory', '$filter', '$routeParams', '$scope', Solicitud.DineroEfectivoController]);