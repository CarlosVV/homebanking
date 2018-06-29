namespace Confirmacion {
    export interface IContent {
        titulo: string;
        subTituloInformacion?: string;
        subTitulo: string;
        subTituloMensaje: string;
        botonTexto: string;
        codigo: string;
        volverTabNombre: string;
        siguienteTabNombre: string;
        endPoint: string;
        httpVerb?: Api.HttpVerb;
        scope?: Api.AuthScope;
        forzarConfirmacion: boolean;
        valores: IDictionary[];
        mostrarOfertas: boolean;
        mostrarConstancia?: boolean;
        idModal?: string;
    }

    export interface IDictionary {
        key: string;
        value: string;
    }

    export interface ITerminosyCondicionesModel {
        codigo: string;
        contenido: string;
    }
}
