namespace Tarjeta {
    export class DetalleTarjetaViewModel implements Api.ITarjeta {
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
        srcImagenTarjeta: string;
        tieneAdicionales: boolean;
        adicionales: DetalleTarjetaViewModel[];
        socioTarjeta: Tarjeta.SocioTarjetaViewModel;
        esTitular: boolean;
        estadoTarjeta: number;
        opcionesTarjeta: any[];
        opcionActual: string;
        cantidadAdicionales?: number;
        numeroOperacion: number;
        fechaOperacion: Date;

        get urlTarjeta(): string {
            return `Content/images/${this.nombreProducto}.png`;
        }
    }
}