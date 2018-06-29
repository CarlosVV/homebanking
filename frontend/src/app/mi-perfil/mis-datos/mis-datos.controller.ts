namespace MiPerfil {
    export class MisDatosController {
        usuario: Api.IUsuarioGet;
        operadoresTelefonicos: Api.IOperadorTelefonico[];
        tabSeleccionado = 'tab-1';
        content: Confirmacion.IContent;
        operadorTelefonicoActual: Api.IOperadorTelefonico;
        operadorTelefonicoSeleccionado: Api.IOperadorTelefonico;
        usuarioActual: Api.IUsuarioGet;
        emailPersonalActual: string;
        emailAlternativoActual: string;
        idTipoOperadorActual: string;
        numeroTelefonicoActual: string;
        emailPersonalCorrecto: boolean = true;
        emailAlternativoCorrecto: boolean = true;
        tipoOperadorCorrecto: boolean = true;
        numeroTelefonicoCorrecto: boolean = true;
        muestraErrores: boolean = false;

        constructor(private usuarioApi: Api.UsuarioApi, private operadoresTelefonicosApi: Api.OperadoresTelefonicosApi, private sessionStorage: Common.SessionFactory, private $location: angular.ILocationService, private $filter: angular.IFilterService, private $scope: angular.IScope) {
            this.obtenerUsuario();
            this.obtenerOperadores();
            this.$scope.$on('activarMenu', ((event, data) => { this.activarMenu(data); }));
        }

        init() {
            this.obtenerUsuario();
            this.obtenerOperadores();
        }

        obtenerUsuario() {
            this.usuarioApi.get().then((result) => {
                this.emailPersonalActual = result.data.emailPrincipal;
                this.emailAlternativoActual = result.data.emailAlternativo;
                this.idTipoOperadorActual = result.data.idOperadorTelefonico;
                this.numeroTelefonicoActual = result.data.numeroCelular;
                this.usuario = result.data;
            });
        }

        obtenerOperadores() {
            this.operadoresTelefonicosApi.get().then((result) => {
                 this.operadoresTelefonicos = result.data;
                 this.operadorTelefonicoActual = result.data[0];
             });
        }

        actualizarUsuario() {
            this.muestraErrores = false;
            if (!this.validateForm) {
                // mostrar mensajes de error
                console.log('faltan ingresar los datos en *');
                this.muestraErrores = true;
                return;
            }
            console.log('actualizando datos de contacto');

            var fechaActual = new Date();
            var fechaFormatoActual = this.$filter('date')(fechaActual, 'dd/MM/yyyy HH:mm') + '';

            var contenido: Confirmacion.IContent = {
                    idModal: 'act01',
                    titulo: 'Actualización de Datos de Contacto',
                    subTitulo: 'Los datos han sido actualizados con éxito.',
                    subTituloMensaje: '',
                    botonTexto: 'CONFIRMAR',
                    codigo: 'MpMd',
                    siguienteTabNombre: 'tab-3',
                    volverTabNombre: 'tab-1',
                    endPoint: '/usuario/',
                    httpVerb: 'PATCH',
                    scope: 'consultas',
                    forzarConfirmacion: true,
                    valores: [
                        { key: 'Fecha y hora', value: fechaFormatoActual },
                        { key: 'E-mail personal', value: this.emailPersonalActual },
                        { key: 'E-mail secundario', value: this.emailAlternativoActual},
                        { key: 'Operador', value: this.operadorTelefonicoActual.nombre},
                        { key: 'Nº de documento', value: this.numeroTelefonicoActual}
                    ],
                  mostrarOfertas: false
             };

             var data = {
                 emailPrincipal: this.emailPersonalActual,
                 emailAlternativo: this.emailAlternativoActual,
                 idOperadorTelefonico: this.operadorTelefonicoActual.id,
                 numeroCelular :  this.numeroTelefonicoActual
              };

              console.log(contenido);
              this.content = contenido;
              this.sessionStorage.confirmacionData = data;
              this.activarMenu('tab-2');
        }

        get validateForm() {
            if (this.emailPersonalActual) {
                this.emailPersonalCorrecto = true;
            } else {
                this.emailPersonalCorrecto = false;
            }

            if (this.emailAlternativoActual) {
                this.emailAlternativoCorrecto = true;
            } else {
                this.emailAlternativoCorrecto = false;
            }

            if (this.idTipoOperadorActual) {
                this.tipoOperadorCorrecto = true;
            } else {
                this.tipoOperadorCorrecto = false;
            }

            if (this.numeroTelefonicoActual) {
                this.numeroTelefonicoCorrecto = true;
            } else {
                this.numeroTelefonicoCorrecto = false;
            }

            return (this.emailPersonalActual && this.emailAlternativoCorrecto && this.tipoOperadorCorrecto && this.numeroTelefonicoCorrecto);
        }

        activarMenu(menuItem: string): void {
            this.tabSeleccionado = menuItem;
        }

        menuSeleccionado(menuItem: string): boolean {
            return this.tabSeleccionado === menuItem;
        }
    }
}
