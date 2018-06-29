angular.module('appDinersClubOnline.ampliacionLinea', [])
.controller('AmpliacionLinea', ['tarjetasApi', '$rootScope', '$routeParams', 'sessionFactory', '$location', 'configuracion', '$scope', AmpliacionCredito.AmpliacionLineaController])
.directive('ampliacionLinea', () => new AmpliacionCredito.AmpliacionLineaDirective());