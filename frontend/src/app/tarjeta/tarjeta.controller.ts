namespace Tarjeta {
    export class TarjetaController {
        mostrarOptionMotivo: boolean;
        listaTarjetas: Tarjeta.DetalleTarjetaViewModel[];
        opcionesTarjeta = [{ id: 'activo', description: 'Activa' }, { id: 'bloqueado', description: 'Bloquear' }];
        estadoActual: string;
        idTarjetaABloquear: string;
        test: string;
        resumenTarjeta = {
            IdTarjeta: '',
            Alias: '',
            NombreProducto: '',
            LineaDisponibleSoles: 0,
            LineaDisponibleDolares: 0,
            MillasDisponibles: 0,
            MontoTotalMesSoles: 0,
            MontoTotalMesDolares: 0,
            MontoMinimoMesSoles: 0,
            MontoMinimoMesDolares: 0,
            FechaVencimiento: new Date(),
            Pagado: false
        };
        motivoBloqueo = { motivo: 'perdida' };
        tabSeleccionado = 'tab-1';
        tarjetaABloquear: Tarjeta.DetalleTarjetaViewModel;
        content: Confirmacion.IContent;

        constructor(private tarjetasApi: Api.TarjetasApi, private authApi: any, private $location: angular.ILocationService, $rootScope: angular.IRootScopeService, private $scope: angular.IScope) {
            // super($rootScope);
            this.listaTarjetas = [];
            this.mostrarOptionMotivo = false;
            this.idTarjetaABloquear = '';
            this.$scope.$on('activarMenu', ((event, data) => { this.activarMenu(data); }));
        }

        monoproductoLocal(): void {
            // get data from api
            this.resumenTarjeta.IdTarjeta = 'asdfasdfasd';
            this.resumenTarjeta.Alias = 'asdfasdfasd';
            this.resumenTarjeta.NombreProducto = 'asdfasdfasd';
            this.resumenTarjeta.LineaDisponibleSoles = 11600;
            this.resumenTarjeta.LineaDisponibleDolares = 0;
            this.resumenTarjeta.MillasDisponibles = 15000;
            this.resumenTarjeta.MontoTotalMesSoles = 1562;
            this.resumenTarjeta.MontoTotalMesDolares = 0;
            this.resumenTarjeta.MontoMinimoMesSoles = 362.8;
            this.resumenTarjeta.MontoMinimoMesDolares = 0;
            this.resumenTarjeta.FechaVencimiento = new Date(2015, 10, 15);
            this.resumenTarjeta.Pagado = true;
        }

        obtenerTarjetas() {
            let tarjetasAdicionales: any[] = [];
            this.tarjetasApi.get()
                .then((response: angular.IHttpPromiseCallbackArg<Api.ITarjeta[]>) => {
                    let tarjetas: Api.ITarjeta[];
                    tarjetas = response.data;
                    tarjetas.forEach(element => {
                        let tarjeta = new Tarjeta.DetalleTarjetaViewModel();
                        tarjeta.idTarjeta = element.idTarjeta;
                        tarjeta.numeroTarjeta = element.numeroTarjeta;
                        tarjeta.nombreProducto = element.nombreProducto;
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
                        tarjeta.estadoTarjeta = element.estadoTarjeta;
                        tarjeta.opcionesTarjeta = this.opcionesTarjeta;
                        tarjeta.opcionActual = element.estadoTarjeta === 1 ? 'activo' : 'bloqueado';
                        tarjeta.numeroOperacion = element.numeroOperacion;
                        tarjeta.fechaOperacion = element.fechaOperacion;

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
                                tarjetaAdicional.estadoTarjeta = item.estadoTarjeta;
                                tarjetaAdicional.opcionesTarjeta = this.opcionesTarjeta;
                                tarjetaAdicional.opcionActual = item.estadoTarjeta === 1 ? 'activo' : 'bloqueado';
                                tarjetaAdicional.numeroOperacion = item.numeroOperacion;
                                tarjetaAdicional.fechaOperacion = item.fechaOperacion;

                                tarjetasAdicionales.push(tarjetaAdicional);
                            });
                        }
                    });

                    tarjetasAdicionales.forEach(tarjetaAdicional => {
                        this.listaTarjetas.push(tarjetaAdicional);
                    });

                }, () => {
                    console.log('Lo sentimos la aplicación Diners Club Online se detuvo');
                });
        }

        mostrarNumeroTarjetaParaBloqueo(numeroTarjeta: string): string {
            return Common.Utility.formatearNumeroTarjeta(numeroTarjeta);
        }

        evaluarSeleccion(idTarjeta: string) {
            let mostarOpcion: boolean = false;

            this.listaTarjetas.forEach(item => {
                if (item.idTarjeta !== idTarjeta && item.estadoTarjeta !== 0) {
                    item.opcionActual = 'activo';
                    this.mostrarOptionMotivo = false;
                }
                if (item.idTarjeta === idTarjeta && item.opcionActual === 'bloqueado') {
                    this.idTarjetaABloquear = idTarjeta;
                    mostarOpcion = true;
                }
            });

            if (mostarOpcion) {
                this.mostrarOptionMotivo = true;
            }
        }

        irAConfirmacion() {
            let idTarjeta = this.idTarjetaABloquear;
            let idMotivo = this.motivoBloqueo.motivo;

            this.listaTarjetas.forEach(item => {
                if (item.idTarjeta === idTarjeta) {
                    let tarjeta = new Tarjeta.DetalleTarjetaViewModel();
                    tarjeta.idTarjeta = item.idTarjeta;
                    tarjeta.nombreProducto = item.nombreProducto;
                    tarjeta.esTitular = item.esTitular;
                    tarjeta.socioTarjeta = item.socioTarjeta;
                    tarjeta.numeroTarjeta = item.numeroTarjeta;

                    this.tarjetaABloquear = tarjeta;
                }
            });
            this.activarMenu('tab-2');
        }

        activarMenu(menuItem: string): void {
            this.tabSeleccionado = menuItem;
        }

        menuSeleccionado(menuItem: string): boolean {
            return this.tabSeleccionado === menuItem;
        }

        bloquearTarjeta() {
            let tarjeta: any = {
                idMotivo: this.motivoBloqueo.motivo === 'Robo' ? 'robo' : 'perdida',
                nombreProducto: this.tarjetaABloquear.nombreProducto,
                numeroTarjeta: this.tarjetaABloquear.numeroTarjeta,
                formatoNombreTarjeta: this.mostrarAlias_o_NombreProducto(this.tarjetaABloquear.alias, this.tarjetaABloquear.nombreProducto) + ' ' + this.formatearNumeroTarjeta(this.tarjetaABloquear.numeroTarjeta),
                esTitular: this.tarjetaABloquear.esTitular,
                nombreTarjetaHabiente: this.tarjetaABloquear.socioTarjeta.nombre,
                apellidoPaternoTarjetaHabiente: this.tarjetaABloquear.socioTarjeta.apellidoPaterno
            };

            this.tarjetasApi.bloquearTarjeta(this.tarjetaABloquear.idTarjeta, tarjeta)
                .then((response: angular.IHttpPromiseCallbackArg<any>) => {
                    var contenido: Confirmacion.IContent = {
                        titulo: 'Bloqueo de tarjeta',
                        subTitulo: '',
                        subTituloMensaje: '',
                        botonTexto: '',
                        codigo: '',
                        siguienteTabNombre: 'tab-3',
                        volverTabNombre: 'tab-1',
                        endPoint: '',
                        forzarConfirmacion: true,
                        valores: [
                            { key: 'Fecha y hora', value: moment(response.data.fechaOperacion).format('DD/MM/YYYY HH:mm') + '' },
                            { key: 'N° Operación', value: response.data.numeroOperacion + '' },
                            { key: 'Tarjeta', value:  tarjeta.formatoNombreTarjeta},
                            { key: this.tarjetaABloquear.esTitular ? 'Titular' : 'Adicional', value: this.tarjetaABloquear.socioTarjeta.nombre + ' ' + this.tarjetaABloquear.socioTarjeta.apellidoPaterno },
                            { key: 'Motivo', value: (this.motivoBloqueo.motivo === 'robo' ? 'Robo' : 'Pérdida') }
                        ],
                        mostrarOfertas: false
                    };

                    this.tarjetaABloquear = new Tarjeta.DetalleTarjetaViewModel;
                    this.content = contenido;
                    this.activarMenu('tab-3');
                }, () => {
                    console.log('Lo sentimos la aplicación Diners Club Online se detuvo');
                });
        }

        mostrarAlias_o_NombreProducto(alias: string, nombreProducto: string): string {
            return Common.Utility.mostrarAlias_o_NombreProducto(alias, nombreProducto);
        }

        formatearNumeroTarjeta(numeroTarjeta: string): string {
            return Common.Utility.formatearNumeroTarjeta(numeroTarjeta);
        }

        formatearFecha(fecha: Date): string {
            let nuevoFormato = moment(fecha).format('DD/MM/YYYY');
            return nuevoFormato;
        }
    }
}