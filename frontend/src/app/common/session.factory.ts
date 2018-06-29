namespace Common {
    export interface IDinersLocalStorage {
        rememberUserName: string;
    }

    export interface IDinersSessionStorage {
        tarjetaTokenType: string;
        tarjetaAccessToken: string;
        claveDigitalTokenType: string;
        claveDigitalAccessToken: string;
        claveDigitalOperacionesTokenType: string;
        claveDigitalOperacionesAccessToken: string;
        geolocation: string;
        documentoIdentidad: string;
        confirmacion: string;
        confirmacionData: string;
        nombreUsuario: string;
        primerIngreso: boolean;
        ofertas: string;
        tarjetas: string;
        datosUsuario: Api.IUsuarioGet;
        imagenSeguridad: string;
    }

    export class SessionFactory {
        constructor(private $localStorage: IDinersLocalStorage, private $sessionStorage: IDinersSessionStorage) {
        }

        get tarjetaTokenType(): string {
            return this.$sessionStorage.tarjetaTokenType;
        }

        set tarjetaTokenType(tokenType: string) {
            this.$sessionStorage.tarjetaTokenType = tokenType;
        }

        get tarjetaAccessToken(): string {
            return this.$sessionStorage.tarjetaAccessToken;
        }

        set tarjetaAccessToken(accessToken: string) {
            this.$sessionStorage.tarjetaAccessToken = accessToken;
        }

        get claveDigitalTokenType(): string {
            return this.$sessionStorage.claveDigitalTokenType;
        }

        set claveDigitalTokenType(tokenType: string) {
            this.$sessionStorage.claveDigitalTokenType = tokenType;
        }

        get claveDigitalAccessToken(): string {
            return this.$sessionStorage.claveDigitalAccessToken;
        }

        set claveDigitalAccessToken(accessToken: string) {
            this.$sessionStorage.claveDigitalAccessToken = accessToken;
        }

        get claveDigitalOperacionesTokenType(): string {
            let tokenType = this.$sessionStorage.claveDigitalOperacionesTokenType;
            this.$sessionStorage.claveDigitalOperacionesTokenType = null;
            return tokenType;
        }

        set claveDigitalOperacionesTokenType(tokenType: string) {
            this.$sessionStorage.claveDigitalOperacionesTokenType = tokenType;
        }

        get claveDigitalOperacionesAccessToken(): string {
            let accessToken = this.$sessionStorage.claveDigitalOperacionesAccessToken;
            this.$sessionStorage.claveDigitalOperacionesAccessToken = null;
            return accessToken;
        }

        set claveDigitalOperacionesAccessToken(accessToken: string) {
            this.$sessionStorage.claveDigitalOperacionesAccessToken = accessToken;
        }

        isAuthenticated(): boolean {
            return !!this.$sessionStorage.claveDigitalAccessToken;
        }

        isAuthenticatedByTarjeta(): boolean {
            return !!this.$sessionStorage.tarjetaAccessToken;
        }

        get documentoIdentidad(): string {
            return this.$sessionStorage.documentoIdentidad;
        }

        set documentoIdentidad(documentoIdentidad: string) {
            this.$sessionStorage.documentoIdentidad = documentoIdentidad;
        }

        clearSessions(): void {
            this.$sessionStorage.claveDigitalAccessToken = null;
            this.$sessionStorage.tarjetaAccessToken = null;
            this.$sessionStorage.geolocation = null;
            this.$sessionStorage.documentoIdentidad = null;
            this.$sessionStorage.nombreUsuario = null;
            this.$sessionStorage.confirmacion = null;
            this.$sessionStorage.ofertas = null;
            this.$sessionStorage.datosUsuario = null;
            this.$sessionStorage.imagenSeguridad = null;
        }

        get geolocation(): Geolocalizacion.IGeoLocalizacion {
            var tmp = JSON.parse(this.$sessionStorage.geolocation) as Geolocalizacion.IGeoLocalizacion;
            return tmp;
        }

        set geolocation(geolocation: Geolocalizacion.IGeoLocalizacion) {
            var obj = JSON.stringify(geolocation);
            this.$sessionStorage.geolocation = obj;
        }

        /** @deprecated */
        get confirmacion(): Confirmacion.IContent {
            return JSON.parse(this.$sessionStorage.confirmacion) as Confirmacion.IContent;
        }

        /**
         * @deprecated 
         */
        set confirmacion(model: Confirmacion.IContent) {
            this.$sessionStorage.confirmacion = JSON.stringify(model);
        }

        get confirmacionData(): any {
            return JSON.parse(this.$sessionStorage.confirmacionData);
        }

        set confirmacionData(model: any) {
            this.$sessionStorage.confirmacionData = JSON.stringify(model);
        }

        get nombreUsuario(): string {
            return this.$sessionStorage.nombreUsuario;
        }

        set nombreUsuario(nombreUsuario: string) {
            this.$sessionStorage.nombreUsuario = nombreUsuario;
        }

        /** Setea el primer ingreso de un usuario */
        setPrimerIngreso() {
            this.$sessionStorage.primerIngreso = true;
        }

        /** Verifica si es el primer ingreso del usuario. Si devuelve true inmediatamente setea la variable en false  */
        isPrimerIngreso() {
            let isPrimerIngreso = this.$sessionStorage.primerIngreso;
            if (this.$sessionStorage.primerIngreso) {
                this.$sessionStorage.primerIngreso = false;
            }

            return isPrimerIngreso;
        }

        get ofertas(): Oferta.OfertaModel[] {
            return JSON.parse(this.$sessionStorage.ofertas) as Oferta.OfertaModel[];
        }

        set ofertas(model: Oferta.OfertaModel[]) {
            this.$sessionStorage.ofertas = JSON.stringify(model);
        }

        get datosUsuario(): Api.IUsuarioGet {
            return this.$sessionStorage.datosUsuario;
        }

        set datosUsuario(datoUsuario: Api.IUsuarioGet) {
            this.$sessionStorage.datosUsuario = datoUsuario;
        }

        get imagenSeguridad(): any {
            return JSON.parse(this.$sessionStorage.imagenSeguridad);
        }

        set imagenSeguridad(imagenSeguridad: any) {
            this.$sessionStorage.imagenSeguridad = JSON.stringify(imagenSeguridad);
        }
    }
}