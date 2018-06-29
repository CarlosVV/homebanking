namespace Common {
    export class Utility {

        static processGreetingsLangByCountry(isLocal: Boolean, countryCode: string): string {
            let morning: string;
            let afternoon: string;
            let evening: string;

            if (isLocal) {
                morning = '¡Buenos días!';
                afternoon = '¡Buenas tardes!';
                evening = '¡Buenas noches!';
            } else {
                morning = 'Good morning!';
                afternoon = 'Good afternoon!';
                evening = 'Good evening!';
            }

            moment.locale(countryCode);
            const hora = moment().hour();
            return (hora < 12) ? morning : (hora >= 12 && hora < 19 ? afternoon : evening);
        }

        static processIconByCountry(isLocal: Boolean, countryCode: string): string {
            const morning = 'icon1-coffee';
            const afternoon = 'icon1-Sun';
            const evening = 'icon1-moon';

            moment.locale(countryCode);
            const hora = moment().hour();
            return (hora < 12) ? morning : (hora >= 12 && hora < 19 ? afternoon : evening);
        }

        static processBannerByCountry(isLocal: Boolean): string {
            const bannerLocal = 'container__section-cabecera-noche';
            const bannerNoLocal = 'container__section-cabecera-dia';

            return (isLocal) ? bannerLocal : bannerNoLocal;
        }

        static obtenerFormatoFechaVencimiento(fechaVencimiento: string, formatoFecha?: string): string {
            let currentDate = new Date();
            currentDate.setHours(0, 0, 0, 0);
            let fechaPago = new Date(fechaVencimiento);
            fechaPago.setHours(0, 0, 0, 0);
            let fechaSegundodia = new Date();
            fechaSegundodia.setDate(currentDate.getDate() + 2);
            fechaSegundodia.setHours(0, 0, 0, 0);
            let result: string;
            if (fechaPago) {
                moment.locale('es');
                if (formatoFecha != null) {
                    result = moment(fechaPago).format('DD/MM/YYYY');
                } else {
                    result = moment(fechaPago).format('ddd DD MMM');
                    result = this.formatearFecha_MensajeVencimiento(result);
                }
            }
            let resultadoFormateado: string;
            if (fechaPago < currentDate) {
                resultadoFormateado = (formatoFecha != null) ? `${result} (Venció)` : `Venció ${result} `;
            } else if (fechaPago > currentDate) {
                if (fechaPago > fechaSegundodia) {
                    resultadoFormateado = (formatoFecha != null) ? `${result}` : `Vence ${result}`;
                } else if (fechaPago < fechaSegundodia) {
                    resultadoFormateado = (formatoFecha != null) ? `${result} (Vence MAÑANA)` : `Vence MAÑANA ${result}`;
                } else {
                    resultadoFormateado = (formatoFecha != null) ? `${result} (Faltan 2 días)` : `Vence ${result} (Faltan 2 días)`;
                }

            } else {
                resultadoFormateado = (formatoFecha != null) ? `${result} (Vence HOY)` : `Vence HOY ${result}`;
            }

            return resultadoFormateado;
        }

        static obtenerColorFechaVencimiento(fechaVencimiento: string): string {
            let currentDate = new Date();
            currentDate.setHours(0, 0, 0, 0);
            let fechaPago = new Date(fechaVencimiento);
            fechaPago.setHours(0, 0, 0, 0);
            let fechaSegundodia = new Date();
            fechaSegundodia.setDate(currentDate.getDate() + 2);
            fechaSegundodia.setHours(0, 0, 0, 0);
            let result: string;
            if (fechaPago) {
                moment.locale('es');
                result = moment(fechaPago).format('ddd DD MMM'); // dddd MMM
            }

            if (fechaPago < currentDate) {
                return 'naranja';
            } else if (fechaPago > currentDate) {
                if (fechaPago > fechaSegundodia) {
                    return 'negro';
                }
            }
            return 'naranja';
        }

        static formatearFecha(fecha: string): string {
            // el input de la fecha deber tener el formato (model.fecha | date : 'dd MMMM') => 01 enero / 15 marzo / 25 noviembre
            // se mostrará la fecha en el siguiente formato: 01 Ene / 31 Oct / 15 Dic

            const strFecha = fecha.toString();
            const diaMes = strFecha.substring(0, 2);
            const letraMesMayuscula = strFecha.substring(3, 4).toUpperCase();
            const nuevoFormatoFecha = diaMes + ' ' + letraMesMayuscula + strFecha.substring(4, 6);

            return nuevoFormatoFecha;
        }

        static formatearNombre(nombre: string, segundoNombre: string, apillidoPaterno: string, apellidoMaterno: string): string {
            // se mostrará las 5 primeras letras de nombre, si el nombre es menor a 5 caracteres, se tomara las del segundo nombre, en caso de no tener segundo nombre
            // se tomará las del apellido paterno.

            let nuevoFormatoNombre = '';

            if (nombre.length >= 4) {
                nuevoFormatoNombre = nombre.substring(0, 5);
            } else if (nombre.length === 3) {
                nuevoFormatoNombre = nombre + ' ' + (segundoNombre !== '' ? segundoNombre.substring(0, 1) : apillidoPaterno.substring(0, 1));
            } else if (nombre.length === 2) {
                nuevoFormatoNombre = nombre + ' ' + (segundoNombre !== '' ? segundoNombre.substring(0, 2) : apillidoPaterno.substring(0, 2));
            }

            return nuevoFormatoNombre;
        }

        static formatearDescripcionMovimiento(descripcion: string): string {
            // si la cantidad de caracteres es mayor o igual a 41, se mostrará solo los primeros 40.

            let nuevaDescripcion: string = '';

            if (descripcion.length >= 41) {
                nuevaDescripcion = descripcion.substring(0, 40);
            } else {
                nuevaDescripcion = descripcion;
            }

            return nuevaDescripcion;
        }

        static formatearLugarMovimiento(lugar: string): string {
            // si la cantidad de caracteres es mayor o igual a 31, se mostrará solo los primeros 30.

            let nuevoDetalleLugar: string;

            if (lugar.length >= 31) {
                nuevoDetalleLugar = lugar.substring(0, 30);
            } else {
                nuevoDetalleLugar = lugar;
            }

            return nuevoDetalleLugar;
        }

        static formatearNumeroTarjeta(numeroTarjeta: string): string {
            let nuevoFormato: string = '';
            // nuevoFormato = numeroTarjeta.substring(0, 4) + ' ' + numeroTarjeta.substring(5, 10) + ' ' + numeroTarjeta.substring(10, 14);
            nuevoFormato = '**** ****** ' + numeroTarjeta.substring(10, 14);
            return nuevoFormato;
        }

        static mostrarUltimosCuatroDigitosNumeroTarjeta(numeroTarjeta: string): string {
            let nuevoFormato: string = '';
            if (numeroTarjeta && numeroTarjeta.length) {
                nuevoFormato = numeroTarjeta.substring(10, 14);
            }
            return nuevoFormato;
        }

        static mostrarNumeroTarjetaConAsterisco(numeroTarjeta: string): string {
            let numeroTarjetaParcial: string = '';
            if (numeroTarjeta.length) {
                numeroTarjetaParcial = numeroTarjeta.substring(numeroTarjeta.length - 4, numeroTarjeta.length);
            }
            return `**** ****** ${numeroTarjetaParcial}`;
        }

        static mostrarAlias_o_NombreProducto(alias: string, nombreProducto: string): string {
            if (alias !== '' && alias !== undefined) {
                return alias;
            }
            return nombreProducto;
        }

        static validarFormatoMontos(monto: number): boolean {
            var patt = /^\d+(\.\d{1,2})?$/;
            var resultado = false;
            if (monto) {
                resultado = patt.test(monto.toString());
            }
            return resultado;
        }

         static FormatearMontos(monto: number, currency: string): string {
             if (!monto) {
                 return '';
             }
            return currency + ' ' + Number(monto).toFixed(2).replace(/(\d)(?=(\d{3})+\.)/g, '$1,');
        }

        static FormatearMontosAdosDecimales(monto: number): string {
             if (!monto) {
                 return '';
             }
            return  monto.toFixed(2);
        }

		static generarUrlImagenSeguridad(idImagen: string): string {
            let urlImagen: string;

            switch (idImagen) {
                case '1':
                    urlImagen = 'content/images/img_profile-2.jpg';
                    break;
                case '2':
                    urlImagen = 'content/images/img_profile-3.jpg';
                    break;
                case '3':
                    urlImagen = 'content/images/img_profile-4.jpg';
                    break;
                case '4':
                    urlImagen = 'content/images/img_profile-5.jpg';
                    break;
                case '5':
                    urlImagen = 'content/images/img_profile-6.jpg';
                    break;
                case '6':
                    urlImagen = 'content/images/img_profile-7.jpg';
                    break;
                case '7':
                    urlImagen = 'content/images/img_profile-8.jpg';
                    break;
                case '8':
                    urlImagen = 'content/images/img_profile-9.jpg';
                    break;
                default:
                    urlImagen = '';
            }

            return urlImagen;
        }

        static generarUrlImagenFacebook(idFacebook: string) {
            return 'http://graph.facebook.com/' + idFacebook + '/picture?type=large';
        }

        static obtenerImagenesSeguridad(): any[] {
            let items: any[] = [
                {'src': 'content/images/img_profile-2.jpg', 'id': 1},
                {'src': 'content/images/img_profile-3.jpg', 'id': 2},
                {'src': 'content/images/img_profile-4.jpg', 'id': 3},
                {'src': 'content/images/img_profile-5.jpg', 'id': 4},
                {'src': 'content/images/img_profile-6.jpg', 'id': 5},
                {'src': 'content/images/img_profile-7.jpg', 'id': 6},
                {'src': 'content/images/img_profile-8.jpg', 'id': 7},
                {'src': 'content/images/img_profile-9.jpg', 'id': 8}
            ];

            return items;
        }

        private static formatearFecha_MensajeVencimiento(fecha: string): string {
            // formato de fecha (input) es 'jue. 29 sep.', 'lun. 29 sep.'
            let nuevoFormato = fecha.substring(0, 3) + ' ' + fecha.substring(7, 5) + ' ' + fecha.substring(11, 8);
            return nuevoFormato;
        }
    }
}