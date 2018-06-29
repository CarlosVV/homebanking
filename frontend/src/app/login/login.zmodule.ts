angular.module('appDinersClubOnline.login', ['appDinersClubOnline.api'])
    .controller('Login', ['authApi', 'sessionFactory', 'geoApi', '$location', '$rootScope', 'usuarioApi', 'facebookPictureFactory', Login.LoginController])
    .directive('tecladoNumerico', () => new Login.TecladoNumericoDirective());