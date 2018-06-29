namespace ServiciosSocio {
    export class CambioClaveDigitalController {
        // menu
        tabSeleccionado = 'tab-1';


        claveAntigua = '';
        claveNueva = '';
        claveNuevaConfirmacion = '';
        claveIncorrecta = false;

        errorGenerico = false;
        fechaHoraOperacion = '';

        get clavesCoinciden() {
            return this.claveNueva === this.claveNuevaConfirmacion;
        }

        get valido() {
            return this.claveAntigua && this.claveNueva && this.claveNuevaConfirmacion && this.clavesCoinciden;
        }

        content: Confirmacion.IContent = {
            idModal: 'cambio-clave-01',
            titulo: 'Cambio de clave digital',
            subTitulo: '',
            subTituloMensaje: '',
            botonTexto: 'CAMBIAR CLAVE DIGITAL',
            codigo: 'c01',
            siguienteTabNombre: 'tab-3',
            volverTabNombre: 'tab-1',
            endPoint: '/usuario/clave-digital',
            httpVerb: 'PUT',
            scope: 'operaciones',
            forzarConfirmacion: false,
            valores: [
            ],
            mostrarOfertas: true
        };

        constructor(
            private usuarioApi: Api.UsuarioApi,
            private authApi: Api.AuthApi,
            private sessionFactory: Common.SessionFactory,
            private $q: angular.IQService,
            private $location: angular.ILocationService,
            private $scope: angular.IScope
        ) {
            this.$scope.$on('activarMenu', (event, data) => { this.activarMenu(data); });
        }

        cambiarClave() {
            this.usuarioApi.get().then((response) => {
                return this.authApi.tokenClaveDigital({
                    usuarioLogin: response.data.numeroDocumentoIdentidad,
                    claveDigital: this.claveAntigua
                }, 'operaciones');
            }, error => {
                this.errorGenerico = true;
                return this.$q.reject();
            }).then((response: angular.IHttpPromiseCallbackArg<Api.IClaveDigitalAccessToken>) => {
                let data: Api.IUsuarioPutClaveDigital = {
                    claveDigital: this.claveNueva
                };

                this.sessionFactory.claveDigitalOperacionesTokenType = response.data.token_type;
                this.sessionFactory.claveDigitalOperacionesAccessToken = response.data.access_token;

                this.sessionFactory.confirmacionData = data;

                this.activarMenu('tab-2');
            }, (response: angular.IHttpPromiseCallbackArg<Api.IAccessTokenError>) => {
                this.claveIncorrecta = true;
                return this.$q.reject(response.data.error_description);
            });
        }

        limpiarValidaciones() {
            this.claveIncorrecta = false;
            this.errorGenerico = false;
        }

        activarMenu(menuItem: string): void {
            this.tabSeleccionado = menuItem;
        }

        menuSeleccionado(menuItem: string): boolean {
            return this.tabSeleccionado === menuItem;
        }
    }
}