namespace Logout {
    export class LogoutController {
        constructor(private logoutFactory: LogoutFactory, private $location: angular.ILocationService, private sessionFactory: Common.SessionFactory, private $rootScope: angular.IRootScopeService) {
            this.$rootScope.$on('cerrarSesionTiempoExpirado', (() => {
                this.cerrarSesionTiempoExpirado();
            }));
        }

        isAuthenticated(): boolean {
            return this.sessionFactory.isAuthenticated();
        }

        cerrarSesion(): void {
            this.sessionFactory.clearSessions();
            this.$rootScope.$broadcast('cancelSessionTimer', { message: 'End timer' });
            this.$location.path('/login');
        }

        cerrarSesionTiempoExpirado() {
            this.sessionFactory.clearSessions();
            this.$location.path('/login');
        }
    }
}