namespace PosicionConsolidada {
    export class ResumenTarjetaModel {
        idTarjeta : string;
        alias  : string;
        nombreProducto  : string;
        lineaDisponibleSoles : number;
        lineaDisponibleDolares : number;
        millasDisponibles : number;
        montoTotalMesSoles : number;
        montoTotalMesDolares : number;
        montoMinimoMesSoles : number;
        montoMinimoMesDolares : number;
        fechaVencimiento : string;
        pagado : boolean;
    }
}