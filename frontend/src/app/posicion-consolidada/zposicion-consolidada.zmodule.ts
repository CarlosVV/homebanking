angular.module('appDinersClubOnline.posicionConsolidada', ['appDinersClubOnline.api'])
    .controller('PosicionConsolidada', ['campanasApi', 'usuarioApi', 'tarjetasApi', 'sessionFactory', 'geoApi', '$location', '$rootScope', '$filter', 'tarjetaService', 'weatherApi', 'timeZoneApi', 'facebookPictureFactory', '$sce', PosicionConsolidada.PosicionConsolidadaController])
    .controller('Privilegios', ['geoApi', 'privilegioApi', PosicionConsolidada.PrivilegiosController])
    .directive('privilegiosSmall', () => new PosicionConsolidada.PrivilegiosSmallDirective())
    .directive('privilegiosFull', () => new PosicionConsolidada.PrivilegiosFullDirective())
    .directive('campanasSlider', () => new PosicionConsolidada.CampanasSliderDirective());
