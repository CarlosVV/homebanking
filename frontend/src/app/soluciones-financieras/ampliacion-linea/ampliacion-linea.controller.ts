namespace AmpliacionCredito {
    interface IRouteParams extends ng.route.IRouteParamsService {
        idTipoOferta: string;
    }
    export class AmpliacionLineaController {
        tarjetas: Tarjeta.DetalleTarjetaViewModel[] = [];
        solicitud: AmpliacionLineaDebitoViewModel;
        muestraErrores: boolean = false;
        tabSeleccionado = 'tab-1';
        content: Confirmacion.IContent;
        ofertas: Oferta.OfertaModel[] = [];
        ofertaDetalle: Oferta.OfertaModel;
        montoCreditoSolicitadoCorrecto: boolean = true;
        mensajeErrorCreditoSolicitado: string;
        montoOfertado: number;
        deshabilitarMonto: boolean = false;
        creditoSolicitadoFormateado: string;
        constructor(private tarjetasApi: Api.TarjetasApi, $rootScope: angular.IRootScopeService,
            private $routeParams: IRouteParams, private sessionStorage: Common.SessionFactory, private $location: angular.ILocationService,
            private configuracion: any, private $scope: angular.IScope) {
            this.$scope.$on('activarMenu', ((event, data) => { this.activarMenu(data); }));
        }

        inicio() {
            this.ofertas = this.sessionStorage.ofertas;
            this.solicitud = new AmpliacionLineaDebitoViewModel();
            this.solicitud.idSocio = this.sessionStorage.documentoIdentidad;
            if (this.solicitudDesdeOferta()) {
                let resultadoOfertas = this.ofertas.filter(this.filtrarOfertasPorIdTipoOferta(this.$routeParams.idTipoOferta));
                if (resultadoOfertas && resultadoOfertas.length > 0) {
                    this.ofertaDetalle = resultadoOfertas[0];
                    this.obtenerTarjetaDetalle(this.ofertaDetalle.idTarjeta);
                    this.montoOfertado = this.ofertaDetalle.montoLineaNueva;
                    this.solicitud.creditoSolicitado = this.ofertaDetalle.montoLineaNueva;
                    this.creditoSolicitadoFormateado = Common.Utility.FormatearMontosAdosDecimales(this.ofertaDetalle.montoLineaNueva);
                }
                this.deshabilitarMonto = true;
            } else {
                this.listarTarjetas();
                this.deshabilitarMonto = false;
            }
        }

        obtenerTarjetaDetalle(idTarjeta: string) {
            this.tarjetasApi.getDetalle(idTarjeta)
                .then((response: angular.IHttpPromiseCallbackArg<Api.ITarjeta>) => {
                    if (response.status === 200) {
                        if (response.data) {
                            let tarjeta = new Tarjeta.DetalleTarjetaViewModel();
                            tarjeta.idTarjeta = response.data.idTarjeta;
                            tarjeta.alias = response.data.alias ? response.data.alias : response.data.nombreProducto;
                            tarjeta.numeroTarjeta = `${tarjeta.alias} ${Common.Utility.mostrarNumeroTarjetaConAsterisco(response.data.numeroTarjeta)}`;
                            tarjeta.nombreProducto = response.data.nombreProducto;
                            tarjeta.lineaCreditoSoles = response.data.lineaCreditoSoles;
                            this.tarjetas.push(tarjeta);

                            this.solicitud.idTarjeta = response.data.idTarjeta;
                            this.solicitud.numeroTarjeta = response.data.numeroTarjeta;
                            this.solicitud.nombreProducto = response.data.nombreProducto;
                            this.solicitud.creditoActual = response.data.lineaCreditoSoles;

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
                                this.solicitud.creditoActual = x.lineaCreditoSoles;
                            }
                            contador += 1;
                            let tarjeta = new Tarjeta.DetalleTarjetaViewModel();
                            tarjeta.idTarjeta = x.idTarjeta;
                            tarjeta.alias = x.alias ? x.alias : x.nombreProducto;
                            tarjeta.numeroTarjeta = `${tarjeta.alias} ${Common.Utility.mostrarNumeroTarjetaConAsterisco(x.numeroTarjeta)}`;
                            tarjeta.fechaVencimiento = x.fechaVencimiento;
                            tarjeta.tieneAdicionales = x.tieneAdicionales;
                            tarjeta.lineaCreditoSoles = x.lineaCreditoSoles;
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
            if (!this.validarDatosSolicitud()) {
                // mostrar mensajes de error
                console.log('faltan ingresar los datos requeridos');
                this.muestraErrores = true;
                return;
            }
            let tarjetaDescripcion: string = '';
            let rutaVolver: string = '';
            let numeroTarjeta: string = '';
            if (this.solicitudDesdeOferta()) {
                tarjetaDescripcion = `${this.tarjetas[0].alias} ${Common.Utility.mostrarNumeroTarjetaConAsterisco(this.solicitud.numeroTarjeta)}`;
                rutaVolver = '/solicitud/ampliacion-linea/' + this.solicitud.idTipoOferta;
                this.solicitud.idTipoOferta = '';
            } else {
                let filtroTarjetas = this.tarjetas.filter(this.filtrarTarjetasPorIdtarjeta(this.solicitud.idTarjeta));
                filtroTarjetas.forEach(element => {
                    tarjetaDescripcion = `${element.alias} ${Common.Utility.mostrarNumeroTarjetaConAsterisco(element.numeroTarjeta)}`;
                    numeroTarjeta = element.numeroTarjeta;
                });
                rutaVolver = '/solicitud/ampliacion-linea';
                this.solicitud.idTipoOferta = '';
                this.solicitud.numeroTarjeta = numeroTarjeta;
            }
            let valoresFacturacion: any = [];
            valoresFacturacion.push({ key: 'Tarjeta', value: tarjetaDescripcion });
            valoresFacturacion.push({ key: 'Línea de crédito actual', value: Common.Utility.FormatearMontos(this.solicitud.creditoActual, 'S/') });
            valoresFacturacion.push({ key: 'Línea de crédito nueva', value: Common.Utility.FormatearMontos(this.solicitud.creditoSolicitado, 'S/') });

            let contenido: Confirmacion.IContent = {
                idModal: 'cargo11',
                titulo: 'Solicitud de Incremento de Línea',
                subTitulo: '',
                subTituloMensaje: '',
                botonTexto: 'CONFIRMAR',
                codigo: 'c01',
                siguienteTabNombre: 'tab-3',
                volverTabNombre: 'tab-1',
                endPoint: '/solicitudes/ampliacion-credito',
                forzarConfirmacion: true,
                valores: valoresFacturacion,
                mostrarOfertas: true
            };
            this.sessionStorage.confirmacion = contenido;
            this.sessionStorage.confirmacionData = this.solicitud;
            this.content = contenido;
            this.activarMenu('tab-2');
        }

        solicitudDesdeOferta(): boolean {
            if (this.$routeParams.idTipoOferta) {
                return true;
            }
            return false;
        }

        seleccionarTarjeta(idTarjeta: string) {
            var tarjetaSeleccionada = this.tarjetas.filter(this.filtrarTarjetasPorIdtarjeta(idTarjeta));
            if (tarjetaSeleccionada && tarjetaSeleccionada.length > 0) {
                this.solicitud.creditoActual = tarjetaSeleccionada[0].lineaCreditoSoles;
            }
        }
        activarMenu(menuItem: string): void {
            this.tabSeleccionado = menuItem;
        }

        menuSeleccionado(menuItem: string): boolean {
            return this.tabSeleccionado === menuItem;
        }

        private validarDatosSolicitud(): boolean {
            this.montoCreditoSolicitadoCorrecto = true;
            this.mensajeErrorCreditoSolicitado = '';
            this.solicitud.creditoSolicitado = Number(this.creditoSolicitadoFormateado);
            if (!this.solicitud.creditoSolicitado) {
                this.mensajeErrorCreditoSolicitado = 'Debe ingresar monto solicitado';
                this.montoCreditoSolicitadoCorrecto = false;
                return false;
            }
            if (this.solicitudDesdeOferta()) {
                if (this.solicitud.creditoSolicitado > this.montoOfertado) {
                    this.mensajeErrorCreditoSolicitado = 'Monto debe ser menor o igual a la oferta';
                    this.montoCreditoSolicitadoCorrecto = false;
                    return false;
                }
            }
            if (this.solicitud.creditoSolicitado <= this.solicitud.creditoActual) {
                this.mensajeErrorCreditoSolicitado = 'Monto debe ser mayor a la línea de crédito actual';
                this.montoCreditoSolicitadoCorrecto = false;
                return false;
            }
            if (!Common.Utility.validarFormatoMontos(this.solicitud.creditoSolicitado)) {
                this.mensajeErrorCreditoSolicitado = 'Monto incorrecto';
                this.montoCreditoSolicitadoCorrecto = false;
                return false;
            }
            return true;
        }
        private filtrarTarjetasPorIdtarjeta(idTarjeta: string) {
            return function (elemento: any): boolean {
                return (elemento.idTarjeta === idTarjeta);
            };
        }

        private filtrarOfertasPorIdTipoOferta(idTipoOferta: string) {
            return function (elemento: any): boolean {
                return (elemento.idTipoOferta === idTipoOferta);
            };
        }
    }
}