angular.module('appDinersClubOnline.api', [])
    .factory('authApi', ['$http', 'configuracion', '$httpParamSerializerJQLike',
        ($http: angular.IHttpService, configuracion: any, $httpParamSerializerJQLike: angular.IHttpParamSerializer) =>
            new Api.AuthApi($http, configuracion, $httpParamSerializerJQLike)
    ])
    .factory('tarjetasApi', ['$http', 'configuracion', 'sessionFactory',
        ($http: angular.IHttpService, configuracion: any, sessionFactory: Common.SessionFactory) =>
            new Api.TarjetasApi($http, configuracion, sessionFactory)
    ])
    .factory('tarjetaApi', ['$http', 'configuracion', 'sessionFactory',
        ($http: angular.IHttpService, configuracion: any, sessionFactory: Common.SessionFactory) =>
            new Api.TarjetaApi($http, configuracion, sessionFactory)
    ])
    .factory('usuarioApi', ['$http', 'configuracion', 'sessionFactory',
        ($http: angular.IHttpService, configuracion: any, sessionFactory: Common.SessionFactory) =>
            new Api.UsuarioApi($http, configuracion, sessionFactory)
    ])
    .factory('operadoresTelefonicosApi', ['$http', 'configuracion', 'sessionFactory',
        ($http: angular.IHttpService, configuracion: any, sessionFactory: Common.SessionFactory) =>
            new Api.OperadoresTelefonicosApi($http, configuracion, sessionFactory)
    ])
    .factory('confirmacionApi', ['$http', 'configuracion', 'sessionFactory',
        ($http: angular.IHttpService, configuracion: any, sessionFactory: Common.SessionFactory) =>
            new Api.ConfirmacionApi($http, configuracion, sessionFactory)
    ])
    .factory('geoApi', ['$http', '$q', '$window', '$rootScope', 'configuracion', 'sessionFactory',
        ($http: angular.IHttpService, $q: angular.IQService, $window: angular.IWindowService, $rootScope: angular.IRootScopeService, configuracion: any, sessionFactory: Common.SessionFactory) =>
            new Geolocalizacion.GeoAPi($http, $q, $window, $rootScope, configuracion, sessionFactory)
    ])
    .factory('categoriasApi', ['$http', 'configuracion', 'sessionFactory',
        ($http: angular.IHttpService, configuracion: any, sessionFactory: Common.SessionFactory) =>
            new Api.CategoriaApi($http, configuracion, sessionFactory)
    ])
    .factory('empresasApi', ['$http', 'configuracion', 'sessionFactory',
        ($http: angular.IHttpService, configuracion: any, sessionFactory: Common.SessionFactory) =>
            new Api.EmpresaApi($http, configuracion, sessionFactory)
    ])
    .factory('serviciosApi', ['$http', 'configuracion', 'sessionFactory',
        ($http: angular.IHttpService, configuracion: any, sessionFactory: Common.SessionFactory) =>
            new Api.ServicioApi($http, configuracion, sessionFactory)
    ])
    .factory('ofertasApi', ['$http', 'configuracion', 'sessionFactory',
        ($http: angular.IHttpService, configuracion: any, sessionFactory: Common.SessionFactory) =>
            new Api.OfertaApi($http, configuracion, sessionFactory)
    ])
    .factory('privilegioApi', ['$http', 'configuracion', 'sessionFactory',
        ($http: angular.IHttpService, configuracion: any, sessionFactory: Common.SessionFactory) =>
            new Api.PrivilegioApi($http, configuracion, sessionFactory)
    ])
    .factory('weatherApi', ['$http', 'configuracion',
        ($http: angular.IHttpService, configuracion: any) =>
            new Api.WeatherApi($http, configuracion)
    ])
    .factory('timeZoneApi', ['$http', 'configuracion',
        ($http: angular.IHttpService, configuracion: any) =>
            new Api.TimeZoneApi($http, configuracion)
    ])
    .factory('alertasNotificacionesApi', ['$http', 'configuracion', 'sessionFactory',
        ($http: angular.IHttpService, configuracion: any, sessionFactory: Common.SessionFactory) =>
            new Api.AlertaApi($http, configuracion, sessionFactory)
    ])
    .factory('campanasApi', ['$http', 'configuracion', 'sessionFactory',
        ($http: angular.IHttpService, configuracion: any, sessionFactory: Common.SessionFactory) =>
            new Api.CampanasApi($http, configuracion, sessionFactory)
    ]);