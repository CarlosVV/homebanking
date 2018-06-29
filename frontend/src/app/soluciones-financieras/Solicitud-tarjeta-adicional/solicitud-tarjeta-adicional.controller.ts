namespace TarjetaAdicional {
    export class TarjetaAdicionalViewModel {
        idTarjeta = '';
        tarjetasAdicionalesDetalle: TarjetaAdicionalDetalleViewModel[] = [];
    }

    export class TarjetaAdicionalDetalleViewModel {
        nombre: string = '';
        segundoNombre: string;
        apellidoPaterno: string;
        apellidoMaterno: string;
        tipoDocumento: string;
        numeroDocumento: string;
        numeroTelefono: string;
        fechaNacimiento: Date;
        correo: string;
        topeConsumoMensual: number;
        mostrarMensajeNumeroDocumento: boolean = false;
        mostrarMensajeNumeroTelefono: boolean = false;
        mostrarMensajeCorreo: boolean = false;
        mostrarMensajeTopeConsumoMensual: boolean = false;
        mostrarMensajeLimiteEdadAdicional: boolean = false;

        get nombreTarjeta() {
            let nombreEmboce: string;
            let nombreSplit: string = (!this.nombre ? '' : this.nombre.split(' ')[0]);
            let apeMaternoPrimeraLetra = (!this.apellidoMaterno ? '' : this.apellidoMaterno.substring(0, 1));
            let apePaternoVacio: string = (!this.apellidoPaterno ? '' : this.apellidoPaterno);
            nombreEmboce = nombreSplit + ' ' + apePaternoVacio + ' ' + apeMaternoPrimeraLetra;
            nombreEmboce = nombreEmboce.trim();
            return nombreEmboce;
        }
    }

    export class TarjetaAdicionalController {
        tarjetaAdicional = new TarjetaAdicionalViewModel();
        listaTarjetasTitulares: Tarjeta.DetalleTarjetaViewModel[] = [];
        listaTarjetasAdicionalesExistentes: Tarjeta.DetalleTarjetaViewModel[] = [];
        lineaCredito: number = 0;
        content: Confirmacion.IContent;
        tabSeleccionado = 'tab-1';
        limiteAdicionales: number = 5;
        cantidadDisponibleAdicionales: number;
        tarjetaAplicante: any = {};
        listaTiposDocumentos: Common.TipoDocumento[];
        topeCosumoMinimo: number = 300;
        edadLimiteAdicional: number = 76;

        constructor($rootScope: angular.IRootScopeService, private tarjetasApi: Api.TarjetasApi, private sessionFactory: Common.SessionFactory,
            private $scope: ng.IScope, private $filter: angular.IFilterService, private tipoDocumentoFactory: Common.TipoDocumentoFactory) {
            // super($rootScope);
            this.$scope.$on('activarMenu', ((event, data) => { this.activarMenu(data); }));
        }

        inicializar(): void {
            this.obtenerTiposDocumentos();
            this.obtenerTarjetas();
        }

        obtenerTarjetas(): void {
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
                        tarjeta.cantidadAdicionales = element.adicionales.length;
                        this.listaTarjetasTitulares.push(tarjeta);

                        // tarjetas adicionales
                        if (element.adicionales.length > 0) {
                            element.adicionales.forEach(item => {
                                let tarjetaAdicional = new Tarjeta.DetalleTarjetaViewModel();
                                tarjetaAdicional.idTarjeta = element.idTarjeta;
                                tarjetaAdicional.numeroTarjeta = element.numeroTarjeta;
                                tarjetaAdicional.nombreProducto = element.nombreProducto;
                                tarjetaAdicional.alias = element.alias;
                                tarjetaAdicional.lineaCreditoSoles = element.lineaCreditoSoles;
                                tarjetaAdicional.lineaCreditoDolares = element.lineaCreditoDolares;

                                this.listaTarjetasAdicionalesExistentes.push(tarjetaAdicional);
                            });
                        }
                    });

                    this.cargarPrimeraTarjetaAdicionalVacia();
                    this.obtenerDatosTarjeta(this.tarjetaAdicional.idTarjeta);
                }, () => {
                    console.log('Lo sentimos la aplicación Diners Club Online se detuvo');
                });
        }

        obtenerTiposDocumentos() {
            this.tipoDocumentoFactory.get()
                .then((response: angular.IHttpPromiseCallbackArg<Common.TipoDocumento[]>) => {
                    if (response.status === 200) {
                        if (response.data) {
                            this.listaTiposDocumentos = response.data.map((item: Common.TipoDocumento) => {
                                let tipoDocumento = new Common.TipoDocumento();
                                tipoDocumento.idTipoDocumento = item.idTipoDocumento;
                                tipoDocumento.nombreTipoDocumento = item.nombreTipoDocumento;
                                return tipoDocumento;
                            });
                        }
                    } else {
                        console.log('Lo sentimos la aplicación Diners Club Online se detuvo');
                    }
                }, () => {
                    console.log('Lo sentimos la aplicación Diners Club Online se detuvo');
                });
        }

        cargarPrimeraTarjetaAdicionalVacia(): void {
            this.instanciarTarjetaAdicional();
            this.tarjetaAdicional.idTarjeta = this.listaTarjetasTitulares[0].idTarjeta;
        }

        instanciarTarjetaAdicional() {
            let tarjetAdicionalDetalle = new TarjetaAdicionalDetalleViewModel();
            tarjetAdicionalDetalle.tipoDocumento = '-1';
            this.tarjetaAdicional.tarjetasAdicionalesDetalle.push(tarjetAdicionalDetalle);
        }

        agregarAdicional(): void {
            let cantidadAdicionalesEnLista: number;
            cantidadAdicionalesEnLista = this.tarjetaAdicional.tarjetasAdicionalesDetalle.length;

            if (cantidadAdicionalesEnLista < this.cantidadDisponibleAdicionales) {
                this.instanciarTarjetaAdicional();
            }
        }

        eliminarAdicional(index: number): void {
            if (this.tarjetaAdicional.tarjetasAdicionalesDetalle.length > 1) {
                delete this.tarjetaAdicional.tarjetasAdicionalesDetalle[index];
                var tempList: any[] = [];
                this.tarjetaAdicional.tarjetasAdicionalesDetalle.forEach(item => {
                    if (item !== undefined) {
                        tempList.push(item);
                    }
                });

                this.tarjetaAdicional.tarjetasAdicionalesDetalle = [];
                tempList.forEach(item => {
                    let adicional = new TarjetaAdicionalDetalleViewModel();
                    adicional.nombre = item.nombre;
                    adicional.segundoNombre = item.segundoNombre;
                    adicional.apellidoPaterno = item.apellidoPaterno;
                    adicional.apellidoMaterno = item.apellidoMaterno;
                    adicional.tipoDocumento = item.tipoDocumento;
                    adicional.numeroDocumento = item.numeroDocumento;
                    adicional.numeroTelefono = item.numeroTelefono;
                    adicional.fechaNacimiento = item.fechaNacimiento;
                    adicional.correo = item.correo;
                    adicional.topeConsumoMensual = item.topeConsumoMensual;

                    this.tarjetaAdicional.tarjetasAdicionalesDetalle.push(adicional);
                });
            }
        }

        formatearNumeroTarjeta(numeroTarjeta: string): string {
            return Common.Utility.formatearNumeroTarjeta(numeroTarjeta);
        }

        mostrarAlias_o_NombreProducto(alias: string, nombreProducto: string): string {
            return Common.Utility.mostrarAlias_o_NombreProducto(alias, nombreProducto);
        }

        obtenerDatosTarjeta(idTarjeta: string): void {
            this.listaTarjetasTitulares.forEach(item => {
                if (item.idTarjeta === idTarjeta) {
                    this.tarjetaAplicante = item;
                    this.cantidadDisponibleAdicionales = (this.limiteAdicionales - item.cantidadAdicionales);
                    if (this.tarjetaAdicional.tarjetasAdicionalesDetalle.length > 1) {
                        this.cargarTarjetaAdicionalVacia();
                    }
                    return;
                }
            });
        }

        cargarTarjetaAdicionalVacia(): void {
            this.tarjetaAdicional.tarjetasAdicionalesDetalle = [];
            this.instanciarTarjetaAdicional();
        }

        irAConfirmacion(): void {
            let contador: number = 1;
            let valores: any[] = [];
            valores.push({ key: 'Tarjeta', value: this.mostrarAlias_o_NombreProducto(this.tarjetaAplicante.alias, this.tarjetaAplicante.nombreProducto) + ' ' + this.formatearNumeroTarjeta(this.tarjetaAplicante.numeroTarjeta) });

            this.tarjetaAdicional.tarjetasAdicionalesDetalle.forEach(item => {
                valores.push({ key: '<strong>Datos del adicional ' + (contador === 1 ? '' : contador) + '</strong>', value: '' });
                valores.push({ key: 'Nombre', value: item.nombre + ' ' + item.apellidoPaterno + ' ' + item.apellidoMaterno });
                valores.push({ key: 'Tipo de documento', value: (item.tipoDocumento === '1' ? 'DNI' : item.tipoDocumento === '2' ? 'PASAPORTE' : 'CARNET EXTRANJERIA') });
                valores.push({ key: 'Número de documento', value: item.numeroDocumento });
                valores.push({ key: 'Fecha de nacimiento', value: item.fechaNacimiento });
                valores.push({ key: 'Correo electrónico', value: item.correo });
                valores.push({ key: '', value: '' });
                valores.push({ key: 'Nombre de la tarjeta', value: item.nombreTarjeta });
                valores.push({ key: 'Tope de consumo mensual', value: this.monedaFormato(item.topeConsumoMensual === undefined ? this.tarjetaAplicante.lineaCreditoSoles : item.topeConsumoMensual) });
                valores.push({ key: '', value: '' });
                contador += 1;
            });

            var contenido: Confirmacion.IContent = {
                titulo: 'Solicitud de Tarjeta Adicional',
                subTitulo: '',
                subTituloMensaje: '',
                botonTexto: 'CONFIRMAR',
                codigo: 'c02',
                volverTabNombre: 'tab-1',
                siguienteTabNombre: 'tab-3',
                endPoint: '/solicitudes/tarjeta-adicional',
                forzarConfirmacion: true,
                valores: valores,
                mostrarOfertas: true,
                idModal: 'tarjeta-adicional-01'
            };

            let data: Api.ITarjetaAdicionalViewModel = {
                idTipoOferta: '',
                idTarjeta: this.tarjetaAdicional.idTarjeta,
                datosCorreo: contenido.valores,
                tarjetasAdicionalesDetalle: this.tarjetaAdicional.tarjetasAdicionalesDetalle.map((item: Api.ITarjetaAdicionalDetalleViewModel) => {
                    let adicional: Api.ITarjetaAdicionalDetalleViewModel = {
                        nombre: item.nombre,
                        segundoNombre: item.segundoNombre,
                        apellidoPaterno: item.apellidoPaterno,
                        apellidoMaterno: item.apellidoMaterno,
                        tipoDocumento: item.tipoDocumento,
                        numeroDocumento: item.numeroDocumento,
                        numeroTelefono: item.numeroTelefono,
                        fechaNacimiento: moment(item.fechaNacimiento.toString(), 'DD/MM/YYYY').toDate(),
                        correo: item.correo,
                        nombreTarjeta: item.nombreTarjeta,
                        topeConsumoMensual: (item.topeConsumoMensual === undefined ? this.tarjetaAplicante.lineaCreditoSoles : item.topeConsumoMensual)
                    };
                    return adicional;
                }),
                nombreProducto: this.tarjetaAplicante.nombreProducto,
                numeroTarjeta: this.tarjetaAplicante.numeroTarjeta,
                lineaCreditoSoles: this.tarjetaAplicante.lineaCreditoSoles
            };

            this.content = contenido;
            this.sessionFactory.confirmacion = contenido;
            this.sessionFactory.confirmacionData = data;
            this.activarMenu('tab-2');
        }

        monedaFormato(value: number): string {
            return this.$filter('currency')(value, 'S/ ', 2);
        }

        validarTopeConsumoMensual(index: number) {
            if (this.tarjetaAdicional.tarjetasAdicionalesDetalle[index].topeConsumoMensual) {
                var topeConsumoMenual: number = this.tarjetaAdicional.tarjetasAdicionalesDetalle[index].topeConsumoMensual;
                if (topeConsumoMenual < this.topeCosumoMinimo) {
                    this.tarjetaAdicional.tarjetasAdicionalesDetalle[index].topeConsumoMensual = this.topeCosumoMinimo;
                    this.tarjetaAdicional.tarjetasAdicionalesDetalle[index].mostrarMensajeTopeConsumoMensual = true;
                }else {
                    this.tarjetaAdicional.tarjetasAdicionalesDetalle[index].mostrarMensajeTopeConsumoMensual = false;
                }
            }
        }

        validarNumeroDocumento(index: number) {
            let numeroDocumento: string = this.tarjetaAdicional.tarjetasAdicionalesDetalle[index].numeroDocumento;
            let errorEncontrado: boolean = false;
            if (numeroDocumento) {

                // validar número de documento en la solicitud
                if (this.tarjetaAdicional.tarjetasAdicionalesDetalle.length > 1) {
                    let cantidadRepetidos: number = 0;
                    this.tarjetaAdicional.tarjetasAdicionalesDetalle.forEach(item => {
                        if (item.numeroDocumento === numeroDocumento) {
                            cantidadRepetidos = cantidadRepetidos + 1;
                        }

                        if (cantidadRepetidos > 1) {
                            this.tarjetaAdicional.tarjetasAdicionalesDetalle[index].numeroDocumento = '';
                            this.tarjetaAdicional.tarjetasAdicionalesDetalle[index].mostrarMensajeNumeroDocumento = true;
                            errorEncontrado = true;
                        }else {
                            this.tarjetaAdicional.tarjetasAdicionalesDetalle[index].mostrarMensajeNumeroDocumento = false;
                        }
                    });
                }

                if (errorEncontrado) { return; }
            }
        }

        validarNumeroCelular(index: number) {
            let numeroTelefono: string = this.tarjetaAdicional.tarjetasAdicionalesDetalle[index].numeroTelefono;
            let errorEncontrado: boolean = false;
            if (numeroTelefono) {

                // validar número de documento en la solicitud
                if (this.tarjetaAdicional.tarjetasAdicionalesDetalle.length > 1) {
                    let cantidadRepetidos: number = 0;
                    this.tarjetaAdicional.tarjetasAdicionalesDetalle.forEach(item => {
                        if (item.numeroTelefono === numeroTelefono) {
                            cantidadRepetidos = cantidadRepetidos + 1;
                        }

                        if (cantidadRepetidos > 1) {
                            this.tarjetaAdicional.tarjetasAdicionalesDetalle[index].numeroTelefono = '';
                            this.tarjetaAdicional.tarjetasAdicionalesDetalle[index].mostrarMensajeNumeroTelefono = true;
                            errorEncontrado = true;
                        }else {
                            this.tarjetaAdicional.tarjetasAdicionalesDetalle[index].mostrarMensajeNumeroTelefono = false;
                        }
                    });
                }

                if (errorEncontrado) { return; }
            }
        }

        validarCorreo(index: number) {
            let correo: string = this.tarjetaAdicional.tarjetasAdicionalesDetalle[index].correo;
            let errorEncontrado: boolean = false;
            if (correo) {

                // validar número de documento en la solicitud
                if (this.tarjetaAdicional.tarjetasAdicionalesDetalle.length > 1) {
                    let cantidadRepetidos: number = 0;
                    this.tarjetaAdicional.tarjetasAdicionalesDetalle.forEach(item => {
                        if (item.correo === correo) {
                            cantidadRepetidos = cantidadRepetidos + 1;
                        }

                        if (cantidadRepetidos > 1) {
                            this.tarjetaAdicional.tarjetasAdicionalesDetalle[index].correo = '';
                            this.tarjetaAdicional.tarjetasAdicionalesDetalle[index].mostrarMensajeCorreo = true;
                            errorEncontrado = true;
                        }else {
                            this.tarjetaAdicional.tarjetasAdicionalesDetalle[index].mostrarMensajeCorreo = false;
                        }
                    });
                }

                if (errorEncontrado) { return; }
            }
        }

        validarEdadAdicional(index: number) {
            var regex = /^[0-9]{2}[\/][0-9]{2}[\/][0-9]{4}$/g;
            let fechaNacimiento: string = this.tarjetaAdicional.tarjetasAdicionalesDetalle[index].fechaNacimiento.toString();
            if (regex.test(fechaNacimiento)) {
                let formmatoFecha = fechaNacimiento.split('/');
                let anhoFN = new Date(parseInt(formmatoFecha[2], 10), parseInt(formmatoFecha[1], 10), parseInt(formmatoFecha[0], 10)).getFullYear();
                var anhoActual = new Date().getFullYear();
                if ((anhoActual - anhoFN) > this.edadLimiteAdicional) {
                    this.tarjetaAdicional.tarjetasAdicionalesDetalle[index].fechaNacimiento = null;
                    this.tarjetaAdicional.tarjetasAdicionalesDetalle[index].mostrarMensajeLimiteEdadAdicional = true;
                } else {
                    this.tarjetaAdicional.tarjetasAdicionalesDetalle[index].mostrarMensajeLimiteEdadAdicional = false;
                }
            }
        }

        activarMenu(menuItem: string): void {
            this.tabSeleccionado = menuItem;
        }

        menuSeleccionado(menuItem: string): boolean {
            return this.tabSeleccionado === menuItem;
        }
    }
}