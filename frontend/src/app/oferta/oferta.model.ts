namespace Oferta {
    export class OfertaModel implements Api.IOfertaModel {
        idTipoOferta: string;
        idTarjeta: string;
        montoLineaNueva: number;
        fechaInicio: Date;
        fechaFin: Date;
        montoOferta: number;
        tea: number;
        tcea: number;
        TextoOferta?: OfertaTextos;
        TextoOfertaDetalle?: OfertaDetalleTextos;
    }
    export class OfertaTextos {
        idTipoOferta: string;
        oferta: string;
        titulo: string;
        descripcion: string;
        botonTexto: string;
        rutaSolicitud: string;
        TextoBotonInformacion: string;
    }
    export class OfertaListaTextos {
        static textosPorTipoOferta: OfertaTextos[] = [
            { idTipoOferta: '1', oferta: 'ampliacion de linea', titulo: 'Tiene un Incremento de Línea a', descripcion: '&nbsp;', botonTexto: 'ME INTERESA', rutaSolicitud: 'ofertas/detalle/', TextoBotonInformacion: 'ME INTERESA' },
            { idTipoOferta: '2', oferta: 'préstamo personal', titulo: 'Tiene un Préstamo Personal Diners por', descripcion: '&nbsp;', botonTexto: 'MÁS INFORMACIÓN', rutaSolicitud: 'ofertas/detalle/', TextoBotonInformacion: 'MÁS INFORMACIÓN' },
            { idTipoOferta: '3', oferta: 'compra deuda', titulo: 'Solicita tu compra de deuda APROBADA por', descripcion: '(en 36 cuotas)', botonTexto: 'SOLICITAR MI COMPRA DE DEUDA', rutaSolicitud: 'ofertas/detalle/', TextoBotonInformacion: '' },
            { idTipoOferta: '4', oferta: 'dinero en efectivo', titulo: 'Su monto disponible para Dinero en Efectivo es', descripcion: '&nbsp;', botonTexto: 'MÁS INFORMACIÓN', rutaSolicitud: 'ofertas/detalle/', TextoBotonInformacion: 'MÁS INFORMACIÓN' }
        ];
    }
    export class OfertaDetalleTextos {
        idTipoOferta: string;
        oferta: string;
        titulo: string;
        descripcion: string;
        botonTexto: string;
        rutaSolicitud: string;
        descripcion2?: string;
        detalles: string[] = [];
    }
    export class OfertaDetalleListaTextos {
        static textosPorTipoOfertaDetalle: OfertaDetalleTextos[] = [
            {
                idTipoOferta: '1', oferta: 'Felicidades', titulo: 'Tiene un Incremento de Línea a:', descripcion: 'Accede a este beneficio exclusivo de Diners Club', botonTexto: 'SOLICITAR AHORA', rutaSolicitud: '/soluciones-financieras',
                detalles: ['Hazlo en un solo click', 'Sin costo ni trámites']
            },
            {
                idTipoOferta: '2', oferta: 'Felicidades', titulo: 'Tiene un Préstamo Personal Diners por:', descripcion: 'fináncialo desde 12 hasta 36 cuotas mensuales', descripcion2: 'Accede a este beneficio exclusivo de Diners Club', botonTexto: 'SOLICITAR AHORA', rutaSolicitud: '/solicitud-prestamo-personal',
                detalles: ['Su tasa preferencial es:<tcea>', 'Sin costo ni trámites']
            },
            {
                idTipoOferta: '3', oferta: 'solicita tu compra de deuda gratis', titulo: 'Tienes una compra de deuda <b>APROBADA</b> por', descripcion: 'en 36 cuotas', descripcion2: 'Simplifica tu vida y olvídate de las tarjetas', botonTexto: 'SOLICITAR MI COMPRA DE DEUDA', rutaSolicitud: '',
                detalles: ['<b>TASA</b> INCREIBLE', 'AHORRA <b>TIEMPO</b>', 'MAYOR <b>CONTROL</b>']
            },
            {
                idTipoOferta: '4', oferta: 'Felicidades', titulo: 'Su monto disponible para Dinero en Efectivo es:', descripcion: 'fináncialo desde 3 hasta 36 cuotas mensuales', descripcion2: 'Accede a este beneficio exclusivo de Diners Club', botonTexto: 'SOLICITAR AHORA', rutaSolicitud: '/solicitud-dinero-efectivo',
                detalles: ['Su tasa preferencial es:<tcea>', 'Sin costo ni trámites']
            }
        ];
    }
}