namespace Usuario {
    export class FacebookPictureFactory {
        constructor(private $http: angular.IHttpService, private $window: angular.IWindowService, private $q: angular.IQService, private configuracion: any) {
        }

        asyncInit() {
            FB.init({
                appId: this.configuracion.facebookAppId,
                status: true,
                cookie: true,
                xfbml: true,
                version: 'v2.7'
            });
        }

        checkStatus() {
            var deferred = this.$q.defer();

            FB.getLoginStatus(
                (response: any) => {
                    this.statusChangeCallback(response).then(response => {
                        deferred.resolve(response);
                    },
                        (reason) => {
                            deferred.reject(reason);
                        });
                }
            );

            return deferred.promise;
        }

        verifyConnectionStatus(): angular.IPromise<any> {
            var deferred = this.$q.defer();

            FB.getLoginStatus((response: any) => {
                deferred.resolve(response);
            }, true);

            return deferred.promise;
        }

        statusChangeCallback(response: any): angular.IPromise<any> {
            var deferred = this.$q.defer();
            if (response.status === 'connected') {
                deferred.resolve(this.getFacebookInfo());
            } else if (response.status === 'not_authorized') {
                deferred.resolve(this.logInWithFacebook());
            } else {
                deferred.resolve(this.logInWithFacebook());
            }

            return deferred.promise;
        }

        logInWithFacebook() {
            var deferred = this.$q.defer();
            FB.login((response: any) => {
                if (response.authResponse) {
                    deferred.resolve(this.getFacebookInfo());
                } else {
                    deferred.reject('User cancelled login or did not fully authorize');
                }
            });

            return deferred.promise;
        };

        getFacebookInfo() {
            var deferredInfo = this.$q.defer();
            FB.api('/me', {
                fields: 'id, name, last_name'
            },
                (response) => {
                    if (!response || response.error) {
                        if (response.error.code === 190) {
                            deferredInfo.resolve(this.checkStatus());
                        } else {
                            deferredInfo.reject('Error ocurred fetching facebok data');
                        }
                    } else {
                        deferredInfo.resolve(response);
                    }
                });

            return deferredInfo.promise;
        }

        // feature for logOut
        reconnectFacebook() {
            var deferred = this.$q.defer();
            FB.logout((response: any) => {
                if (!response || response.error) {
                    deferred.reject(response);
                } else {
                    deferred.resolve(this.logInWithFacebook());
                }
            });

            return deferred.promise;
        }
    }
}
