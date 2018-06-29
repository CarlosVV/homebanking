angular.module('appDinersClubOnline.constancia', [])
    .controller('Constancia', ['sessionFactory', '$rootScope', '$sce', '$scope', 'constanciaFactory', Constancia.ConstanciaController])
    .directive('constanciaSolicitud', () => new Constancia.ConstanciaDirective())
    .factory('constanciaFactory', ['$http', 'configuracion', 'sessionFactory',
        ($http: angular.IHttpService, configuracion: any, sessionFactory: Common.SessionFactory) =>
            new Constancia.ConstanciaFactory($http, configuracion, sessionFactory)
    ]);