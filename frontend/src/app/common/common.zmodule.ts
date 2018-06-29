angular.module('appDinersClubOnline.common', [])
    .factory('sessionFactory', ['$localStorage', '$sessionStorage',
        ($localStorage: Common.IDinersLocalStorage, $sessionStorage: Common.IDinersSessionStorage) =>
            new Common.SessionFactory($localStorage, $sessionStorage)
    ])
    .factory('bancoFactory', ['$http', 'configuracion', 'sessionFactory', ($http: ng.IHttpService, configuracion: any, sessionFactory: Common.SessionFactory) => new Common.BancoFactory($http, configuracion, sessionFactory)])
    .factory('tipoPagoFactory', ['$http', 'configuracion', 'sessionFactory', ($http: ng.IHttpService, configuracion: any, sessionFactory: Common.SessionFactory) => new Common.TipoPagoFactory($http, configuracion, sessionFactory)])
    .factory('tipoDocumentoFactory', ['$http', 'configuracion', 'sessionFactory', ($http: ng.IHttpService, configuracion: any, sessionFactory: Common.SessionFactory) => new Common.TipoDocumentoFactory($http, configuracion, sessionFactory)])
    .factory('bancoAfiliadoFactory', ['$http', 'configuracion', 'sessionFactory', ($http: ng.IHttpService, configuracion: any, sessionFactory: Common.SessionFactory) => new Common.BancoAfiliadoFactory($http, configuracion, sessionFactory)])
    .directive('datePicker', () => new Common.DatePicker());