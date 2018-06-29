namespace SolicitudDebito {
    interface IRouteParams extends ng.route.IRouteParamsService {
        tarjetaId: string;
    }
    export class SolicitudDebitoController {
        tarjetas: Tarjeta.DetalleTarjetaViewModel[] = [];
        solicitud: SolicitudDebitoViewModel;
        listaBancos: Common.Banco[];
        listaTiposPago: Common.TipoPago[];
        muestraFacturacionDolares: boolean = false;
        muestraFacturacionSoles: boolean = true;
        facturacionSolesValida: boolean = false;
        facturacionDolaresValida: boolean = false;
        muestraErrores: boolean = false;
        tabSeleccionado = 'tab-1';
        content: Confirmacion.IContent;
        // validaciones
        bancoSolesError: boolean = false;
        numeroCuentaSolesError: boolean = false;
        bancoDolaresError: boolean = false;
        numeroCuentaDolaresError: boolean = false;
        tipoPagoaCargarError: boolean = false;

        constructor(private tarjetasApi: Api.TarjetasApi, $rootScope: angular.IRootScopeService,
            private $routeParams: IRouteParams, private bancos: Common.BancoFactory, private tipoPago: Common.TipoPagoFactory,
            private sessionStorage: Common.SessionFactory, private $location: angular.ILocationService, private configuracion: any, private $scope: angular.IScope) {
            // super($rootScope);
            this.$scope.$on('activarMenu', ((event, data) => { this.activarMenu(data); }));
        }

        inicio() {
            this.solicitud = new SolicitudDebitoViewModel();
            this.solicitud.facturacionSoles = new SolicitudDebito.SolicitudDebitoFacturacionViewModel();
            this.solicitud.facturacionDolares = new SolicitudDebito.SolicitudDebitoFacturacionViewModel();
            this.solicitud.idSocio = this.sessionStorage.documentoIdentidad;
            this.solicitud.facturacionSoles.tipoCuenta = 'Ahorros';
            this.solicitud.facturacionSoles.monedaDelaCta = 'Soles';
            this.solicitud.facturacionDolares.tipoCuenta = 'Ahorros';
            this.solicitud.facturacionDolares.monedaDelaCta = 'Soles';
            if (this.solicitudDesdeOferta()) {
                this.obtenerTarjetaDetalle();
            } else {
                this.listarTarjetas();
            }

            this.listarBancos();
            this.listarTiposPago();
        }

        listarBancos() {
            let contador: number = 0;
            this.bancos.get()
                .then((response: angular.IHttpPromiseCallbackArg<Common.Banco[]>) => {
                    if (response.status === 200) {
                        if (response.data) {
                            this.listaBancos = response.data.map((b: Common.Banco) => {
                                if (contador <= 0) {
                                    // this.facturacionSolesBancoId = b.id;
                                    // this.facturacionDolaresBancoId = b.id;
                                    // this.solicitud.facturacionSoles.idBanco = b.id;
                                    // this.solicitud.facturacionDolares.idBanco = b.id;
                                }
                                contador += 1;
                                let banco = new Common.Banco();
                                banco.id = b.id;
                                banco.nombre = b.nombre;
                                return banco;
                            });
                        }
                    } else {
                        console.log('Lo sentimos la aplicación Diners Club Online se detuvo');
                    }
                }, () => {
                    console.log('Lo sentimos la aplicación Diners Club Online se detuvo');
                });
        }
        listarTiposPago() {
            let contadorTipoPago: number = 0;
            this.tipoPago.get()
                .then((response: angular.IHttpPromiseCallbackArg<Common.TipoPago[]>) => {
                    if (response.status === 200) {
                        if (response.data) {
                            this.listaTiposPago = response.data.map((b: Common.TipoPago) => {
                                // if (contadorTipoPago <= 0) {
                                //     this.solicitud.tipoPagoaCargar = b.id;
                                // }
                                contadorTipoPago += 1;
                                let tipoPago = new Common.TipoPago();
                                tipoPago.id = b.id;
                                tipoPago.nombre = b.nombre;
                                return tipoPago;
                            });
                        }
                    } else {
                        console.log('Lo sentimos la aplicación Diners Club Online se detuvo');
                    }
                }, () => {
                    console.log('Lo sentimos la aplicación Diners Club Online se detuvo');
                });
        }
        obtenerTarjetaDetalle() {
            this.tarjetasApi.getDetalle(this.$routeParams.tarjetaId)
                .then((response: angular.IHttpPromiseCallbackArg<Api.ITarjeta>) => {
                    if (response.status === 200) {
                        if (response.data) {
                            let tarjeta = new Tarjeta.DetalleTarjetaViewModel();
                            tarjeta.idTarjeta = response.data.idTarjeta;
                            tarjeta.alias = response.data.alias ? response.data.alias : response.data.nombreProducto;
                            tarjeta.numeroTarjeta = `${tarjeta.alias} ${Common.Utility.mostrarNumeroTarjetaConAsterisco(response.data.numeroTarjeta)}`;
                            tarjeta.nombreProducto = response.data.nombreProducto;
                            this.tarjetas.push(tarjeta);

                            this.solicitud.idTarjeta = response.data.idTarjeta;
                            this.solicitud.numeroTarjeta = response.data.numeroTarjeta;
                            this.solicitud.nombreProducto = response.data.nombreProducto;
                            // this.tarjeta.lineaCreditoSoles = response.data.lineaCreditoSoles;
                            // this.tarjeta.lineaCreditoDolares = response.data.lineaCreditoDolares;
                        }
                    } else {
                        console.log('Lo sentimos la aplicación Diners Club Online se detuvo');
                    }
                }, () => {
                    console.log('Lo sentimos la aplicación Diners Club Online se detuvo');
                });
        }
        listarTarjetas() {
            let contador: number = 0;
            this.tarjetasApi.get()
                .then((response: angular.IHttpPromiseCallbackArg<Api.ITarjeta[]>) => {
                    if (response.data) {
                        this.tarjetas = response.data.map((x: Api.ITarjeta) => {
                            if (contador <= 0) {
                                this.solicitud.idTarjeta = x.idTarjeta;
                            }
                            contador += 1;
                            let tarjeta = new Tarjeta.DetalleTarjetaViewModel();
                            tarjeta.idTarjeta = x.idTarjeta;
                            tarjeta.alias = x.alias ? x.alias : x.nombreProducto;
                            tarjeta.numeroTarjeta = `${tarjeta.alias} ${Common.Utility.mostrarNumeroTarjetaConAsterisco(x.numeroTarjeta)}`;
                            tarjeta.fechaVencimiento = x.fechaVencimiento;
                            tarjeta.tieneAdicionales = x.tieneAdicionales;
                            // tarjeta.srcImagenTarjeta = `Content/images/${x.nombreProducto}.png`;
                            return tarjeta;
                        });
                    } else {
                        console.log('Lo sentimos la aplicación Diners Club Online se detuvo');
                    }
                }, () => {
                    console.log('Lo sentimos la aplicación Diners Club Online se detuvo');
                });
        }

        enviarSolicitud() {
            this.muestraErrores = false;
            if (!this.validarDatosFacturacion()) {
                // mostrar mensajes de error
                console.log('faltan ingresar los datos obligatorios');
                // this.muestraErrores = true;
                return;
            }
            let tarjetaDescripcion: string = '';
            let rutaVolver: string = '';
            let numeroTarjeta: string = '';
            if (this.solicitudDesdeOferta()) {
                tarjetaDescripcion = `${this.tarjetas[0].alias} ${Common.Utility.mostrarNumeroTarjetaConAsterisco(this.solicitud.numeroTarjeta)}`;
                rutaVolver = '/solicitud-afiliacion-debito/' + this.solicitud.idTarjeta;
                this.solicitud.idTipoOferta = '';
            } else {
                let filtroTarjetas = this.tarjetas.filter(this.filtrarTarjetasPorIdtarjeta(this.solicitud.idTarjeta));
                filtroTarjetas.forEach(element => {
                    tarjetaDescripcion = `${element.alias} ${Common.Utility.mostrarNumeroTarjetaConAsterisco(element.numeroTarjeta)}`;
                    numeroTarjeta = element.numeroTarjeta;
                });
                rutaVolver = '/solicitud-afiliacion-debito';
                this.solicitud.idTipoOferta = '';
                this.solicitud.numeroTarjeta = numeroTarjeta;
            }
            let bancoSeleccionado: any = this.listaBancos.filter(this.seleccionarBanco(this.solicitud.facturacionSoles.idBanco));
            let tipoPagoSeleccionado: any = this.listaTiposPago.filter(this.seleccionarTipoPago(this.solicitud.tipoPagoaCargar));
            let valoresFacturacion: any = [];
            valoresFacturacion.push({ key: 'Tarjeta', value: tarjetaDescripcion });
            if (this.facturacionSolesValida) {
                this.solicitud.facturacionSoles.banco = bancoSeleccionado[0].nombre;
                valoresFacturacion.push({ key: '<strong>Facturación en Soles</strong>', value: '' });
                valoresFacturacion.push({ key: 'Nombre del banco', value: bancoSeleccionado[0].nombre });
                valoresFacturacion.push({ key: 'Tipo de cuenta', value: this.solicitud.facturacionSoles.tipoCuenta });
                valoresFacturacion.push({ key: 'Nº Cuenta', value: this.solicitud.facturacionSoles.numeroCuenta });
                valoresFacturacion.push({ key: 'Moneda de la cuenta', value: this.solicitud.facturacionSoles.monedaDelaCta });
            }
            if (this.facturacionDolaresValida) {
                bancoSeleccionado = this.listaBancos.filter(this.seleccionarBanco(this.solicitud.facturacionDolares.idBanco));
                this.solicitud.facturacionDolares.banco = bancoSeleccionado[0].nombre;
                valoresFacturacion.push({ key: '<strong>Facturación en Dólares</strong>', value: '' });
                valoresFacturacion.push({ key: 'Nombre del banco', value: bancoSeleccionado[0].nombre });
                valoresFacturacion.push({ key: 'Tipo de cuenta', value: this.solicitud.facturacionDolares.tipoCuenta });
                valoresFacturacion.push({ key: 'Nº Cuenta', value:  this.solicitud.facturacionDolares.numeroCuenta });
                valoresFacturacion.push({ key: 'Moneda de la cuenta', value: this.solicitud.facturacionDolares.monedaDelaCta });
            } else {
                this.solicitud.facturacionDolares.idBanco = '';
            }
            valoresFacturacion.push({ key: 'Tipo de pago', value: tipoPagoSeleccionado[0].nombre });
            let contenido: Confirmacion.IContent = {
                idModal: 'cargo02',
                titulo: 'Solicitud de Débito Automático',
                subTitulo: '',
                subTituloMensaje: '',
                botonTexto: 'CONFIRMAR',
                codigo: 'c01',
                siguienteTabNombre: 'tab-3',
                volverTabNombre: 'tab-1',
                endPoint: '/solicitudes/debito-automatico',
                forzarConfirmacion: true,
                valores: valoresFacturacion,
                mostrarOfertas: true
            };
            // let objSolicitud =
            this.sessionStorage.confirmacion = contenido;
            this.sessionStorage.confirmacionData = this.solicitud;
            this.content = contenido;
            // this.$location.url('/confirmacion');
            this.activarMenu('tab-2');
        }
        guardarComoFrecuenteFacturacionSoles() {
            // this.solicitud.facturacionSoles.guardarComoFrecuente = true;
        }

        guardarComoFrecuenteFacturacionDolares() {
            // this.solicitud.facturacionDolares.guardarComoFrecuente = true;
        }
        solicitudDesdeOferta(): boolean {
            if (this.$routeParams.tarjetaId) {
                return true;
            }
            return false;
        }
        activarMenu(menuItem: string): void {
            this.tabSeleccionado = menuItem;
        }

        menuSeleccionado(menuItem: string): boolean {
            return this.tabSeleccionado === menuItem;
        }
        private validarDatosFacturacion(): boolean {
            this.facturacionSolesValida = false;
            this.facturacionDolaresValida = false;
            this.bancoSolesError = false;
            this.numeroCuentaSolesError = false;
            this.bancoDolaresError = false;
            this.numeroCuentaDolaresError = false;
            this.tipoPagoaCargarError = false;

            if (this.muestraFacturacionSoles) {
                if ((this.solicitud.facturacionSoles.idBanco && this.solicitud.facturacionSoles.idBanco !== '') &&
                    (this.solicitud.facturacionSoles.tipoCuenta && this.solicitud.facturacionSoles.tipoCuenta !== '') &&
                    (this.solicitud.facturacionSoles.monedaDelaCta && this.solicitud.facturacionSoles.monedaDelaCta !== '') &&
                    (this.solicitud.facturacionSoles.numeroCuenta && this.solicitud.facturacionSoles.numeroCuenta !== '')) {
                    this.facturacionSolesValida = true;
                } else {
                    if (!this.solicitud.facturacionSoles.idBanco || this.solicitud.facturacionSoles.idBanco === '') {
                        this.bancoSolesError = true;
                    }
                    if (!this.solicitud.facturacionSoles.numeroCuenta || this.solicitud.facturacionSoles.numeroCuenta === '') {
                        this.numeroCuentaSolesError = true;
                    }
                }
            }
            if (this.muestraFacturacionDolares) {
                if ((this.solicitud.facturacionDolares.idBanco && this.solicitud.facturacionDolares.idBanco !== '') &&
                    (this.solicitud.facturacionDolares.tipoCuenta && this.solicitud.facturacionDolares.tipoCuenta !== '') &&
                    (this.solicitud.facturacionDolares.monedaDelaCta && this.solicitud.facturacionDolares.monedaDelaCta !== '') &&
                    (this.solicitud.facturacionDolares.numeroCuenta && this.solicitud.facturacionDolares.numeroCuenta !== '')) {
                    this.facturacionDolaresValida = true;
                } else {
                    if (!this.solicitud.facturacionDolares.idBanco || this.solicitud.facturacionDolares.idBanco === '') {
                        this.bancoDolaresError = true;
                    }
                    if (!this.solicitud.facturacionDolares.numeroCuenta || this.solicitud.facturacionDolares.numeroCuenta === '') {
                        this.numeroCuentaDolaresError = true;
                    }
                }
            }
            if (!this.solicitud.tipoPagoaCargar || this.solicitud.tipoPagoaCargar === '') {
                 this.tipoPagoaCargarError = true;
                return false;
            }
            if ((this.muestraFacturacionSoles && !this.facturacionSolesValida) || (this.muestraFacturacionDolares && !this.facturacionDolaresValida)) {
                return false;
            }
            if (!this.muestraFacturacionSoles && !this.muestraFacturacionDolares) {
                this.muestraErrores = true;
                return false;
            }
            return true;
        }

        private seleccionarBanco(idBanco: string) {
            return function (elemento: any): any {
                return (elemento.id === idBanco);
            };
        }

        private seleccionarTipoPago(tipoPago: string) {
            return function (elemento: any): any {
                return (elemento.id === tipoPago);
            };
        }

        private filtrarTarjetasPorIdtarjeta(idTarjeta: string) {
            return function (elemento: any): boolean {
                return (elemento.idTarjeta === idTarjeta);
            };
        }
    }
}