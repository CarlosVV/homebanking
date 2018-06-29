angular.module('appDinersClubOnline.logout', [])
    .controller('logout', ['logoutFactory', '$location', 'sessionFactory', '$rootScope', Logout.LogoutController])
    .factory('logoutFactory', ['$http', 'configuracion', 'sessionFactory',
        ($http: angular.IHttpService, configuracion: any, sessionFactory: Common.SessionFactory) =>
            new Logout.LogoutFactory($http, configuracion, sessionFactory)
    ]);