angular.module('appDinersClubOnline.generarClaveDigital', [])
    .controller('GenerarClaveDigitalIdentificacion', ['sessionFactory', 'authApi', '$location', GenerarClaveDigital.IdentificacionController])
    .controller('GenerarClaveDigitalCrearClave', ['tarjetaApi', 'usuarioApi', 'authApi', 'facebookPictureFactory', 'operadoresTelefonicosApi', 'sessionFactory', '$location', '$q', GenerarClaveDigital.CrearClaveController]);