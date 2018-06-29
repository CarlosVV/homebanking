namespace Api {
    export class TarjetasApi extends BaseApi {
        constructor(private $http: angular.IHttpService, private configuracion: any, sessionFactory: Common.SessionFactory) {
            super(configuracion.urlServicioApi + '/tarjetas', sessionFactory);
        }

        get(): angular.IHttpPromise<ITarjeta[]> {
            return this.$http.get(this.baseUrl, {
                headers: this.getAuthorizationHeader()
            });
        }

        getAdicionales(idTarjeta: string): angular.IHttpPromise<ITarjeta | ITarjeta> {
            let url = `${this.baseUrl}/${idTarjeta}/adicionales `;
            return this.$http.get(url, {
                headers: this.getAuthorizationHeader()
            });
        }

        bloquearTarjeta(idTarjeta: string, tarjeta: any): angular.IHttpPromise<any> {
            let url = `${this.baseUrl}/${idTarjeta}/bloquear`;
            return this.$http.post(url,
                tarjeta,
                { headers: this.getAuthorizationHeader() });
        }

        getDetalle(idTarjeta: string): angular.IHttpPromise<ITarjeta | ITarjeta> {
            let url = `${this.baseUrl}/${idTarjeta}`;
            return this.$http.get(url, {
                headers: this.getAuthorizationHeader()
            });
        }

         cambiarAlias(idTarjeta: string, alias: string): angular.IHttpPromise<boolean> {
            let url = `${this.baseUrl}/${idTarjeta}/cambiar-alias/?alias=${alias}`;
            return this.$http.post(url, {params: {'alias': alias}}, { headers: this.getAuthorizationHeader()});
        }
    }

    export interface ITarjeta {
        idTarjeta: string;
        numeroTarjeta: string;
        alias: string;
        nombreProducto: string;
        lineaCreditoSoles: number;
        lineaCreditoDolares: number;
        lineaDisponibleSoles: number;
        lineaDisponibleDolares: number;
        millasDisponibles: number;
        montoTotalMesSoles: number;
        montoTotalMesDolares: number;
        montoMinimoMesSoles: number;
        montoMinimoMesDolares: number;
        fechaVencimiento: string;
        pagado: boolean;
        idTarjetaTitular: string;
        tieneAdicionales: boolean;
        socioTarjeta: Tarjeta.SocioTarjetaViewModel;
        adicionales: Tarjeta.DetalleTarjetaViewModel[];
        estadoTarjeta: number;
        srcImagenTarjeta?: string;
        idTipoProducto?: string;
        numeroOperacion: number;
        fechaOperacion: Date;
    }

    export interface ITarjetaAdicionalDetalleViewModel {
        nombre: string;
        segundoNombre: string;
        apellidoPaterno: string;
        apellidoMaterno: string;
        tipoDocumento: string;
        numeroDocumento: string;
        numeroTelefono: string;
        fechaNacimiento: Date;
        correo: string;
        nombreTarjeta: string;
        topeConsumoMensual: number;
    }

    export interface ITarjetaAdicionalViewModel {
        idTipoOferta: string;
        idTarjeta: string;
        datosCorreo: any[];
        nombreProducto: string;
        numeroTarjeta: string;
        lineaCreditoSoles: number;
        tarjetasAdicionalesDetalle: ITarjetaAdicionalDetalleViewModel[];
    }
}