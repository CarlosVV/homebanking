namespace MiPerfil {
    export class CambiarImagenController {
        usuario: Api.IUsuarioGet;
        idImagenSeleccionada: number = 0;
        habilitado: boolean;
        imagenSeguridad: string;
        mostrarImagenFb: boolean = false;
        urlImagenFb: string;
        idImagenSeguridadActual: number;
        imagenesSeguridad: any[];

        constructor(private usuarioApi: Api.UsuarioApi,
            private operadoresTelefonicosApi: Api.OperadoresTelefonicosApi,
            private sessionFactory: Common.SessionFactory,
            private $location: angular.ILocationService,
            private $filter: angular.IFilterService,
            private $scope: angular.IScope,
            private $rootScope: angular.IRootScopeService) {
        }

        inicializar() {
            this.obtenerUsuario();
            this.obtenerImagen();
        }

        obtenerUsuario() {
            this.usuarioApi.get().then((result) => {
                this.usuario = result.data;
            });
        }

        grabarCambioImagen() {
            if (this.usuario.idFacebook === '' && this.usuario.idImagen === '0') { return; }
            if (this.idImagenSeleccionada !== 0) {
                this.usuario.idFacebook = '';
                this.usuario.idImagen = this.idImagenSeleccionada.toString();
            }

            this.usuarioApi.patchActualizarImagenPerfil(this.usuario).then((response: angular.IHttpPromiseCallbackArg<boolean>) => {
                if (response) {
                    let imagenSeguridad: any = {
                        tieneImagen: true,
                        urlImagen: Common.Utility.generarUrlImagenSeguridad(this.idImagenSeleccionada.toString())
                    };
                    this.sessionFactory.imagenSeguridad = imagenSeguridad;
                    this.obtenerImagen();
                    this.$rootScope.$broadcast('refrescarImagenSeguridad', { message: 'LLamado desde CambiarImagenController' });
                }
            }, () => {
                console.log('Lo sentimos la aplicación Diners Club Online se detuvo');
            });
        }

        seleccionarImagen(idImagen: any): void {
            var index = idImagen - 1;
            var elemento = angular.element(document.querySelector('#li' + index));
            elemento.prepend('<i class="icon1-checked checkIcon"></i>');
            elemento.siblings().children('i').remove();
            this.idImagenSeleccionada = idImagen;
            this.habilitado = true;
        }

        obtenerImagen() {
            this.imagenesSeguridad = Common.Utility.obtenerImagenesSeguridad();
            this.imagenSeguridad = this.sessionFactory.imagenSeguridad.urlImagen;
            let indexImagen = this.imagenesSeguridad.map(item => { return item.src; }).indexOf(this.imagenSeguridad);

            if (indexImagen > -1) {
                this.idImagenSeleccionada = indexImagen + 1;
                this.idImagenSeguridadActual = indexImagen + 1;
                this.mostrarImagenFb = false;
            } else {
                this.mostrarImagenFb = true;
                this.urlImagenFb = this.imagenSeguridad;
                this.idImagenSeguridadActual = 0;
            }
        }
    }
}
