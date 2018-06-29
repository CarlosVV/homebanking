namespace Tarjeta {
    export class SocioTarjetaViewModel {
        idTarjeta: string;
        nombre: string;
        apellidoMaterno: string;
        apellidoPaterno: string;
        segundoNombre: string;

        get nombreCompleto(): string {
            return `${this.nombre} ${this.apellidoPaterno} ${this.apellidoMaterno}`;
        }
    }
}