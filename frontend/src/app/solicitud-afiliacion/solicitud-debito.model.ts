namespace SolicitudDebito {
    export class SolicitudDebitoViewModel {
        idSocio: string;
        idTarjeta: string;
        facturacionSoles: SolicitudDebitoFacturacionViewModel;
        facturacionDolares: SolicitudDebitoFacturacionViewModel;
        tipoPagoaCargar: string;
        nombreProducto: string;
        numeroTarjeta: string;
        idTipoOferta: string;
    }

    export class SolicitudDebitoFacturacionViewModel {
        idBanco: string;
        banco: string;
        tipoCuenta: string;
        monedaDelaCta: string;
        numeroCuenta: string;
        // guardarComoFrecuente: boolean;
    }
}