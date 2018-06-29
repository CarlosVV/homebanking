namespace Tarjeta {
    export class TarjetaService {
        idTarjeta: string;
        tarjetas: [any];
        tarjetasTitular: [any];

        getTarjeta(): string {
            return this.idTarjeta;
        }

        setTarjeta(tarjeta: string) {
            this.idTarjeta = tarjeta;
        }

        setListaTarjetaConAdicionales(tarjetas: [any]) {
            this.tarjetas = tarjetas;
        }

        getListaTarjetaConAdicionales(): [any] {
            return this.tarjetas;
        }
    }
}