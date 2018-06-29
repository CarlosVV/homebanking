namespace Alerta {
    export interface IAlertaTarjetaUsuario {
        idTarjeta: string;
        emailSeleccionado: number;
        emailAdicional: string;
        telefonoAdicional: string;
        activo: boolean;
        alertasActivas: IAlertaTarjeta[];
    }

    export interface IAlertaTarjeta {
        idAlerta: string; // iAlerta;
        /** alerta seleccionada */
        alertaSeleccionada?: IAlerta;
        celular1Activo: boolean;
        celular2Activo: boolean;
        email1Activo: boolean;
        email2Activo: boolean;
        idCondicionAdicional: string; // iCondicionAdicional;
        /** condicion adicional seleccionada */
        condicioneAdicionalSeleccionada: ICondicionAdicional;
    }

    export interface IAlerta {
        idAlerta: string;
        nombre: string;
        descripcion: string;
        tituloCondicionAdicional?: string;
        condicionesAdicionales: ICondicionAdicional[];
    }

    export interface ICondicionAdicional {
        idCondicionAdicional: string;
        nombre: string;
        // opciones: string[];
    }
}