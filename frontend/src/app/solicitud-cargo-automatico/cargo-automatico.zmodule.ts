angular.module('appDinersClubOnline.cargoAutomatico', [])
    .controller('CargoAutomatico', ['categoriasApi', 'empresasApi', 'serviciosApi', 'tarjetasApi', 'sessionFactory', '$location', '$filter', '$scope', CargoAutomatico.CargoAutomaticoController])
    .directive('solicitudCargoAutomatico', () => new CargoAutomatico.CargoAutomaticoDirective())
    .directive('solicitudCargoAutomaticoGestor', () => new CargoAutomatico.CargoAutomaticoGestorDirective());