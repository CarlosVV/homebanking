angular.module('appDinersClubOnline.miPerfil', [])
.controller('MisDatos',
        ['usuarioApi', 'operadoresTelefonicosApi', 'sessionFactory', '$location', '$filter',
        '$scope', '$location', 'configuracion',
        '$scope', MiPerfil.MisDatosController])
     .directive('misDatos', () => new MiPerfil.MisDatosDirective())
     .directive('misDatosGestor', () => new MiPerfil.MisDatosGestorDirective())
.controller('CambiarImagen',
        ['usuarioApi', 'operadoresTelefonicosApi', 'sessionFactory', '$location', '$filter',
        '$scope', '$rootScope', MiPerfil.CambiarImagenController])
     .directive('cambiarImagen', () => new MiPerfil.CambiarImagenDirective())
.controller('MiPerfil', ['sessionFactory', MiPerfil.MiPerfilController]);

