angular.module('appDinersClubOnline.movimientos', [])
    .controller('Movimientos', ['tarjetasApi', 'movimientosFactory', '$filter', '$routeParams', '$rootScope', 'geoApi', 'tarjetaService', 'paginationService', 'configuracion', 'usuarioApi', Movimientos.MovimientosController])
    .factory('movimientosFactory', ['$http', 'configuracion', 'sessionFactory',
        ($http: angular.IHttpService, configuracion: any, sessionFactory: Common.SessionFactory) =>
            (new Movimientos.MovimientosFactory($http, configuracion, sessionFactory))
    ])
    .directive('movimientosHistoricos', () => new Movimientos.MovimientoHistoricoDirective())
    .service('paginationService', [Common.PaginationService]);