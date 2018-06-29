namespace Alerta {
    interface IOpciones {
        id: string;
        value: string;
        descripcion: string;
    }

    export class AlertaNotificacionController {
        tabSeleccionado = 'tab-1';
        content: Confirmacion.IContent;

        emails: IOpciones[] = [];
        telefonos: IOpciones[] = [];
        tarjetas: Api.ITarjeta[];
        tarjetaActual: Api.ITarjeta;

        alertasGestor: IAlerta[];
        alertasTarjetaUsuarioConfiguradas: IAlertaTarjetaUsuario;
        alertasTarjetaUsuarioMatch: IAlertaTarjetaUsuario;
        tmpEmailAdicional: string;
        tmpTelefonoAdicional: string;

        // todo: mails, telefonos y adicionales

        constructor(
            private alertasNotificacionesApi: Api.AlertaApi,
            private usuarioApi: Api.UsuarioApi,
            private tarjetasApi: Api.TarjetasApi,
            private sessionStorage: Common.SessionFactory,
            private $scope: angular.IScope) {
            this.obtenerAlertasTodas();
            this.obtenerTarjetas();
            this.$scope.$on('activarMenu', (event, data) => { this.activarMenu(data); });
            this.cargarDatosUsuario();
        }

        obtenerAlertasTarjeta() {
            this.alertasNotificacionesApi.obtenerAlertasTarjeta(this.tarjetaActual.idTarjeta).then((resul) => {
                if (resul) {
                    this.alertasTarjetaUsuarioConfiguradas = resul.data;
                    this.matchAlertas();
                } else {
                    console.log('Dinners online se detuvo');
                }

            });
        }

        guardarAlertas() {
            var alertasEmailPrincipal = this.alertasTarjetaUsuarioMatch.alertasActivas.filter(item => item.email1Activo);
            var alertasEmailAdicional = this.alertasTarjetaUsuarioMatch.alertasActivas.filter(item => item.email2Activo);
            var alertasTelefonoPrincipal = this.alertasTarjetaUsuarioMatch.alertasActivas.filter(item => item.celular1Activo);
            var alertasTelefonoAdicional = this.alertasTarjetaUsuarioMatch.alertasActivas.filter(item => item.celular2Activo);

            var alertasEmailPrincipalHTML: string, alertasEmailAdicionalHTML: string, alertasTelefonoPrincipalHTML: string, alertasTelefonoAdicionalHTML: string;
            alertasEmailPrincipalHTML = this.crearHtmlLista(alertasEmailPrincipal);
            alertasEmailAdicionalHTML = this.crearHtmlLista(alertasEmailAdicional);
            alertasTelefonoPrincipalHTML = this.crearHtmlLista(alertasTelefonoPrincipal);
            alertasTelefonoAdicionalHTML = this.crearHtmlLista(alertasTelefonoAdicional);

            var contenido: Confirmacion.IContent = {
                idModal: 'alerta01',
                titulo: 'Alertas y notificaciones',
                subTitulo: '',
                subTituloMensaje: '',
                subTituloInformacion: 'Alertas configuradas para',
                botonTexto: 'GUARDAR CONFIGURACIÓN',
                codigo: 'c04',
                siguienteTabNombre: 'tab-3',
                volverTabNombre: 'tab-1',
                endPoint: `/tarjetas/${this.alertasTarjetaUsuarioMatch.idTarjeta}/alertas`,
                forzarConfirmacion: true,
                httpVerb: 'PUT',
                valores: [
                    { key: 'Alertas configuradas para', value: '' },
                    { key: 'Email Principal', value: alertasEmailPrincipalHTML },
                    { key: 'Email Adicional', value: alertasEmailAdicionalHTML },
                    { key: '', value: '' },
                    { key: '<hr>', value: '<hr>' },
                    { key: 'Teléfono Principal', value: alertasTelefonoPrincipalHTML },
                    { key: 'Teléfono Adicional', value: alertasTelefonoAdicionalHTML },
                    { key: '', value: '' },
                    { key: '<hr>', value: '<hr>' }
                ],
                mostrarOfertas: true
            };

            var data = {
                idTarjeta: this.alertasTarjetaUsuarioMatch.idTarjeta,
                emailSeleccionado: this.alertasTarjetaUsuarioMatch.emailSeleccionado,
                emailAdicional: this.alertasTarjetaUsuarioMatch.emailAdicional,
                telefonoAdicional: this.alertasTarjetaUsuarioMatch.telefonoAdicional,
                activo: true,
                alertasActivas: this.alertasTarjetaUsuarioMatch.alertasActivas.map(item => {
                    var alertasTmp = {
                        idAlerta: item.alertaSeleccionada.idAlerta,
                        idCondicionAdicional: item.condicioneAdicionalSeleccionada != null ? item.condicioneAdicionalSeleccionada.idCondicionAdicional : null,
                        celular1Activo: item.celular1Activo,
                        celular2Activo: item.celular2Activo,
                        Email1Activo: item.email1Activo,
                        Email2Activo: item.email2Activo,
                        datosCorreo: contenido.valores
                    };
                    return alertasTmp;
                })
            };

            // datosCorreo: contenido.valores    

            this.content = contenido;
            this.sessionStorage.confirmacionData = data;
            this.activarMenu('tab-2');
        }

        menuSeleccionado(menuItem: string): boolean {
            return this.tabSeleccionado === menuItem;
        }

        modificarTelefonoAdicional() {
            this.alertasTarjetaUsuarioMatch.telefonoAdicional = this.tmpTelefonoAdicional;
        }

        editarTelefonoAdicional() {
            this.tmpTelefonoAdicional = this.alertasTarjetaUsuarioMatch.telefonoAdicional;
        }

        modificarEmailAdicional() {
            this.alertasTarjetaUsuarioMatch.emailAdicional = this.tmpEmailAdicional;
        }

        anularTelefonoAdicional() {
            this.alertasTarjetaUsuarioMatch.telefonoAdicional = null;
            this.tmpTelefonoAdicional = null;
        }

        editarEmailAdicional() {
            this.tmpEmailAdicional = this.alertasTarjetaUsuarioMatch.emailAdicional;
        }

        anularEmailAdicional() {
            this.alertasTarjetaUsuarioMatch.emailAdicional = null;
            this.tmpEmailAdicional = null;
        }

        get tieneEmailAdicional(): boolean {
            if (!this.alertasTarjetaUsuarioMatch) {
                return false;
            }

            return this.noEsNuloVacio(this.alertasTarjetaUsuarioMatch.emailAdicional);
        }

        get tieneTelefonoAdicional(): boolean {
            if (!this.alertasTarjetaUsuarioMatch) {
                return false;
            }

            return this.noEsNuloVacio(this.alertasTarjetaUsuarioMatch.telefonoAdicional);
        }

        private noEsNuloVacio(parametro: string) {
            var valido: boolean;
            if (parametro) {
                valido = true;
            } else {
                valido = false;
            }

            return valido;
        }

        private obtenerTarjetas() {
            this.tarjetasApi.get().then((result) => {
                this.tarjetas = result.data.map((x: Api.ITarjeta) => {
                    let tarjeta = new Tarjeta.DetalleTarjetaViewModel();
                    tarjeta.idTarjeta = x.idTarjeta;
                    tarjeta.nombreProducto = x.nombreProducto;
                    tarjeta.alias = x.alias;
                    if (!x.alias) {
                        tarjeta.alias = x.nombreProducto;
                    }

                    tarjeta.numeroTarjeta = `${tarjeta.alias} ${Common.Utility.mostrarNumeroTarjetaConAsterisco(x.numeroTarjeta)}`;
                    tarjeta.lineaCreditoSoles = x.lineaCreditoSoles;
                    tarjeta.lineaCreditoDolares = x.lineaCreditoDolares;
                    tarjeta.lineaDisponibleSoles = x.lineaDisponibleSoles;
                    tarjeta.lineaDisponibleDolares = x.lineaDisponibleDolares;
                    tarjeta.millasDisponibles = x.millasDisponibles;
                    tarjeta.montoTotalMesSoles = x.montoTotalMesSoles;
                    tarjeta.montoTotalMesDolares = x.montoTotalMesDolares;
                    tarjeta.montoMinimoMesSoles = x.montoMinimoMesSoles;
                    tarjeta.montoTotalMesDolares = x.montoTotalMesDolares;
                    tarjeta.montoMinimoMesDolares = x.montoMinimoMesDolares;
                    tarjeta.fechaVencimiento = x.fechaVencimiento;
                    tarjeta.tieneAdicionales = x.tieneAdicionales;
                    tarjeta.srcImagenTarjeta = `Content/images/${x.nombreProducto}.png`;
                    return tarjeta;
                });

                this.tarjetaActual = this.tarjetas[0];
                this.obtenerAlertasTarjeta();
            });
        }

        private activarMenu(menuItem: string): void {
            this.tabSeleccionado = menuItem;
        }

        private cargarDatosUsuario() {
            this.usuarioApi.get().then(item => {
                this.emails.push({ id: '1', value: item.data.emailPrincipal, descripcion: 'Principal' });
                this.emails.push({ id: '2', value: item.data.emailAlternativo, descripcion: 'Secundario' });
                this.telefonos.push({ id: '1', value: item.data.numeroCelular, descripcion: 'Principal' });
            });
        }

        private obtenerAlertasTodas() {
            this.alertasNotificacionesApi.obtenerAlertasTodas().then(item => {
                if (item.data) {
                    this.alertasGestor = item.data;
                }
            });
        }

        private matchAlertas() {
            // todo: verificar que se haya cargado todas las alertas

            this.alertasTarjetaUsuarioMatch = {
                idTarjeta: this.alertasTarjetaUsuarioConfiguradas.idTarjeta,
                emailSeleccionado: this.alertasTarjetaUsuarioConfiguradas.emailSeleccionado,
                emailAdicional: this.alertasTarjetaUsuarioConfiguradas.emailAdicional,
                telefonoAdicional: this.alertasTarjetaUsuarioConfiguradas.telefonoAdicional,
                activo: this.alertasTarjetaUsuarioConfiguradas.activo,
                alertasActivas: new Array()
            };

            var alertaTarjetaTmp: IAlertaTarjeta;

            this.alertasGestor.forEach((alerta, index) => {
                var alertaExiste: IAlertaTarjeta[];
                if (this.alertasTarjetaUsuarioConfiguradas.alertasActivas && this.alertasTarjetaUsuarioConfiguradas.alertasActivas.length) {
                    alertaExiste = this.alertasTarjetaUsuarioConfiguradas.alertasActivas.filter(item => item.idAlerta === alerta.idAlerta);
                }

                if (alertaExiste && alertaExiste.length) {
                    alertaTarjetaTmp = {
                        alertaSeleccionada: alerta,
                        condicioneAdicionalSeleccionada: alerta.condicionesAdicionales.filter(condicion => condicion.idCondicionAdicional === alertaExiste[0].idCondicionAdicional)[0],
                        idAlerta: alertaExiste[0].idAlerta,
                        idCondicionAdicional: alertaExiste[0].idCondicionAdicional,
                        celular1Activo: alertaExiste[0].celular1Activo,
                        celular2Activo: alertaExiste[0].celular2Activo,
                        email1Activo: alertaExiste[0].email1Activo,
                        email2Activo: alertaExiste[0].email2Activo
                    };

                } else {
                    alertaTarjetaTmp = {
                        alertaSeleccionada: alerta,
                        condicioneAdicionalSeleccionada: alerta.condicionesAdicionales != null && alerta.condicionesAdicionales.length ? alerta.condicionesAdicionales[0] : null,
                        idAlerta: alerta.idAlerta,
                        idCondicionAdicional: alerta.condicionesAdicionales != null && alerta.condicionesAdicionales.length ? alerta.condicionesAdicionales[0].idCondicionAdicional : null,
                        celular1Activo: false,
                        celular2Activo: false,
                        email1Activo: false,
                        email2Activo: false
                    };
                }

                this.alertasTarjetaUsuarioMatch.alertasActivas.push(alertaTarjetaTmp);
            });
        }

        private crearHtmlLista(alertas: IAlertaTarjeta[]): string {
            var htmlString: string = '<ul class="alerta-lista">';
            if (alertas && alertas.length) {
                alertas.forEach(item => {
                    htmlString = `${htmlString}<li>${item.alertaSeleccionada.nombre}</li>`;
                });
            }
            htmlString = `${htmlString}</ul>`;

            return htmlString;
        }
    }
    // export class AlertaNotificacionController extends ServiciosSocio.BaseServiciosSocio {
    //     constructor() {
    //         super('tab-1');
    //     }
    // }
}