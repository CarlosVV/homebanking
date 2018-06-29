namespace EstadoCuenta {
    export class EstadoCuentaController {
        estadoCuenta: EstadoCuentaModel;
        idTarjeta: string;
        fechaActual: Date;
        colorFechaPago: string;
        linksDescargaEstadoCuenta: any[] = [];
        linkSeleccionado: string;
        tieneEstadoCuentaDisponible: boolean = true;
        mensajesEstadoCuenta: string;
        tarjetaConMillas: boolean;
        constructor(private estadoCuentaFactory: EstadoCuentaFactory, private sessionFactory: Common.SessionFactory, private $location: angular.ILocationService,
            private tarjetaService: Tarjeta.TarjetaService, $rootScope: angular.IRootScopeService, private $rootScope2: angular.IRootScopeService,
            private configuracion: any) {
            this.idTarjeta = this.tarjetaService.getTarjeta();
            this.$rootScope2.$on('obtenerEstadoDeCuenta', ((event, data) => {
                this.idTarjeta = data.idTarjeta;
                this.load();
            }));
        }

        load() {
            this.mensajesEstadoCuenta = '';
            this.obtenerResumenEstadoCuenta(this.idTarjeta);
            this.obtenerLinksDescargaParaEstadoCuenta(this.idTarjeta);
        }
        obtenerResumenEstadoCuenta(idTarjeta: string) {
            this.fechaActual = new Date();
            this.estadoCuentaFactory.obtenerResumenEstadoCuenta(idTarjeta)
                .then((response: angular.IHttpPromiseCallbackArg<any>) => {
                    if (response.data) {
                        this.tieneEstadoCuentaDisponible = true;
                        let resumen = response.data;
                        this.estadoCuenta = new EstadoCuentaModel;
                        this.estadoCuenta.abonoDolares = resumen.abonoDolares;
                        this.estadoCuenta.abonoSoles = resumen.abonoSoles;
                        this.estadoCuenta.comisionesInteresesPenalidadesGastosDolares = resumen.comisionesInteresesPenalidadesGastosDolares;
                        this.estadoCuenta.comisionesInteresesPenalidadesGastosSoles = resumen.comisionesInteresesPenalidadesGastosSoles;
                        this.estadoCuenta.consumosDolares = resumen.consumosDolares;
                        this.estadoCuenta.consumosSoles = resumen.consumosSoles;
                        this.estadoCuenta.deudaAnteriorDolares = resumen.deudaAnteriorDolares;
                        this.estadoCuenta.deudaAnteriorSoles = resumen.deudaAnteriorSoles;
                        this.estadoCuenta.deudaTotalDolares = resumen.deudaTotalDolares;
                        this.estadoCuenta.deudaTotalSoles = resumen.deudaTotalSoles;
                        this.estadoCuenta.estaVencido = resumen.estaVencido;
                        this.estadoCuenta.fechaFinPeriodo = resumen.fechaFinPeriodo;
                        this.estadoCuenta.fechaInicioPeriodo = resumen.fechaInicioPeriodo;
                        this.estadoCuenta.fechaVencimiento = Common.Utility.obtenerFormatoFechaVencimiento(resumen.fechaVencimiento, 'dd/MM/yyyy');
                        this.estadoCuenta.idTarjeta = resumen.idTarjeta;
                        this.estadoCuenta.millasDisponibles = resumen.millasDisponibles;
                        this.estadoCuenta.millasGanadas = resumen.millasGanadas;
                        this.estadoCuenta.millasSaldoAnterior = resumen.millasSaldoAnterior;
                        this.estadoCuenta.millasVencidas = resumen.millasVencidas;
                        this.estadoCuenta.montoAPagarMinimoDolares = resumen.montoAPagarMinimoDolares;
                        this.estadoCuenta.montoAPagarMinimoSoles = resumen.montoAPagarMinimoSoles;
                        this.estadoCuenta.montoAPagarTotalDolares = resumen.montoAPagarTotalDolares;
                        this.estadoCuenta.montoAPagarTotalSoles = resumen.montoAPagarTotalSoles;
                        this.colorFechaPago = Common.Utility.obtenerColorFechaVencimiento(resumen.fechaVencimiento);
                        this.tarjetaConMillas = resumen.acumulaMillas;
                        // this.tarjetaAcumulaMillas();
                    } else {
                        this.tieneEstadoCuentaDisponible = false;
                        this.mensajesEstadoCuenta = this.configuracion.mensajeEstadoCtaNoDisponible;
                    }
                }, () => {
                    console.log('Lo sentimos la aplicación Diners Club Online se detuvo');
                    this.tieneEstadoCuentaDisponible = false;
                    this.mensajesEstadoCuenta = this.configuracion.mensajeErrorWebApi;
                });
        }
        obtenerLinksDescargaParaEstadoCuenta(idTarjeta: string) {
            this.estadoCuentaFactory.obtenerLinksDescargaParaEstadoCuenta(idTarjeta)
                .then((response: angular.IHttpPromiseCallbackArg<any>) => {
                    this.linksDescargaEstadoCuenta = response.data;
                    this.linkSeleccionado = response.data[0] ? response.data[0].linkDescarga : '';
                }, () => {
                    console.log('Lo sentimos la aplicación Diners Club Online se detuvo');
                });
        }
        descargarEstadoCuenta() {
            this.estadoCuentaFactory.descargarEstadoCuenta(this.linkSeleccionado);
        }
        tarjetaAcumulaMillas() {
            let lista = this.tarjetaService.getListaTarjetaConAdicionales();
            if (lista) {
                let idTarjetaTemporal: string = this.idTarjeta;
                let resultadoLista = lista.filter(this.filtrarTarjetasPorIdtarjetaEstadoCta(idTarjetaTemporal));
                resultadoLista.forEach(element => {
                    this.tarjetaConMillas = element.acumulaMillas;
                });
            }
        }
        formatFechaPago(fechaVencimiento: string): string {
            return Common.Utility.obtenerFormatoFechaVencimiento(fechaVencimiento);
        }

        formatTextColor(fechaVencimiento: string) {
            return Common.Utility.obtenerColorFechaVencimiento(fechaVencimiento);
        }
        private filtrarTarjetasPorIdtarjetaEstadoCta(idTarjeta: string) {
            return function (elemento: any): boolean {
                return (elemento.idTarjeta === idTarjeta);
            };
        }
    }
}