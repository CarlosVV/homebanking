namespace GenerarClaveDigital {
    export class Usuario {
        tarjetaId: string = '';
        claveDigital: string = '';
        email: string = '';
        numeroCelular: string = '';
        imagenId: string = '';
        facebookId: string = '';
        operadorTelefonico: Api.IOperadorTelefonico = null;
    }
}