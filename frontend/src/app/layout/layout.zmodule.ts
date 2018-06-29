angular.module('appDinersClubOnline.layout', [])
    .controller('MenuLateral', ['ofertasApi', '$window', Layout.MenuLateralController])
    .directive('menuLateral', () => new Layout.MenuLateralDirective())
    .controller('Header', ['$location', 'sessionFactory', '$rootScope', Layout.HeaderController])
    .directive('header', () => new Layout.HeaderDirective())
    .directive('headerExterno', () => new Layout.HeaderExternoDirective());