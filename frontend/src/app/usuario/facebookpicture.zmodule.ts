angular.module('appDinersClubOnline.facebookpicture', [])
    .factory('facebookPictureFactory', ['$http', '$window', '$q', 'configuracion',
        ($http: angular.IHttpService, $window: angular.IWindowService, $q: angular.IQService, configuracion: any) =>
            new Usuario.FacebookPictureFactory($http, $window, $q, configuracion)
    ]);