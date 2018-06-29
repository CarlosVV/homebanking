namespace EstadoCuenta {
    export class EstadoCuentaModel {
        fechaInicioPeriodo: Date;

        fechaFinPeriodo: Date;

        fechaVencimiento: string;

        estaVencido: boolean;

        deudaAnteriorSoles: number;

        deudaAnteriorDolares: number;

        abonoSoles: number;

        abonoDolares: number;

        consumosSoles: number;

        consumosDolares: number;

        comisionesInteresesPenalidadesGastosSoles: number;

        comisionesInteresesPenalidadesGastosDolares: number;

        deudaTotalSoles: number;

        deudaTotalDolares: number;

        montoAPagarMinimoSoles: number;

        montoAPagarMinimoDolares: number;

        montoAPagarTotalSoles: number;

        montoAPagarTotalDolares: number;

        millasSaldoAnterior: number;

        millasGanadas: number;

        millasVencidas: number;

        millasDisponibles: number;

        idTarjeta: string;
    }
}