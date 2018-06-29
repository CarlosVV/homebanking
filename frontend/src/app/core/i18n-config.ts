angular.module('appDinersClubOnline')
.config(function ($translateProvider: any, etiquetas: any) {
    // mas informacion sobre como usar la
    // libreria de i18n: https://angular-translate.github.io/docs/#/guide
    $translateProvider
    .useSanitizeValueStrategy('sanitizeParameters')
    .translations('en', etiquetas)
    .preferredLanguage('en');
});
