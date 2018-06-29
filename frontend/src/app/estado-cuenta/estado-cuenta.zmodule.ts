angular.module('appDinersClubOnline.estadoCuenta', [])
    .controller('EstadoCuenta', ['estadoCuentaFactory', 'sessionFactory', '$location', 'tarjetaService', '$rootScope', '$rootScope', 'configuracion', EstadoCuenta.EstadoCuentaController])
    .factory('estadoCuentaFactory', ['$http', 'configuracion', '$httpParamSerializerJQLike', 'sessionFactory',
        ($http: angular.IHttpService, configuracion: any, $httpParamSerializerJQLike: angular.IHttpParamSerializer, sessionFactory: Common.SessionFactory) =>
            new EstadoCuenta.EstadoCuentaFactory($http, configuracion, $httpParamSerializerJQLike, sessionFactory)
    ])
    .directive('resumenEstadocuenta', () => new EstadoCuenta.EstadodeCuentaDirective());