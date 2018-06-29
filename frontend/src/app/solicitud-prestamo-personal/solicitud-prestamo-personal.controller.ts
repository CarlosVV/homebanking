namespace Solicitud {
    export class SolicitudPrestamoViewModel {
        idTipoOferta: string;
        idTarjeta: string;
        idBanco: string;
        banco: string;
        esTitular: boolean;
        montoOferta: number;
        montoPrestamo: number;
        montoMinimo: number;
        cuotas: number;
        tcea: number;
        montoCuota: number;
        tipoCuenta: string;
        tipoMoneda: string;
        numeroCuentaDestino: string;
    }

    interface IRouteParams extends angular.route.IRouteParamsService {
        idTipoOferta: string;
    }

    export class PrestamoPersonalController {
        listaBancos: Common.Banco[];
        listaPlazos: any[] = [{ plazo: 12 }, { plazo: 24 }, { plazo: 36 }];
        listaTarjetas: Tarjeta.DetalleTarjetaViewModel[] = [];
        tarjetaAplicante: any = {};
        solicitudPrestamo: SolicitudPrestamoViewModel;
        socioMultiproducto: boolean = false;
        bannerByCountry: string;
        idTipoOferta: string;
        ofertas: Oferta.OfertaModel[] = [];
        ofertaDetalle: Oferta.OfertaModel;
        tabSeleccionado = 'tab-1';
        content: Confirmacion.IContent;

        constructor(private tarjetasApi: Api.TarjetasApi, private $location: angular.ILocationService, private bancoFactory: Common.BancoFactory, private sessionFactory: Common.SessionFactory,
            private $filter: angular.IFilterService, private $routeParams: IRouteParams, private $scope: ng.IScope) {
            this.idTipoOferta = this.$routeParams.idTipoOferta;
            this.$scope.$on('activarMenu', ((event, data) => { this.activarMenu(data); }));
        }

        inicializar() {
            this.listarBancos();
            this.cargarSolicitudPrestamo();
            this.obtenerTarjetas();
            this.processBannerByCountry();
        }

        cargarSolicitudPrestamo() {
            this.ofertas = this.sessionFactory.ofertas;

            this.ofertas.forEach(_oferta => {
                if (_oferta.idTipoOferta === this.idTipoOferta) {
                    this.ofertaDetalle = _oferta;
                }
            });

            this.solicitudPrestamo = {
                idTipoOferta: this.idTipoOferta,
                idTarjeta: this.ofertaDetalle.idTarjeta,
                idBanco: '',
                banco: '',
                esTitular: true,
                montoOferta: this.ofertaDetalle.montoOferta,
                montoPrestamo: this.ofertaDetalle.montoOferta,
                cuotas: 36,
                montoMinimo: 2500,
                tcea: this.ofertaDetalle.tcea,
                montoCuota: 0,
                tipoCuenta: 'ahorros',
                tipoMoneda: 'soles',
                numeroCuentaDestino: ''
            };

            this.calcularMontoMensual();
        }

        listarBancos() {
            this.bancoFactory.get()
                .then((response: angular.IHttpPromiseCallbackArg<Common.Banco[]>) => {
                    if (response.status === 200) {
                        if (response.data) {
                            this.listaBancos = response.data.map((b: Common.Banco) => {
                                let banco = new Common.Banco();
                                banco.id = b.id;
                                banco.nombre = b.nombre;
                                return banco;
                            });
                            // this.bancoPorDefecto = this.listaBancos[0].id;
                            // this.solicitudPrestamo.idBanco = this.listaBancos[0].id;
                        }
                    } else {
                        console.log('Lo sentimos la aplicación Diners Club Online se detuvo');
                    }
                }, () => {
                    console.log('Lo sentimos la aplicación Diners Club Online se detuvo');
                });
        }

        obtenerTarjetas() {
            this.tarjetasApi.get()
                .then((response: angular.IHttpPromiseCallbackArg<Api.ITarjeta[]>) => {
                    let tarjetas: Api.ITarjeta[];
                    tarjetas = response.data;
                    tarjetas.forEach(element => {
                        let tarjeta = new Tarjeta.DetalleTarjetaViewModel();
                        tarjeta.idTarjeta = element.idTarjeta;
                        tarjeta.numeroTarjeta = element.numeroTarjeta;
                        tarjeta.nombreProducto = element.nombreProducto;
                        tarjeta.alias = element.alias;
                        tarjeta.lineaCreditoSoles = element.lineaCreditoSoles;
                        tarjeta.lineaCreditoDolares = element.lineaCreditoDolares;
                        tarjeta.lineaDisponibleSoles = element.lineaDisponibleSoles;
                        tarjeta.lineaDisponibleDolares = element.lineaDisponibleDolares;
                        tarjeta.millasDisponibles = element.millasDisponibles;
                        tarjeta.montoTotalMesSoles = element.montoTotalMesSoles;
                        tarjeta.montoTotalMesDolares = element.montoTotalMesDolares;
                        tarjeta.montoMinimoMesSoles = element.montoMinimoMesSoles;
                        tarjeta.montoTotalMesDolares = element.montoTotalMesDolares;
                        tarjeta.montoMinimoMesDolares = element.montoMinimoMesDolares;
                        tarjeta.fechaVencimiento = element.fechaVencimiento;
                        tarjeta.adicionales = element.adicionales;
                        tarjeta.tieneAdicionales = element.tieneAdicionales;
                        tarjeta.esTitular = true;
                        tarjeta.socioTarjeta = element.socioTarjeta;

                        this.listaTarjetas.push(tarjeta);

                        if (tarjeta.adicionales.length > 0) {
                            tarjeta.adicionales.forEach(item => {
                                let tarjetaAdicional = new Tarjeta.DetalleTarjetaViewModel();
                                tarjetaAdicional.idTarjeta = item.idTarjeta;
                                tarjetaAdicional.numeroTarjeta = item.numeroTarjeta;
                                tarjetaAdicional.nombreProducto = item.nombreProducto;
                                tarjetaAdicional.lineaCreditoSoles = item.lineaCreditoSoles;
                                tarjetaAdicional.lineaCreditoDolares = item.lineaCreditoDolares;
                                tarjetaAdicional.lineaDisponibleSoles = item.lineaDisponibleSoles;
                                tarjetaAdicional.lineaDisponibleDolares = item.lineaDisponibleDolares;
                                tarjetaAdicional.millasDisponibles = item.millasDisponibles;
                                tarjetaAdicional.montoTotalMesSoles = item.montoTotalMesSoles;
                                tarjetaAdicional.montoTotalMesDolares = item.montoTotalMesDolares;
                                tarjetaAdicional.montoMinimoMesSoles = item.montoMinimoMesSoles;
                                tarjetaAdicional.montoTotalMesDolares = item.montoTotalMesDolares;
                                tarjetaAdicional.montoMinimoMesDolares = item.montoMinimoMesDolares;
                                tarjetaAdicional.fechaVencimiento = item.fechaVencimiento;
                                tarjetaAdicional.adicionales = item.adicionales;
                                tarjetaAdicional.tieneAdicionales = item.tieneAdicionales;
                                tarjetaAdicional.esTitular = false;
                                tarjetaAdicional.socioTarjeta = item.socioTarjeta;

                                this.listaTarjetas.push(tarjetaAdicional);
                            });
                        }
                    });

                    this.obtenerTarjetaAplicante();
                    this.socioMultiproducto = this.listaTarjetas.length > 1;
                }, () => {
                    console.log('Lo sentimos la aplicación Diners Club Online se detuvo');
                });
        }

        obtenerTarjetaAplicante() {
            this.listaTarjetas.forEach(item => {
                if (item.idTarjeta === this.solicitudPrestamo.idTarjeta) {
                    let tarjeta = new Tarjeta.DetalleTarjetaViewModel();
                    tarjeta.idTarjeta = item.idTarjeta;
                    tarjeta.alias = item.alias;
                    tarjeta.nombreProducto = item.nombreProducto;
                    tarjeta.esTitular = item.esTitular;
                    tarjeta.socioTarjeta = item.socioTarjeta;
                    tarjeta.numeroTarjeta = item.numeroTarjeta;

                    this.tarjetaAplicante = tarjeta;
                }
            });
        }

        IrAConfirmacion() {
            var solicitud = this.solicitudPrestamo;

            var contenido: Confirmacion.IContent = {
                titulo: 'Solicitud de Préstamo Personal Diners',
                subTitulo: '',
                subTituloMensaje: '',
                botonTexto: 'CONFIRMAR',
                codigo: 'c02',
                volverTabNombre: 'tab-1',
                siguienteTabNombre: 'tab-3',
                endPoint: '/solicitudes/prestamo-personal',
                forzarConfirmacion: true,
                valores: [
                    { key: 'Tarjeta', value: this.mostrarAlias_o_NombreProducto(this.tarjetaAplicante.alias, this.tarjetaAplicante.nombreProducto) + ' ' + this.formatearNumeroTarjeta(this.tarjetaAplicante.numeroTarjeta) },
                    { key: 'Monto', value: this.monedaFormato(solicitud.montoPrestamo).toString() },
                    { key: 'N° Cuotas', value: solicitud.cuotas.toString() },
                    { key: 'T.C.E.A. <div style="width:100%"><p class="datos-obligatorios" style="color:gray; text-align:right;"> Tasa de Costo Efectivo Anual</p> </div>', value: this.formatoDecimal(solicitud.tcea).toString() + ' %' },
                    { key: 'Monto cuota <p class="datos-obligatorios" style="color:gray; text-align:right;">(referencial)</p>', value: this.monedaFormato(solicitud.montoCuota).toString() },
                    { key: '', value: '' },
                    { key: 'Nombre del banco', value: this.obtenerNombreBanco(solicitud.idBanco) },
                    { key: 'Moneda', value: (solicitud.tipoMoneda === 'soles' ? 'Soles' : 'Dolares') },
                    { key: 'Cuenta y Nº', value: this.obtenerTipoCuenta(solicitud.tipoCuenta) + ' ' + solicitud.numeroCuentaDestino }
                ],
                mostrarOfertas: true,
                idModal: 'prestamo-personal-01'
            };

            let data: any = {
                idTipoOferta: this.solicitudPrestamo.idTipoOferta,
                idBanco: this.solicitudPrestamo.idBanco,
                idTarjeta: this.solicitudPrestamo.idTarjeta,
                banco: this.obtenerNombreBanco(this.solicitudPrestamo.idBanco),
                numeroCuentaDestino: this.solicitudPrestamo.numeroCuentaDestino,
                montoPrestamo: this.solicitudPrestamo.montoPrestamo,
                cuotas: this.solicitudPrestamo.cuotas,
                tcea: this.solicitudPrestamo.tcea,
                montoCuota: this.solicitudPrestamo.montoCuota,
                tipoCuenta: this.solicitudPrestamo.tipoCuenta,
                tipoMoneda: this.solicitudPrestamo.tipoMoneda,
                datosCorreo: contenido.valores,
                nombreProducto: this.tarjetaAplicante.nombreProducto,
                numeroTarjeta: this.tarjetaAplicante.numeroTarjeta
            };

            this.content = contenido;
            this.sessionFactory.confirmacion = contenido;
            this.sessionFactory.confirmacionData = data;
            this.activarMenu('tab-2');
        }

        calcularMontoMensual() {
            let montoPrestamo: number = this.solicitudPrestamo.montoPrestamo;
            let tcea: number = this.solicitudPrestamo.tcea; // se está considerando a TCEA como TEA
            let plazo: number = this.solicitudPrestamo.cuotas;
            let tasaEfectivaMensual: number;
            let montoCuota: number;

            tasaEfectivaMensual = Math.pow(1.0 + tcea / 100.00, 1.0 / 12.0) - 1;
            montoCuota = (montoPrestamo * tasaEfectivaMensual) / (1 - Math.pow(1 + tasaEfectivaMensual, -plazo));
            this.solicitudPrestamo.montoCuota = montoCuota;
        }

        obtenerNombreBanco(id: string): string {
            let nombreBanco: string;
            this.listaBancos.forEach(item => {
                if (item.id === id) {
                    nombreBanco = item.nombre;
                }
            });
            return nombreBanco;
        }

        formatearNumeroTarjeta(numeroTarjeta: string): string {
            return Common.Utility.formatearNumeroTarjeta(numeroTarjeta);
        }

        mostrarAlias_o_NombreProducto(alias: string, nombreProducto: string): string {
            return Common.Utility.mostrarAlias_o_NombreProducto(alias, nombreProducto);
        }

        monedaFormato(value: number): string {
            return this.$filter('currency')(value, 'S/ ', 2);
        }

        formatoDecimal(value: number): string {
            return this.$filter('currency')(value, '', 2);
        }

        obtenerTipoCuenta(value: string): string {
            if (value === 'ahorros') {
                return 'Cuenta de Ahorros';
            } else {
                return 'Cuenta Corriente';
            }
        }

        processBannerByCountry() {
            var isLocal = this.sessionFactory.geolocation.isLocal;
            this.bannerByCountry = Common.Utility.processBannerByCountry(isLocal);
        }

        activarMenu(menuItem: string): void {
            this.tabSeleccionado = menuItem;
        }

        menuSeleccionado(menuItem: string): boolean {
            return this.tabSeleccionado === menuItem;
        }
    }
}