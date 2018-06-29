declare let pdfMake: any;
module Movimientos {
    interface IRouteParams extends angular.route.IRouteParamsService {
        tarjetaId: string;
        tieneAdicionales: string;
        tabMenuItem: string;
    }

    interface ISocio {
        idTarjeta: string;
        nombreSocio: string;
    }

    export class MovimientosController extends Common.BaseController {
        tarjetasTitular: Tarjeta.DetalleTarjetaViewModel[] = [];
        movimientosTarjeta: {};
        ultimosmovimientosTarjeta: {};
        tieneAdicionales: boolean;
        tabSeleccionado = 'tab-1';
        datosTarjeta: any = {};
        tieneAdicionalesHistoricos: boolean;
        movimientosHistoricos: any = {};
        tablaMovimientos: any;
        paginationParams: any = {
            currentPage: 1,
            totalPages: 1,
            pages: 1
        };
        cantidadRegistrosPorPagina = 10;
        bannerByCountry: string;
        isLocal: boolean = true;
        rangoFechaInicio: string;
        rangoFechaFin: string;
        listaSocios: ISocio[] = [];
        movimientosHistoricosCargado: boolean = false;
        tieneHistoricosDisponible: boolean = false;
        mensajesMovimientoHistoricos: string;
        tieneUltimosMovimientosDisponible: boolean = false;
        mensajesUltimosMovimientos: string;
        movimientosFilter = {
            tarjetaId: '', // se debe setear con la tarjeta a consultar
            fechaInicio: null as any, // '2015-07-26', //null o formato 2015-07-26
            fechaFin: null as any, // '2016-08-26', //null o formato 2015-07-26
            cantidadMovimientos: this.cantidadRegistrosPorPagina, // 0 o la cantidad de movimientos
            pagina: 1, // 0 o la pagina
            opcion: 'Último mes',
            idTarjetaConsulta: ''
        };
        tarjetaSeleccionada = {
            idTarjeta: ''
        };
        nuevoAlias: string;
        idTarjetaSeleccionada: string;
        tipoDescarga: string;
        usuario: any = {};
        correoDestino: any = { correo: '', esValido: false, enviado: false, tieneDatos: true };

        constructor(private tarjetasApi: Api.TarjetasApi, private movimientosFactory: MovimientosFactory, private $filter: angular.IFilterService, private $routeParams: IRouteParams, $rootScope: angular.IRootScopeService,
            private geoApi: Geolocalizacion.GeoAPi, private tarjetaService: Tarjeta.TarjetaService, private paginationService: Common.PaginationService,
            private configuracion: any, private usuarioApi: Api.UsuarioApi) {
            super($rootScope);
            this.greetings();
            this.movimientosFilter.tarjetaId = this.$routeParams.tarjetaId;
            this.tieneAdicionales = (this.$routeParams.tieneAdicionales != null && this.$routeParams.tieneAdicionales.toLowerCase() === 'false' ? false : true);
            this.tarjetaSeleccionada.idTarjeta = this.$routeParams.tarjetaId;
            this.tarjetaService.setTarjeta(this.movimientosFilter.tarjetaId);
            this.$rootScope.$on('obtenerUltimosMovimientos', ((event, data) => {
                this.obtenerUltimosMovimientos(data.idTarjeta, data.tieneAdicionales);
            }));
            this.$rootScope.$on('obtenerMovimientosHistoricos', ((event, data) => {
                this.movimientosFilter.tarjetaId = data.idTarjeta;
                this.movimientosFilter.idTarjetaConsulta = '';
                this.loadHistoricos();
            }));
            this.movimientosHistoricosCargado = false;
            this.init();

            if (this.$routeParams.tabMenuItem) {
                this.tabSeleccionado = this.$routeParams.tabMenuItem;
            }
        }

        init() {
            this.movimientosFilter.cantidadMovimientos = 5;
            this.obtenerUltimosMovimientos(this.movimientosFilter.tarjetaId, this.tieneAdicionales);
            this.obtenerTarjetasTitular();
            this.obtenerInformacionUsuario();
            this.tipoDescarga = 'excel';
        }

        esTarjetaASeleccionar(idTarjeta: string) {
            if (this.tarjetaSeleccionada.idTarjeta === idTarjeta) {
                return true;
            } else {
                return false;
            }
        }

        loadHistoricos() {
            this.setearFiltroMovimientoRangoTiempo(0);
            this.movimientosFilter.cantidadMovimientos = this.cantidadRegistrosPorPagina;
            this.movimientosFilter.pagina = 1;
            this.movimientosFilter.opcion = 'Última semana';
            this.rangoFechaInicio = '';
            this.rangoFechaFin = '';
            this.obtenerMovimientos(1);
            this.cargarSocios();
        }

        setPage(page: number) {
            if (page < 1 || page > this.paginationParams.totalPages) {
                return;
            }
            this.movimientosFilter.fechaInicio = this.movimientosFilter.fechaInicio || null;
            this.movimientosFilter.fechaFin = this.movimientosFilter.fechaFin || null;
            this.movimientosFilter.cantidadMovimientos = this.cantidadRegistrosPorPagina;
            this.tieneAdicionalesHistoricos = false;
            this.movimientosHistoricos = {};
            this.obtenerMovimientos(page);
        }

        greetings() {
            this.geoApi.isLocal().then((response) => {
                this.isLocal = response.isLocal;
                this.bannerByCountry = Common.Utility.processBannerByCountry(response.isLocal);
            });
        }

        obtenerTarjetasTitular() {
            this.tarjetasApi.get()
                .then((response: angular.IHttpPromiseCallbackArg<Api.ITarjeta[]>) => {
                    let tarjetas = response.data.map((x: Api.ITarjeta) => {
                        let tarjeta = new Tarjeta.DetalleTarjetaViewModel();
                        tarjeta.idTarjeta = x.idTarjeta;
                        tarjeta.nombreProducto = x.nombreProducto;
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
                        tarjeta.alias = x.alias ? x.alias : x.nombreProducto;
                        return tarjeta;
                    });

                    this.tarjetasTitular = tarjetas;
                }, () => {
                    console.log('Lo sentimos la aplicación Diners Club Online se detuvo');
                });
        }

        obtenerMovimientos(page: number) {
            this.movimientosFilter.cantidadMovimientos = this.cantidadRegistrosPorPagina;
            this.movimientosFilter.fechaInicio = moment(this.movimientosFilter.fechaInicio, 'DD/MM/YYYY').toDate();
            this.movimientosFilter.fechaFin = moment(this.movimientosFilter.fechaFin, 'DD/MM/YYYY').toDate();
            this.movimientosFilter.pagina = page;
            this.paginationParams.currentPage = page;

            this.mensajesMovimientoHistoricos = '';
            this.movimientosFactory.obtenerMovimientos(this.movimientosFilter)
                .then((response: angular.IHttpPromiseCallbackArg<any>) => {
                    if (response.data) {
                        this.tieneAdicionalesHistoricos = this.tieneAdicionales;
                        this.movimientosHistoricos = response.data.movimientos;
                        let totalItems = response.data.totalMovimientos;
                        let cantidadPaginas = this.paginationService.GetPager(totalItems, this.paginationParams.currentPage, 10);
                        this.paginationParams.totalPages = cantidadPaginas.totalPages;
                        this.paginationParams.pages = cantidadPaginas.pages;
                    } else {
                        console.log('Lo sentimos la aplicación Diners Club Online se detuvo');
                        this.mensajesMovimientoHistoricos = this.configuracion.mensajeMovimientosDisponible;
                    }

                    this.tieneHistoricosDisponible = (this.movimientosHistoricos.length > 0 ? true : false);
                }, () => {
                    this.tieneHistoricosDisponible = (this.movimientosHistoricos.length > 0 ? true : false);
                    console.log('Lo sentimos la aplicación Diners Club Online se detuvo');
                    this.mensajesMovimientoHistoricos = this.configuracion.mensajeErrorWebApi;
                });
        }

        obtenerInformacionUsuario() {
            this.usuarioApi.get().then((result) => {
                this.usuario = result.data;
                this.correoDestino.correo = this.usuario.emailSeleccionado === '1' ? this.usuario.emailPrincipal : this.usuario.emailAlternativo;
                this.correoDestino.esValido = this.validarCorreo(this.correoDestino.correo);
            });
        }

        procesarDescarga() {
            if (this.movimientosHistoricos.length <= 0) {
                return;
            }
            if (this.tipoDescarga === 'excel') {
                this.descargarMovimientosHistoricos_Excel();
            } else {
                this.descargarMovimientosHistoricos_PDF();
            }
        }

        descargarMovimientosHistoricos_Excel() {
            this.movimientosFactory.descargarMovimientosHistoricos(this.movimientosFilter)
                .then((response: angular.IHttpPromiseCallbackArg<any>) => {
                    let file = new Blob([response.data], {
                        type: response.headers('Content-Type')
                    });
                    saveAs(file, 'Movimientos.xlsx');
                });
        }

        descargarMovimientosHistoricos_PDF() {
            this.movimientosFactory.descargarPdfMovimientosHistoricos(this.movimientosFilter)
                .then((response: angular.IHttpPromiseCallbackArg<any>) => {
                    let file = new Blob([response.data], {
                        type: response.headers('Content-Type')
                    });
                    saveAs(file, 'MovimientosHistoricos.pdf');
                    // let cabeceraTabla = [
                    //     { text: '', style: 'header' },
                    //     { text: 'Fecha', style: 'header' },
                    //     { text: 'Usuario', style: 'header' },
                    //     { text: 'Descripción', style: 'header' },
                    //     { text: 'Lugar', style: 'header' },
                    //     { text: 'Cuotas', style: 'header' },
                    //     { text: 'Importe US$', style: 'header' },
                    //     { text: 'Importe S/', style: 'header' }
                    // ];

                    // let body = response.data;
                    // body.unshift(cabeceraTabla);

                    // let docDefinition = {
                    //     content: [
                    //         { text: 'Movimientos Historicos' },
                    //         {
                    //             style: 'tablaMovi',
                    //             table: {
                    //                 body: body
                    //             }
                    //         }
                    //     ],
                    //     styles: {
                    //         header: {
                    //             bold: true,
                    //             color: '#000',
                    //             fontSize: 10
                    //         },
                    //         tablaMovi: {
                    //             color: '#666',
                    //             fontSize: 10
                    //         }
                    //     }
                    // };

                    // // crear PDf 
                    // pdfMake.createPdf(docDefinition).open();
                });
        }

        enviarCorreo() {
            if (this.movimientosHistoricos.length <= 0) {
                this.correoDestino.tieneDatos = false;
                this.correoDestino.enviado = false;
                this.correoDestino.esValido = true;
                return;
            }

            if (this.validarCorreo(this.correoDestino.correo)) {
                let model: any = {
                    id: this.movimientosFilter.tarjetaId,
                    idTarjetaConsulta: this.movimientosFilter.idTarjetaConsulta,
                    correo: this.correoDestino.correo,
                    fechaInicio: this.movimientosFilter.fechaInicio,
                    fechaFin: this.movimientosFilter.fechaFin
                };

                this.movimientosFactory.enviarCorreo(model).then((response: angular.IHttpPromiseCallbackArg<any>) => {
                    this.correoDestino.tieneDatos = true;
                    this.correoDestino.enviado = true;
                    this.correoDestino.esValido = true;
                });
            } else {
                this.correoDestino.tieneDatos = true;
                this.correoDestino.enviado = false;
                this.correoDestino.esValido = false;
            }
        }

        validarCorreo(correo: string): boolean {
            var re = /^(([^<>()\[\]\.,;:\s@\"]+(\.[^<>()\[\]\.,;:\s@\"]+)*)|(\".+\"))@(([^<>()[\]\.,;:\s@\"]+\.)+[^<>()[\]\.,;:\s@\"]{2,})$/i;
            return re.test(correo);
        }

        obtenerUltimosMovimientos(idTarjeta: string, tieneAdicionales: boolean) {
            this.movimientosFilter.cantidadMovimientos = 5;
            var movimientosFilter = this.movimientosFilter;
            this.mensajesUltimosMovimientos = '';
            this.movimientosFactory.obtenerUltimosMovimientos({ tarjetaId: idTarjeta, cantidadMovimientos: movimientosFilter.cantidadMovimientos })
                .then((response: angular.IHttpPromiseCallbackArg<any>) => {
                    if (response.data) {
                        this.tieneUltimosMovimientosDisponible = true;
                        this.tieneAdicionales = tieneAdicionales;
                        this.ultimosmovimientosTarjeta = response.data.movimientos;
                    } else {
                        this.tieneUltimosMovimientosDisponible = false;
                        this.mensajesUltimosMovimientos = this.configuracion.mensajeMovimientosNoFacturados;
                    }

                }, () => {
                    this.tieneUltimosMovimientosDisponible = false;
                    this.mensajesUltimosMovimientos = this.configuracion.mensajeErrorWebApi;
                });
        }

        getImporte(importeSoles: number, importeDolares: number): string {
            var fractionSize = 2;
            return (importeSoles > 0 ? 'S/ ' + this.$filter('number')(importeSoles, fractionSize) : '') + ' ' + (importeDolares > 0 ? 'US$ ' + this.$filter('number')(importeDolares, fractionSize) : '');
        }

        getImporteFormateado(importe: number): string {
            var fractionSize = 2;
            return (importe > 0 ? this.$filter('number')(importe, fractionSize) : '');
        }

        activarMenu(menuItem: string): void {
            this.resetTimer();
            this.tabSeleccionado = menuItem;
            // if (!this.movimientosHistoricosCargado && menuItem === 'tab-2') {
            //     this.loadHistoricos();
            //     this.movimientosHistoricosCargado = true;
            // }
        }

        menuSeleccionado(menuItem: string): boolean {
            return this.tabSeleccionado === menuItem;
        }

        formatFechaPago(fechaVencimiento: string): string {
            return Common.Utility.obtenerFormatoFechaVencimiento(fechaVencimiento);
        }

        formatTextColor(fechaVencimiento: string) {
            return Common.Utility.obtenerColorFechaVencimiento(fechaVencimiento);
        }

        setearFiltroMovimientoRangoTiempo(opcion: number) {
            let fechaFinal = new Date();
            this.movimientosFilter.fechaFin = fechaFinal;
            this.movimientosFilter.fechaInicio = new Date();
            this.rangoFechaInicio = '';
            this.rangoFechaFin = '';
            switch (opcion) {
                case 0:
                    this.movimientosFilter.opcion = 'Última semana';
                    this.movimientosFilter.fechaInicio.setDate(fechaFinal.getDate() - 7);
                    break;
                case 1:
                    this.movimientosFilter.opcion = 'Último mes';
                    this.movimientosFilter.fechaInicio.setMonth(fechaFinal.getMonth() - 1);
                    break;
                case 2:
                    this.movimientosFilter.opcion = 'Último 3 meses';
                    this.movimientosFilter.fechaInicio.setMonth(fechaFinal.getMonth() - 3);
                    break;
                case 3:
                    this.movimientosFilter.opcion = 'Último 6 meses';
                    this.movimientosFilter.fechaInicio.setMonth(fechaFinal.getMonth() - 6);
                    break;
                case 4:
                    this.movimientosFilter.opcion = 'Último año';
                    this.movimientosFilter.fechaInicio.setMonth(fechaFinal.getMonth() - 12);
                    break;
            }
        }

        aplicarFiltroRangoFechas() {
            if (this.rangoFechaInicio && this.rangoFechaInicio !== '') {
                this.movimientosFilter.fechaInicio = this.stringToDate(this.rangoFechaInicio, 'dd/MM/yyyy', '/');
                this.movimientosFilter.opcion = 'Por rango de fechas';
            }

            if (this.rangoFechaFin && this.rangoFechaFin !== '') {
                this.movimientosFilter.fechaFin = this.stringToDate(this.rangoFechaFin, 'dd/MM/yyyy', '/');
            }
        }

        stringToDate(fecha: string, formato: string, separador: string): Date {
            var formatLowerCase = formato.toLowerCase();
            var formatItems = formatLowerCase.split(separador);
            var dateItems = fecha.split(separador);
            var monthIndex = formatItems.indexOf('mm');
            var dayIndex = formatItems.indexOf('dd');
            var yearIndex = formatItems.indexOf('yyyy');
            var day = parseInt(dateItems[dayIndex], 10);
            var month = parseInt(dateItems[monthIndex], 10) - 1;
            var year = parseInt(dateItems[yearIndex], 10);
            return new Date(year, month, day);
        }

        cargarSocios() {
            this.tarjetasApi.getAdicionales(this.movimientosFilter.tarjetaId)
                .then((response: angular.IHttpPromiseCallbackArg<Api.ITarjeta>) => {
                    this.listaSocios = [];
                    let tarjeta = new Tarjeta.DetalleTarjetaViewModel();
                    tarjeta.idTarjeta = response.data.idTarjeta;
                    tarjeta.socioTarjeta = response.data.socioTarjeta;
                    tarjeta.tieneAdicionales = response.data.tieneAdicionales;
                    tarjeta.adicionales = response.data.adicionales;
                    tarjeta.numeroTarjeta = response.data.numeroTarjeta;
                    let socioItem = {
                        idTarjeta: tarjeta.idTarjeta,
                        nombreSocio: tarjeta.socioTarjeta ? `${tarjeta.socioTarjeta.nombre} ${tarjeta.socioTarjeta.apellidoPaterno} ${tarjeta.socioTarjeta.apellidoMaterno}` : '',
                        numeroTarjeta: tarjeta.numeroTarjeta
                    };
                    this.listaSocios.push(socioItem);
                    let lista = tarjeta.adicionales;
                    if (lista) {
                        lista.forEach(element => {
                            let socioItem = {
                                idTarjeta: element.idTarjeta,
                                nombreSocio: element.socioTarjeta ? `${element.socioTarjeta.nombre} ${element.socioTarjeta.apellidoPaterno} ${element.socioTarjeta.apellidoMaterno}` : '',
                                numeroTarjeta: element.numeroTarjeta
                            };
                            this.listaSocios.push(socioItem);
                        });
                    }
                }, () => {
                    console.log('Lo sentimos la aplicación Diners Club Online se detuvo');
                });
        }

        formatearFecha(fecha: string): string {
            return Common.Utility.formatearFecha(fecha);
        }

        formatearNombre(nombre: string, segundoNombre: string, apillidoPaterno: string, apellidoMaterno: string): string {
            return Common.Utility.formatearNombre(nombre, segundoNombre, apillidoPaterno, apellidoMaterno);
        }

        formatearDescripcionMovimiento(descripcion: string): string {
            return Common.Utility.formatearDescripcionMovimiento(descripcion);
        }

        formatearLugarMovimiento(lugar: string): string {
            return Common.Utility.formatearLugarMovimiento(lugar);
        }

        obtenerFormatoFechaVencimiento(fechaVencimiento: string): string {
            return Common.Utility.obtenerFormatoFechaVencimiento(fechaVencimiento);
        }

        seleccionarAliasTarjeta(aliasTarjeta: string, idTarjeta: string) {
            this.nuevoAlias = aliasTarjeta;
            this.idTarjetaSeleccionada = idTarjeta;
        }

        cambiarAlias() {
            if (!this.nuevoAlias) {
                return false;
            }
            this.tarjetasApi.cambiarAlias(this.idTarjetaSeleccionada, this.nuevoAlias)
                .then((response: angular.IHttpPromiseCallbackArg<boolean>) => {
                    if (response) {
                        this.tarjetasTitular.forEach(item => {
                            if (item.idTarjeta === this.idTarjetaSeleccionada) {
                                item.alias = this.nuevoAlias;
                                return false;
                            }
                        });
                        console.log('nuevo alias actualizado');
                    }
                }, () => {
                    console.log('Lo sentimos la aplicación Diners Club Online se detuvo');
                });
        }

        mostrarUltimosCuatroDigitosNumeroTarjeta(numeroTarjeta: string): string {
            return Common.Utility.mostrarUltimosCuatroDigitosNumeroTarjeta(numeroTarjeta);
        }

        private filtrarTarjetasPorIdtarjeta(idTarjeta: string) {
            return function (elemento: any): boolean {
                return ((elemento.idTarjeta === idTarjeta) || (elemento.idTarjetaTitular === idTarjeta));
            };
        }
    }
}