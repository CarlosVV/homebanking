namespace Timer {
    export class TimerController {
        counter: number;
        sessionTimer : any = null;
        constructor(private sessionFactory: Common.SessionFactory, private $timeout: angular.ITimeoutService, private $rootScope: angular.IRootScopeService, private configuracion: any) {
            this.counter = this.configuracion.expirationTimeSession;
            this.sessionTimer = this.$timeout;

            this.restaurarTimerEventoload();

            this.$rootScope.$on('startSessionTimer', (() => {
                this.startSessionTimer();
            }));

            this.$rootScope.$on('cancelSessionTimer', (() => {
                this.cancelSessionTimer();
            }));

            this.$rootScope.$on('restaurarTimer', (() => {
                this.restaurarTimerAfterAnyAction();
            }));
        }

        get tiempoRestante(): string {
            let segundosRestantes = this.counter;

            const horas = Math.floor(segundosRestantes / 3600);
            segundosRestantes = segundosRestantes - (horas * 3600);

            const minutos = Math.floor(segundosRestantes / 60);
            const segundos = segundosRestantes - (minutos * 60);
            return `${horas ? `${horas}:` : ''}${this.leftPadding(minutos)}:${this.leftPadding(segundos)}`;
        }

        isAuthenticated() {
            return this.sessionFactory.isAuthenticated();
        }

        mostrarContador() {
            return !(this.sessionFactory.claveDigitalAccessToken === null);
        };

        cancelSessionTimer() {
            this.$timeout.cancel(this.sessionTimer);
        }

        restaurarTimerEventoload() {
            if (this.isAuthenticated()) {
                this.counter = this.configuracion.expirationTimeSession;
                this.startSessionTimer();
            }
        }

        restaurarTimerDespuesLogin() {
            if (this.isAuthenticated()) {
                this.counter = this.configuracion.expirationTimeSession;
            }
        }

        restaurarTimerAfterAnyAction() {
            if (this.isAuthenticated()) {
                this.counter = this.configuracion.expirationTimeSession;
            }
        }

        startSessionTimer() {
            this.restaurarTimerDespuesLogin();
            this.countDown();
        }

        countDown() {
            if (this.isAuthenticated()) {
                this.sessionTimer = this.$timeout(() => {
                    this.counter--;
                    if (this.counter === 0) {
                        this.cancelSessionTimer();
                        this.$rootScope.$broadcast('cerrarSesionTiempoExpirado', { message: 'Close session after timer ends' });
                        return;
                    }
                    this.countDown();
                }, 1000);
            }
        }

        private leftPadding(n: number): string {
            return `00${n}`.slice(-2);
        }
    }
}
