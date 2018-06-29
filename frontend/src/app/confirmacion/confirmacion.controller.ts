namespace Confirmacion {
    export class Controller {
        content: IContent;
        contentData: any;
        hasAccepted: boolean = false;
        terminosCondiciones: any;
        eventoVolverPadre: string = 'activarMenu';

        constructor(private sessionStorage: Common.SessionFactory, private confirmacionApi: Api.ConfirmacionApi, private $location: ng.ILocationService, private $sce: ng.ISCEService, private $filter: angular.IFilterService, private $scope: angular.IScope) {
            this.$scope.$watch('content', (newValue: IContent, oldValue: IContent) => {
                if (newValue) {
                    this.content = newValue;
                    if (this.content.forzarConfirmacion) {
                        this.terminosCondiciones = this.$sce.trustAsHtml(TerminosyCondiciones.get(this.content.codigo).contenido);
                    } else {
                        this.hasAccepted = true;
                    }
                }
            });
        }

        acceptAgreements() {
            this.hasAccepted = true;
        }

        sendRequest() {
            if (this.content.forzarConfirmacion) {
                if (!this.hasAccepted) {
                    return;
                }
            }
            this.contentData = this.sessionStorage.confirmacionData;

            if (!this.content.httpVerb) {
                this.content.httpVerb = 'POST';
            }

            this.confirmacionApi.send(this.content.endPoint, this.contentData, this.content.httpVerb, this.content.scope).then((response) => {
                if (!response.data) {
                    return;
                }

                if (response.data.numeroSolicitud) {
                    var numeroSolicitud = response.data.numeroSolicitud + '';
                    this.content.valores.unshift({ key: 'Nº Solicitud', value: numeroSolicitud });
                }

                if (response.data.fechaRegistro) {
                    var fechaRegistro = this.$filter('date')(response.data.fechaRegistro, 'dd/MM/yyyy HH:mm') + '';
                    this.content.valores.unshift({ key: 'Fecha y hora', value: fechaRegistro });
                }

                this.content.mostrarConstancia = true;
                this.sessionStorage.confirmacion = this.content;
                this.$scope.$emit(this.eventoVolverPadre, this.content.siguienteTabNombre);
            },
                (error) => { console.log(error); });
        }

        trustedValue(value: string): any {
            return this.$sce.trustAsHtml(value);
        }

        volverClick() {
            this.$scope.$emit(this.eventoVolverPadre, this.content.volverTabNombre);
        }
    }
}
