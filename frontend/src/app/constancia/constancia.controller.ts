namespace Constancia {
    export class ConstanciaController {
        datosSolicitud: Confirmacion.IContent;
        nombreUsuario: string;
        mostrarOfertas: Boolean;
        esBloqueoTarjeta: Boolean;
        bannerByCountry: string;
        solicitudPdf: IConstanciaModel = {
            titulo : '',
            socio : '',
            datos: ['']
        };

        constructor(private sessionFactory: Common.SessionFactory, $rootScope: angular.IRootScopeService, private $sce: ng.ISCEService,
            private $scope: angular.IScope, private constanciaFactory: ConstanciaFactory) {
            // super($rootScope);
            this.$scope.$watch('content', (newValue: Confirmacion.IContent, oldValue: Confirmacion.IContent) => {
                if (newValue) {
                    this.datosSolicitud = newValue;
                    this.mostrarOfertas = this.datosSolicitud.mostrarOfertas;
                    this.esBloqueoTarjeta = this.datosSolicitud.subTitulo ? this.datosSolicitud.subTitulo.length > 0 : false;
                }
            });
        }

        inicializar() {
            // this.datosSolicitud = this.sessionFactory.confirmacion;
            this.nombreUsuario = this.sessionFactory.nombreUsuario;
            this.processBannerByCountry();
        }

        descargarComprobante() {
            this.solicitudPdf.socio = this.nombreUsuario;
            this.solicitudPdf.titulo = this.datosSolicitud.titulo;
            let dataPdf = this.datosSolicitud.valores;
            let cuerpotabla: any = [];
            // recorrer los datos de la solicitud
            dataPdf.forEach(element => {
                let nuevoValue = this.removerTags(element.value);
                let nuevoKey = this.removerTags(element.key);
                cuerpotabla.push([nuevoKey, nuevoValue]);
            });
            this.solicitudPdf.datos = cuerpotabla;
            this.constanciaFactory.descargarPdfSolicitud(this.solicitudPdf)
                .then((response: angular.IHttpPromiseCallbackArg<any>) => {
                    let file = new Blob([response.data], {
                        type: response.headers('Content-Type')
                    });
                    saveAs(file, 'Solicitud.pdf');
                });
        }
        descargarComprobanteFrontEnd() {
            let dataPdf = this.datosSolicitud.valores;
            let cuerpotabla: any = [];
            let cabeceraTabla: any = [];
            cabeceraTabla.push({ text: '', style: 'header' }, { text: '', style: 'header' });
            // recorrer los datos de la solicitud
            dataPdf.forEach(element => {
                let nuevoValue = this.removerTags(element.value);
                let nuevoKey = this.removerTags(element.key);
                cuerpotabla.push([nuevoKey, nuevoValue]);
            });

            cuerpotabla.unshift(cabeceraTabla);

            let docDefinition: any = {};
            let docDefinitionTabla: any = {};
            docDefinition.content = [];
            docDefinition.content.push({ text: this.datosSolicitud.titulo });
            docDefinitionTabla.style = 'tablaMovi';
            docDefinitionTabla.table = {
                // widths: ['*', '*', '*', '*', '*', '*', '*', '*'],
                body: cuerpotabla
            };
            docDefinition.content.push(docDefinitionTabla);
            docDefinition.styles = {
                header: {
                    bold: true,
                    color: '#000',
                    fontSize: 10
                },
                tablaMovi: {
                    color: '#666',
                    fontSize: 10
                }
            };
            // crear PDf 
            pdfMake.createPdf(docDefinition).open();
        }

        trustedValue(value: string): any {
            return this.$sce.trustAsHtml(value);
        }

        removerTags(texto: string): string {
            if (texto === undefined) {
                return '';
            }

            let rex = /<(?:.|\n)*?>/gm;
            return texto.toString().replace(rex, '');
        }

        processBannerByCountry() {
            var isLocal = this.sessionFactory.geolocation.isLocal;
            this.bannerByCountry = Common.Utility.processBannerByCountry(isLocal);
        }
    }
}