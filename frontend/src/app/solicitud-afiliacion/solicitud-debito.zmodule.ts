angular.module('appDinersClubOnline.solicitudDebito', [])
.controller('SolicitudDebito', ['tarjetasApi', '$rootScope', '$routeParams', 'bancoFactory', 'tipoPagoFactory', 'sessionFactory', '$location', 'configuracion', '$scope', SolicitudDebito.SolicitudDebitoController])
.directive('solicitudDebitoautomatico', () => new SolicitudDebito.SolicitudDebitoDirective());