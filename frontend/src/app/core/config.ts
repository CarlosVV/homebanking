(() => {
    angular.module('appDinersClubOnline')
        .factory('timeinInterceptor', ['$rootScope', timeinInterceptor])
        .factory('unauthorizedInterceptor', ['$q', '$location', unauthorizedInterceptor])
        .config(['$routeProvider', '$locationProvider', configRoute])
        .config(['$httpProvider', configInterceptors])
        .run(run)
        .directive('script', function () {
            return {
                restrict: 'E',
                scope: false,
                link: function (scope: any, elem: JQuery, attr: any) {
                    if (attr.type === 'text/javascript-lazy') {
                        var s = document.createElement('script');
                        s.type = 'text/javascript';
                        var src = elem.attr('src');
                        if (src !== undefined) {
                            s.src = src;
                        } else {
                            var code = elem.text();
                            s.text = code;
                        }
                        document.head.appendChild(s);
                        elem.remove();
                    }
                }
            };
        });

    function configRoute($routeProvider: angular.route.IRouteProvider) {
        $routeProvider
            .when('/login', {
                templateUrl: 'app/login/login.html',
            })
            .when('/generar-clave-digital/identificacion', {
                templateUrl: 'app/generar-clave-digital/identificacion.html'
            })
            .when('/generar-clave-digital/crear-clave', {
                templateUrl: 'app/generar-clave-digital/crear-clave.html'
            })
            .when('/recuperar-clave-digital/identificacion', {
                templateUrl: 'app/recuperar-clave-digital/identificacion.html'
            })
            .when('/recuperar-clave-digital/nueva-clave', {
                templateUrl: 'app/recuperar-clave-digital/nueva-clave.html'
            })
            .when('/usuario', {
                templateUrl: 'app/usuario/usuario.html',
                controller: 'Usuario'
            })
            .when('/usuario/datos', {
                templateUrl: 'app/usuario/datos-adicionales.html',
                controller: 'Usuario'
            })
            .when('/consulta-saldos', {
                templateUrl: 'app/consulta-saldos/estado-de-cuenta.html'
            })
            .when('/movimientos/:tarjetaId', {
                templateUrl: 'app/movimientos/movimientos.html'
            })
            .when('/movimientos-filtros-predefinidos', {
                templateUrl: 'app/tarjeta/movimientos-filtros-predefinidos.html'
            })
            .when('/posicion-consolidada', {
                templateUrl: 'app/posicion-consolidada/posicion-consolidada.html'
            })
            .when('/confirmacion', {
                templateUrl: 'app/confirmacion/confirmacion.html'
            })
            .when('/constancia', {
                templateUrl: 'app/constancia/constancia.html'
            })
            .when('/mi-perfil', {
                templateUrl: 'app/mi-perfil/mi-perfil.html'
            })
            .when('/solicitud-afiliacion-debito/:tarjetaId', {
                templateUrl: 'app/solicitud-afiliacion/solicitud-debito-automatico.html'
            })
            .when('/solicitud-afiliacion-debito', {
                templateUrl: 'app/solicitud-afiliacion/solicitud-debito-automatico.html'
            })
            .when('/soluciones-financieras', {
                templateUrl: 'app/soluciones-financieras/soluciones-financieras.html'
            })
            .when('/soluciones-financieras/:idTipoOferta', {
                templateUrl: 'app/soluciones-financieras/soluciones-financieras.html'
            })
            .when('/servicios-socio', {
                templateUrl: 'app/servicios-socio/servicios-socio.html'
            })
            .when('/solicitud-prestamo-personal/:idTipoOferta', {
                templateUrl: 'app/solicitud-prestamo-personal/solicitud-prestamo-personal.html'
            })
            .when('/solicitud-dinero-efectivo/:idTipoOferta', {
                templateUrl: 'app/solicitud-dinero-efectivo/solicitud-dinero-efectivo.html'
            })
            .when('/solicitud/cargo-automatico', {
                templateUrl: 'app/solicitud-cargo-automatico/cargo-automatico.html'
            })
            .when('/solicitud/cargo-automatico/confirmacion', {
                templateUrl: 'app/confirmacion/confirmacion.html'
            })
            .when('/ofertas/detalle/:idTipoOferta', {
                templateUrl: 'app/oferta/detalle-oferta.html'
            })
            .when('/ofertas', {
                templateUrl: 'app/oferta/oferta.html'
            })
            .when('/solicitud/ampliacion-linea', {
                templateUrl: 'app/soluciones-financieras/ampliacion-linea/ampliacion-linea.html'
            })
            .when('/solicitud/ampliacion-linea/:idTipoOferta', {
                templateUrl: 'app/soluciones-financieras/ampliacion-linea/ampliacion-linea.html'
            })
            .when('/formas-pago', {
                templateUrl: 'app/formas-pago/formas-pago.html'
            })
            .when('/estado-cuenta/:tarjetaId/:tabMenuItem', {
                templateUrl: 'app/movimientos/movimientos.html'
            })
            .when('/404', {
                controller: ['$location', function ($location: angular.ILocationService) {
                    $location.replace();
                }]
            })
            .otherwise({
                redirectTo: '/login'
            });
    }

    /** Por cada request al servidor resetea el timer */
    function timeinInterceptor($rootScope: angular.IRootScopeService) {
        return {
            request: function (config: angular.IRequestConfig) {
                // console.log(`request: ${config.method} ${config.url}`);

                $rootScope.$broadcast('restaurarTimer', { message: 'start timer' });

                return config;
            }
        };
    }

    function unauthorizedInterceptor($q: angular.IQService, $location: angular.ILocationService) {
        return {
            responseError: function (rejection: any) {
                if (rejection.status === 401) {
                    $location.path('/login').search('returnUrl', $location.path());
                }

                return $q.reject(rejection);
            }
        };
    }

    function configInterceptors($httpProvider: angular.IHttpProvider) {
        $httpProvider.interceptors.push('timeinInterceptor', 'unauthorizedInterceptor');
    }

    function run($rootScope: any, $location: angular.ILocationService, sessionFactory: Common.SessionFactory) {

        $rootScope.$on('$routeChangeStart', function (event: any, next: any, current: any) {
            // redirect to login page if not logged in and trying to access a restricted page
            if (!sessionFactory.isAuthenticated() && !paginaSinValidacion(next, sessionFactory)) {
                sessionFactory.clearSessions();
                $location.path('/login');
            }
            if (sessionFactory.isAuthenticated() && noPermitirIrPaginaLoginParaUsuarioAutentidado(next)) {
                $location.path('/posicion-consolidada');
            }
        });
    }

    function paginaSinValidacion(next: any, sessionFactory: Common.SessionFactory) {
        var route = next.$$route;

        if (!route) {
            return false;
        }

        switch (route.originalPath) {
            case '/generar-clave-digital/identificacion':
                return true;
            case '/generar-clave-digital/crear-clave':
                return sessionFactory.isAuthenticatedByTarjeta();
            case '/recuperar-clave-digital/identificacion':
                return true;
            case '/recuperar-clave-digital/nueva-clave':
                return sessionFactory.isAuthenticatedByTarjeta();
            default:
                return false;
        }
    }

    function noPermitirIrPaginaLoginParaUsuarioAutentidado(next: any) {
        var route = next.$$route;

        if (!route) {
            return true;
        }

        switch (route.originalPath) {
            case '/login':
                return true;
            case '/generar-clave-digital/crear-clave':
                return true;
            case '/recuperar-clave-digital/nueva-clave':
                return true;
            default:
                return false;
        }
    };

    run.$inject = ['$rootScope', '$location', 'sessionFactory'];
})();