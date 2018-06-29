namespace FormasPago {
    export class FormasPagoController {
        listaBancosAfiliados : Common.BancoAfiliado[];
        bannerByCountry: string;

        constructor(private bancoAfiliadoFactory: Common.BancoAfiliadoFactory,
            private sessionFactory: Common.SessionFactory) {
            // 
        }

        inicializar() {
            this.obtenerBancosAfiliados();
            this.processBannerByCountry();
        }

        obtenerBancosAfiliados() {
            this.bancoAfiliadoFactory.get().then((response: angular.IHttpPromiseCallbackArg<Common.BancoAfiliado[]>) => {
                if (response.status === 200) {
                    if (response.data) {
                        this.listaBancosAfiliados = response.data.map((item: Common.BancoAfiliado) => {
                            let bancoAfiliado = new Common.BancoAfiliado();
                            bancoAfiliado.idBanco = item.idBanco;
                            bancoAfiliado.nombreBanco = item.nombreBanco;
                            bancoAfiliado.pagoVentanilla = item.pagoVentanilla;
                            bancoAfiliado.pagoInternet = item.pagoInternet;
                            bancoAfiliado.pagoCargoEnCuenta = item.pagoCargoEnCuenta;
                            bancoAfiliado.urlArchivo = item.urlArchivo;
                            return bancoAfiliado;
                        });
                    }
                } else {
                    console.log('Lo sentimos la aplicación Diners Club Online se detuvo');
                }
            }, () => {
                console.log('Lo sentimos la aplicación Diners Club Online se detuvo');
            });
        }

        processBannerByCountry() {
            var isLocal = this.sessionFactory.geolocation.isLocal;
            this.bannerByCountry = Common.Utility.processBannerByCountry(isLocal);
        }
    }
}