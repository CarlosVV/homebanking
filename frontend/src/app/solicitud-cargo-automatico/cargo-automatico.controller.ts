namespace CargoAutomatico {
    export class CargoAutomaticoController {
        categorias: Api.ICategoria[];
        empresas: Api.IEmpresa[];
        servicios: Api.IServicio[];
        tarjetas: Api.ITarjeta[];
        tarjetaActual: Api.ITarjeta;
        categoriaActual: Api.ICategoria;
        empresaActual: Api.IEmpresa;
        servicioActual: Api.IServicio;
        datoServicio: string;
        montoTope: number;
        idTipoOferta: string = '0001';
        tabSeleccionado = 'tab-1';
        content: Confirmacion.IContent;

        categoriaCorrecta: boolean = true;
        empresaCorrecta: boolean = true;
        servicioCorrecto: boolean = true;
        datoServicioCorrecto: boolean = true;
        montoCorrecto: boolean = true;

        constructor(private categoriasApi: Api.CategoriaApi, private empresasApi: Api.EmpresaApi, private serviciosApi: Api.ServicioApi, private tarjetasApi: Api.TarjetasApi, private sessionStorage: Common.SessionFactory, private $location: angular.ILocationService, private $filter: angular.IFilterService, private $scope: angular.IScope) {
            this.obtenerTarjetas();
            this.obtenerCategorias();

            this.$scope.$on('activarMenu', ((event, data) => { this.activarMenu(data); }));
        }

        init() {
            this.obtenerTarjetas();
            this.obtenerCategorias();
        }

        obtenerTarjetas() {
            this.tarjetasApi.get().then((result) => {
                this.tarjetas = result.data.map((x: Api.ITarjeta) => {
                    let tarjeta = new Tarjeta.DetalleTarjetaViewModel();
                    tarjeta.idTarjeta = x.idTarjeta;
                    tarjeta.nombreProducto = x.nombreProducto;
                    tarjeta.alias = x.alias;
                    if (!x.alias) {
                        tarjeta.alias = x.nombreProducto;
                    }

                    tarjeta.numeroTarjeta = `${tarjeta.alias} ${this.mostrarNumeroTarjetaConAsterisco(x.numeroTarjeta)}`;
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
            });
        }

        obtenerCategorias() {
            this.categoriasApi.obtenerCategorias().then((result) => {
                this.categorias = result.data;
            });
        }

        obtenerEmpresas(idCategoria: string) {
            if (!idCategoria) {
                this.empresas = null;
                this.empresaActual = null;
                this.obtenerServicios(null);
                return;
            }

            this.empresasApi.obtenerEmpresas(idCategoria).then((result) => {
                this.empresas = result.data;
            });
        }

        obtenerServicios(idEmpresa: string) {
            if (!idEmpresa) {
                this.servicios = null;
                this.servicioActual = null;
                return;
            }

            this.serviciosApi.obtenerServicios(idEmpresa).then((result) => {
                this.servicios = result.data;
            });
        }

        enviarSolicititud() {
            if (this.validateForm) {
                var contenido: Confirmacion.IContent = {
                    idModal: 'cargo01',
                    titulo: 'Solicitud de Cargo de Automático Diners',
                    subTitulo: '',
                    subTituloMensaje: '',
                    botonTexto: 'CONFIRMAR',
                    codigo: 'c02',
                    siguienteTabNombre: 'tab-3',
                    volverTabNombre: 'tab-1',
                    endPoint: '/solicitudes/cargo-automatico',
                    forzarConfirmacion: true,
                    valores: [
                        { key: 'Tarjeta', value: '<strong>' + this.tarjetaActual.numeroTarjeta },
                        { key: 'Categoría', value: this.categoriaActual.nombreCategoria },
                        { key: 'Empresa', value: this.empresaActual.nombreEmpresa },
                        { key: 'Servicio', value: this.servicioActual.nombreServicio },
                        { key: this.servicioActual.parametroServicio, value: this.datoServicio },
                        { key: 'Monto consumo tope', value: this.monedaFormato(this.montoTope) }
                    ],
                    mostrarOfertas: true
                };

                var data = {
                    idTipoOferta: this.idTipoOferta,
                    idTarjeta: this.tarjetaActual.idTarjeta,
                    tarjetaProducto: this.tarjetaActual.nombreProducto,
                    tarjetaVence: this.tarjetaActual.fechaVencimiento,
                    tarjetaNumero: this.tarjetaActual.numeroTarjeta,

                    idCategoria: this.categoriaActual.idCategoria,
                    categoriaNombre: this.categoriaActual.nombreCategoria,

                    idEmpresa: this.empresaActual.idEmpresa,
                    empresaNombre: this.empresaActual.nombreEmpresa,

                    idServicio: this.servicioActual.idServicio,
                    servicioNombre: this.servicioActual.nombreServicio,

                    datoServicio: this.datoServicio,
                    montoTope: this.montoTope,
                    datosCorreo: contenido.valores
                };

                this.content = contenido;
                this.sessionStorage.confirmacionData = data;
                this.activarMenu('tab-2');
            }
        }

        get validateForm() {
            if (this.empresaActual) {
                this.empresaCorrecta = true;
            } else {
                this.empresaCorrecta = false;
            }

            if (this.categoriaActual) {
                this.categoriaCorrecta = true;
            } else {
                this.categoriaCorrecta = false;
            }

            if (this.servicioActual) {
                this.servicioCorrecto = true;
            } else {
                this.servicioCorrecto = false;
            }

            if (this.datoServicio) {
                this.datoServicioCorrecto = true;
            } else {
                this.datoServicioCorrecto = false;
            }

            if (this.montoTope) {
                this.montoCorrecto = true;
            } else {
                this.montoCorrecto = false;
            }

            return (this.empresaCorrecta && this.categoriaCorrecta && this.servicioCorrecto && this.datoServicioCorrecto && this.montoCorrecto);
        }

        mostrarNumeroTarjetaConAsterisco(numeroTarjeta: string): string {
            let numeroTarjetaParcial: string = '';
            if (numeroTarjeta.length) {
                numeroTarjetaParcial = numeroTarjeta.substring(numeroTarjeta.length - 4, numeroTarjeta.length);
            }
            return `****-****** ${numeroTarjetaParcial}`;
        }

        monedaFormato(value: number): string {
            return this.$filter('currency')(value, 'S/ ', 2);
        }

        activarMenu(menuItem: string): void {
            this.tabSeleccionado = menuItem;
        }

        menuSeleccionado(menuItem: string): boolean {
            return this.tabSeleccionado === menuItem;
        }
    }
}