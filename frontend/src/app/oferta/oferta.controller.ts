namespace Oferta {
    interface IRouteParams extends angular.route.IRouteParamsService {
        idTipoOferta: string;
    }
    export class OfertaController {
        ofertas: OfertaModel[] = [];
        ofertaDetalle: OfertaModel;
        ofertasPrincipales: OfertaModel[] = [];
        bannerByCountry: string;
        idTipoOferta: string;
        listaTarjetas: Api.ITarjeta[] = [];
        tarjeta: Api.ITarjeta;

        constructor(private tarjetasApi: Api.TarjetasApi, private ofertaApi: Api.OfertaApi, private sessionFactory: Common.SessionFactory, private configuracion: any, private $sce: ng.ISCEService,
            private $location: angular.ILocationService, private $routeParams: IRouteParams) {
            this.idTipoOferta = this.$routeParams.idTipoOferta;
        }

        inicializar() {
            if (this.sessionFactory.ofertas != null && !(this.sessionFactory.ofertas.length > 0)) {
                this.obtenerOfertas();
            } else {
                this.ofertas = this.sessionFactory.ofertas;
                this.obtenerOfertaDetalle(this.idTipoOferta);
            }
            this.obtenerTarjetas();
            this.processBannerByCountry();
        }

        inicioOfertas() {
            this.processBannerByCountry();
            this.obtenerOfertas();
        }

        obtenerOfertas() {
            this.ofertaApi.obtenerOfertas()
                .then((response: angular.IHttpPromiseCallbackArg<Api.IOfertaModel[]>) => {
                    if (response.data) {
                        let contador: number = 0;
                        this.ofertas = response.data.map((x: Api.IOfertaModel) => {
                            let oferta = new OfertaModel();
                            oferta.idTarjeta = x.idTarjeta;
                            oferta.idTipoOferta = x.idTipoOferta;
                            oferta.fechaInicio = x.fechaInicio;
                            oferta.fechaFin = x.fechaFin;
                            oferta.montoLineaNueva = x.montoLineaNueva;
                            oferta.montoOferta = x.montoOferta;
                            oferta.tcea = x.tcea;
                            oferta.tea = x.tea;
                            oferta.TextoOferta = new OfertaTextos();
                            let textoSeleccionado: OfertaTextos[] = OfertaListaTextos.textosPorTipoOferta.filter(this.obtenerTextos(x.idTipoOferta));
                            if (textoSeleccionado && textoSeleccionado.length > 0) {
                                oferta.TextoOferta.titulo = textoSeleccionado[0].titulo;
                                oferta.TextoOferta.botonTexto = textoSeleccionado[0].botonTexto;
                                oferta.TextoOferta.rutaSolicitud = textoSeleccionado[0].rutaSolicitud;
                                oferta.TextoOferta.descripcion = textoSeleccionado[0].descripcion;
                                oferta.TextoOferta.TextoBotonInformacion = textoSeleccionado[0].TextoBotonInformacion;
                            }
                            if (contador < 2) {
                                this.ofertasPrincipales.push(oferta);
                            }
                            contador += 1;
                            return oferta;
                        });

                        if (this.ofertas.length > 0) {
                            this.sessionFactory.ofertas = this.ofertas;
                        }
                    } else {
                        console.log('No existen ofertas para el usuario');
                    }
                }, () => {
                    console.log('Lo sentimos la aplicación Diners Club Online se detuvo');
                });
        }

        obtenerOfertaDetalle(idTipoOferta: string) {
            this.ofertas.forEach(x => {
                if (x.idTipoOferta === idTipoOferta) {
                    let ofertaDetalle = new OfertaModel();
                    ofertaDetalle.idTarjeta = x.idTarjeta;
                    ofertaDetalle.idTipoOferta = x.idTipoOferta;
                    ofertaDetalle.fechaInicio = x.fechaInicio;
                    ofertaDetalle.fechaFin = x.fechaFin;
                    ofertaDetalle.montoLineaNueva = x.montoLineaNueva;
                    ofertaDetalle.montoOferta = x.idTipoOferta === '1' ? x.montoLineaNueva : x.montoOferta;
                    ofertaDetalle.tcea = x.tcea;
                    ofertaDetalle.tea = x.tea;
                    ofertaDetalle.TextoOfertaDetalle = new OfertaDetalleTextos();
                    let textoSeleccionado: OfertaDetalleTextos[] = OfertaDetalleListaTextos.textosPorTipoOfertaDetalle.filter(this.obtenerTextos(x.idTipoOferta));
                    if (textoSeleccionado && textoSeleccionado.length > 0) {
                        ofertaDetalle.TextoOfertaDetalle.oferta = textoSeleccionado[0].oferta;
                        ofertaDetalle.TextoOfertaDetalle.titulo = textoSeleccionado[0].titulo;
                        ofertaDetalle.TextoOfertaDetalle.descripcion = textoSeleccionado[0].descripcion;
                        ofertaDetalle.TextoOfertaDetalle.descripcion2 = textoSeleccionado[0].descripcion2;
                        ofertaDetalle.TextoOfertaDetalle.botonTexto = textoSeleccionado[0].botonTexto;
                        ofertaDetalle.TextoOfertaDetalle.rutaSolicitud = textoSeleccionado[0].rutaSolicitud;
                        ofertaDetalle.TextoOfertaDetalle.detalles = textoSeleccionado[0].detalles;
                    }

                    this.ofertaDetalle = ofertaDetalle;
                }
            });
        }

        processBannerByCountry() {
            var isLocal = this.sessionFactory.geolocation.isLocal;
            this.bannerByCountry = Common.Utility.processBannerByCountry(isLocal);
        }

        iraSolicitud(idTipoOferta: string) {
            this.$location.path('/ofertas/detalle/' + idTipoOferta);
        }

        mostrarNombreSocio() {
            return this.sessionFactory.nombreUsuario;
        }

        obtenerInformacionTarjeta() {
            this.listaTarjetas.forEach(_tarjeta => {
                if (_tarjeta.idTarjeta === this.ofertaDetalle.idTarjeta) {
                    this.tarjeta = _tarjeta;
                }
            });
        }

        obtenerTarjetas() {
            this.tarjetasApi.get()
                .then((response: angular.IHttpPromiseCallbackArg<Api.ITarjeta[]>) => {
                    let tarjetas: Api.ITarjeta[];
                    tarjetas = response.data;
                    tarjetas.forEach(_tarjeta => {
                        _tarjeta.srcImagenTarjeta = `Content/images/${_tarjeta.nombreProducto}.png`;
                        this.listaTarjetas.push(_tarjeta);
                        if (_tarjeta.adicionales.length > 0) {
                            _tarjeta.adicionales.forEach(_tarjetaAdicional => {
                                _tarjetaAdicional.srcImagenTarjeta = `Content/images/${_tarjetaAdicional.nombreProducto}.png`;
                                this.listaTarjetas.push(_tarjetaAdicional);
                            });
                        }
                    });
                    this.obtenerInformacionTarjeta();
                }, () => {
                    console.log('Lo sentimos la aplicación Diners Club Online se detuvo');
                });
        }

        procesarSolicitud(urlPath: string) {
            let fullPath: string = urlPath + '/' + this.ofertaDetalle.idTipoOferta;
            this.$location.path(fullPath);
        }

        iraSolicitudDesdeConstancia(rutaSolicitud: string, idTipoOferta: string) {
            this.$location.path(rutaSolicitud + idTipoOferta);
        }

        private obtenerTextos(tipoOferta: string) {
            return function (elemento: any): boolean {
                return (elemento.idTipoOferta === tipoOferta);
            };
        }
    }
}