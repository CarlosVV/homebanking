namespace PosicionConsolidada {
    export class PosicionConsolidadaController extends Common.BaseController {
        campanas: Api.ICampana[];
        tarjetas: Tarjeta.DetalleTarjetaViewModel[] = [];
        tarjetasConAdicionales: Api.ITarjeta[] = [];
        tarjetasTitular: Tarjeta.DetalleTarjetaViewModel[] = [];
        saludo: string;
        iconoSaludo: string;
        bannerByCountry: string;
        isLocal: boolean = true;
        nombreUsuario: string = '';
        apellidoPaternoUsuario: string = '';
        aliasUsuario: string;
        nuevoAlias: string;
        datosCiudad: any = {};
        usuario: Api.IUsuarioPatchActualizarImagenPerfil = {
            idFacebook: '',
            idImagen: '0'
        };
        imagenSeguridad: string;

        get tarjeta(): Tarjeta.DetalleTarjetaViewModel {
            return this.tarjetas[0];
        }

        get esMonoproducto(): boolean {
            return this.tarjetas.length === 1;
        }

        get nombreApellidoUsuario(): string {
            return `${this.nombreUsuario} ${this.apellidoPaternoUsuario}`;
        }

        constructor(private campanasApi: Api.CampanasApi,
            private usuarioApi: Api.UsuarioApi,
            private tarjetasApi: Api.TarjetasApi,
            private sessionFactory: Common.SessionFactory,
            private geoApi: Geolocalizacion.GeoAPi,
            private $location: angular.ILocationService,
            $rootScope: angular.IRootScopeService,
            private $filter: angular.IFilterService,
            private tarjetaService: Tarjeta.TarjetaService,
            private weatherApi: Api.WeatherApi,
            private timeZoneApi: Api.TimeZoneApi,
            private facebookPictureFactory: Usuario.FacebookPictureFactory,
            private $sce: ng.ISCEService
        ) {
            super($rootScope);
        }

        inicioConsolidada() {
            this.greetings();
            this.getPosicionConsolidada();
            this.getInfoUsuario();
            this.showBienvenidaModal();
			this.obtenerImagen();
            this.getCampanas();
        }

        greetings() {
            this.geoApi.isLocal().then((response) => {
                this.isLocal = response.isLocal;
                this.saludo = Common.Utility.processGreetingsLangByCountry(response.isLocal, response.countryCode.toLowerCase());
                this.iconoSaludo = Common.Utility.processIconByCountry(response.isLocal, response.countryCode.toLowerCase());
                this.bannerByCountry = Common.Utility.processBannerByCountry(response.isLocal);
                this.obtenerDatosCiudad();
            });
        }

        obtenerDatosCiudad(): void {
            let coordenadas: any;
            this.geoApi.getCurrentPosition().then((response) => {
                coordenadas = response.coords;

                this.weatherApi.get(coordenadas.latitude, coordenadas.longitude).then((response) => {
                    this.datosCiudad.ciudad = response.data.name;
                    this.datosCiudad.temperatura = parseInt(response.data.main.temp, 10);

                    this.timeZoneApi.get(coordenadas.latitude, coordenadas.longitude).then((response) => {
                        this.datosCiudad.hora = this.formatear12Horas(response.data.formatted);

                    }, (error) => {
                        console.log(error);
                    });
                }, (error) => {
                    console.log(error);
                });
            }, (error) => {
                console.log(error);
            });
        }

        getPosicionConsolidada() {
            this.tarjetasApi.get()
                .then((response: angular.IHttpPromiseCallbackArg<Api.ITarjeta[]>) => {
                    this.tarjetasConAdicionales = response.data;
                    this.tarjetas = response.data.map((x: Api.ITarjeta) => {
                        let tarjeta = new Tarjeta.DetalleTarjetaViewModel();
                        tarjeta.idTarjeta = x.idTarjeta;
                        tarjeta.nombreProducto = x.nombreProducto;
                        tarjeta.alias = (x.alias && x.alias.length > 0) ? x.alias : x.nombreProducto;
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

                    this.tarjetasTitular = this.tarjetas;
                }, () => {
                    console.log('Lo sentimos la aplicación Diners Club Online se detuvo');
                });
        }

        obtenerFormatoFechaVencimiento(fecha: string) {
            return Common.Utility.obtenerFormatoFechaVencimiento(fecha);
        }

        obtenerColorFechaVencimiento(fecha: string) {
            return Common.Utility.obtenerColorFechaVencimiento(fecha);
        }

        getInfoUsuario() {
            this.usuarioApi.get().then((response) => {
                this.nombreUsuario = response.data.nombre;
                this.apellidoPaternoUsuario = response.data.apellidoPaterno;
                this.sessionFactory.nombreUsuario = response.data.nombre;
                this.aliasUsuario = (response.data.alias && response.data.alias.length > 0) ? response.data.alias : this.nombreApellidoUsuario;
                let imagenSeguridad = this.sessionFactory.imagenSeguridad;

                if (!imagenSeguridad.tieneImagen) {
                    $('#ElegirNuevaImagenSeguridadModal').modal({
                        backdrop: 'static',   // this disable for click outside event
                        keyboard: false // this for keyboard event 
                    });
                }
            }, (error) => {
                console.log(error);
            });
        }

        showBienvenidaModal() {
            if (this.sessionFactory.isPrimerIngreso()) {
                $('#modal-prehome').modal();
            }
        }

        seleccionarAliasUsuario(aliasUsuario: string) {
            this.nuevoAlias = aliasUsuario;
        }

        cambiarAliasUsuario() {
            if (!this.nuevoAlias) {
                return false;
            }
            this.usuarioApi.actualizarAliasUsuario(this.nuevoAlias)
                .then((response: angular.IHttpPromiseCallbackArg<boolean>) => {
                    if (response) {
                        this.aliasUsuario = this.nuevoAlias;
                        console.log('nuevo alias de usuario actualizado');
                    }
            }, () => {
                console.log('Lo sentimos la aplicación Diners Club Online se detuvo');
            });
        }

        actualizarImagenPerfil() {
            if (this.usuario.idFacebook === '' && this.usuario.idImagen === '0') {
                return;
            }
            this.usuario.idFacebook = '';
            this.usuarioApi.patchActualizarImagenPerfil(this.usuario).then((response: angular.IHttpPromiseCallbackArg<boolean>) => {
                if (response) {
                    // let data = response;
                    let imagenSeguridad: any = {
                        tieneImagen: true,
                        urlImagen: Common.Utility.generarUrlImagenSeguridad(this.usuario.idImagen)
                    };
                    this.sessionFactory.imagenSeguridad = imagenSeguridad;

                    $('#ElegirNuevaImagenSeguridadModal').modal('toggle');
                    location.reload();
                }
            }, () => {
                console.log('Lo sentimos la aplicación Diners Club Online se detuvo');
            });
        }

        getCampanas() {
            this.campanasApi.getCampanas().then((result) => {
                this.campanas = result.data;

                this.campanas.forEach(x => {
                    x.banner = this.$sce.trustAsHtml(x.banner);
                });
            });
        }

		obtenerImagen() {
            let imagen = this.sessionFactory.imagenSeguridad.urlImagen;
            this.imagenSeguridad = imagen;
        }

        private formatear12Horas(date: string): string {
            let currentDate: Date = new Date(date);

            let hour = currentDate.getHours();
            let minute = currentDate.getMinutes();
            let amPM = (hour > 11) ? 'pm' : 'am';

            if (hour > 12) {
                hour -= 12;
            } else if (hour === 0) {
                hour = 12;
            }

            return hour + ':' + (minute < 10 ? '0' + minute : minute) + ' ' + amPM;
        }
    }
}
