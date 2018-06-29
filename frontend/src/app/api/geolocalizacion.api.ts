namespace Geolocalizacion {
    export interface IGeoLocalizacion {
        // https://en.wikipedia.org/wiki/ISO_3166-1
        countryCode: string;
        isLocal: boolean;
        zoneInfo: any; // feature
    }

    export interface IGoogleMapsApiResult {
        results: google.maps.places.PlaceResult[];
        status: string;
    }

    export class GeoAPi {
        private googleCountryFilter: string = 'country';

        constructor(private $http: angular.IHttpService,
            private $q: angular.IQService,
            private $window: angular.IWindowService,
            private $rootScope: angular.IRootScopeService,
            private configuracion: any,
            private sessionFactory: Common.SessionFactory) {
        }

        forceInitialize() {
            var zone = this.defaultGeoLocation;
            this.sessionGeolocation = zone;
            this.checkLocation().then((response) => {
                var zone: IGeoLocalizacion = {
                    isLocal: response.address_components[0].short_name === this.configuracion.peruCountryCode,
                    countryCode: response.address_components[0].short_name,
                    zoneInfo: response
                };

                this.sessionGeolocation = zone;
            },
                (error) => {
                    var zone = this.defaultGeoLocation;
                    this.sessionGeolocation = zone;
                });
        }

        isLocal(): angular.IPromise<IGeoLocalizacion> {
            var deferred = this.$q.defer();
            if (this.sessionGeolocation) {
                deferred.resolve(this.sessionGeolocation);
            } else {
                this.checkLocation().then((response) => {
                    var zone: IGeoLocalizacion = {
                        isLocal: response.address_components[0].short_name === this.configuracion.peruCountryCode,
                        countryCode: response.address_components[0].short_name,
                        zoneInfo: response
                    };

                    this.sessionGeolocation = zone;
                    deferred.resolve(zone);
                },
                    (error) => {
                        var zone = this.defaultGeoLocation;
                        this.sessionGeolocation = zone;
                        deferred.resolve(zone);
                    });
            }

            return deferred.promise;
        }

        get defaultGeoLocation(): IGeoLocalizacion {
            var zone: IGeoLocalizacion = {
                isLocal: true,
                countryCode: this.configuracion.peruCountryCode,
                zoneInfo: {}
            };
            return zone;
        }

        getCurrentPosition(): angular.IPromise<any> {
            var deferred = this.$q.defer();
            if (!this.$window.navigator.geolocation) {
                deferred.reject('Geolocation not supported');
            } else {
                this.$window.navigator.geolocation.getCurrentPosition((position) => {
                    deferred.resolve(position);
                }, (error) => {
                    deferred.reject(error);
                });
            }

            return deferred.promise;
        }

        private checkLocation(): angular.IPromise<any> {
            var deferred = this.$q.defer();
            this.getCurrentPosition().then((result) => {
                deferred.resolve(this.getZoneInformation({ latitude: result.coords.latitude, longitude: result.coords.longitude }));
            },
                (error) => deferred.reject(error));

            return deferred.promise;
        }

        /* https://developers.google.com/maps/documentation/geocoding/intro */
        private getZoneInformation(coordinates: any): angular.IPromise<IGoogleMapsApiResult> {
            var deferred = this.$q.defer();
            var parameters = {
                sensor: false,
                latlng: coordinates.latitude + ',' + coordinates.longitude
            };

            this.$http.get(this.configuracion.urlGoogleMapsGeocodeApi, { params: parameters }).then((response: angular.IHttpPromiseCallbackArg<any>) => {
                var country = response.data.results.filter((item: any) => this.googleLocationFilter(item));

                if (country.length >= 1) {
                    deferred.resolve(country[0]);
                } else {
                    deferred.reject('No countries');
                }
            }, (error) => deferred.reject(error));

            return deferred.promise;
        }

        private googleLocationFilter(value: any): boolean {
            return value.types.indexOf(this.googleCountryFilter) !== -1;
        }

        private get sessionGeolocation(): IGeoLocalizacion {
            return this.sessionFactory.geolocation;
        }

        private set sessionGeolocation(geolocation: IGeoLocalizacion) {
            this.sessionFactory.geolocation = geolocation;
        }
    }
}