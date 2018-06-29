namespace ServiciosSocio {
    export class ReclamoIngresarController {
        tabSeleccionado = 'tab-1';
        descripcionSolicitudMaxLength: number = 500;
        solocionEsperadaMaxLength: number = 500;
        content: Confirmacion.IContent;
        emailDefault: string;

        tarjetas: Api.ITarjeta[];
        tarjetaActual: Api.ITarjeta;

        medioRespuesta: any;
        motivo: any;

        direccionEnvio: string;
        descripcionSolicitud: string;
        solucionEsperada: string;

        motivos: any[] = [{
            'id': 'tipo1',
            'value': 'Reclamo'
        },
        {
            'id': 'tipo2',
            'value': 'Queja'
        }];

        mediosRespuesta: any[] = [
            {
                'id': 'medio1',
                'value': 'Correo Electronico',
                'type': 'email',
                'placeholder': 'ejemplo@mail.com'
            },
            {
                'id': 'medio2',
                'value': 'Carta',
                'type': 'text',
                'placeholder': 'Ingrese una direccion'
            }];

        constructor(
            private usuarioApi: Api.UsuarioApi,
            private tarjetasApi: Api.TarjetasApi,
            private sessionStorage: Common.SessionFactory,
            private $scope: angular.IScope
        ) {
            this.obtenerTarjetas();
            this.obtenerUsuarioDatos();
            this.$scope.$on('activarMenu', (event, data) => { this.activarMenu(data); });
            this.motivo = this.motivos[0];
            this.medioRespuesta = this.mediosRespuesta[0];
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

                    tarjeta.numeroTarjeta = `${tarjeta.alias} ${Common.Utility.mostrarNumeroTarjetaConAsterisco(x.numeroTarjeta)}`;
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

        obtenerUsuarioDatos() {
            this.usuarioApi.get().then((result) => {
                this.direccionEnvio = result.data.emailPrincipal;
            });
        }

        enviarReclamo() {
            var contenido: Confirmacion.IContent = {
                idModal: 'reclamo01',
                titulo: 'Ingreso de Reclamos y atención al Cliente',
                subTitulo: '',
                subTituloMensaje: '',
                subTituloInformacion: 'Datos del Reclamo',
                botonTexto: 'ENVIAR SOLICITUD',
                codigo: '',
                siguienteTabNombre: 'tab-3',
                volverTabNombre: 'tab-1',
                endPoint: '/solicitudes/reclamo',
                forzarConfirmacion: false,
                valores: [
                    { key: 'Tarjeta', value: '<strong>' + this.tarjetaActual.numeroTarjeta },
                    { key: 'Motivo de la solicitud', value: this.motivo.value },
                    { key: 'Será enviado a', value: this.direccionEnvio },
                    { key: 'Descripcion', value: this.descripcionSolicitud },
                    { key: 'Solución esperada', value: this.solucionEsperada }
                ],
                mostrarOfertas: false
            };

            var data = {
                idTipoOferta: '',
                idTarjeta: this.tarjetaActual.idTarjeta,
                tarjetaProducto: this.tarjetaActual.nombreProducto,
                tarjetaNumero: this.tarjetaActual.numeroTarjeta,
                motivo: this.motivo.value,
                medio: this.medioRespuesta.value,
                direccionEnvio: this.direccionEnvio,
                descripcion: this.descripcionSolicitud,
                solucionEsperada: this.solucionEsperada,

                datosCorreo: contenido.valores
            };

            this.content = contenido;
            this.sessionStorage.confirmacionData = data;
            this.activarMenu('tab-2');

        }

        get descripcion(): string {
            return this.descripcionSolicitud;
        }

        set descripcion(value: string) {
            if (value && value.length > this.descripcionSolicitudMaxLength) {
                this.descripcionSolicitud = value.substr(this.descripcionSolicitudMaxLength);
                return;
            }

            this.descripcionSolicitud = value;
        }

        get solucion(): string {
            return this.solucionEsperada;
        }

        set solucion(value: string) {
            if (!value) {
                this.solucionEsperada = value;
                return;
            }

            if (value.length > this.solocionEsperadaMaxLength) {
                this.solucionEsperada = value.substr(this.solocionEsperadaMaxLength);
                return;
            }

            this.solucionEsperada = value;
        }

        limpiarDireccion() {
            this.direccionEnvio = '';
        }
        activarMenu(menuItem: string): void {
            this.tabSeleccionado = menuItem;
        }

        menuSeleccionado(menuItem: string): boolean {
            return this.tabSeleccionado === menuItem;
        }
    }
}