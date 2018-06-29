angular.module('appDinersClubOnline.confirmacion', [])
    .controller('Confirmacion', ['sessionFactory', 'confirmacionApi', '$location', '$sce', '$filter', '$scope', Confirmacion.Controller])
    .directive('solicitudConfirmacion', () => new Confirmacion.ConfirmacionDirective());