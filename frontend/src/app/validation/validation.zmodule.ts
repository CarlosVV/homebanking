angular.module('appDinersClubOnline.validation', [])
    .directive('numbersOnly', () => new Validation.NumbersOnlyDirective())
    .directive('alfanumericoOnly', () => new Validation.AlfaNumericoOnlyDirective())
    .directive('emailValidacion', () => new Validation.EmailValidacionDirective())
    .directive('decimalOnly', () => new Validation.DecimalOnlyDirective())
    .directive('alfanumericoconEspeciales', () => new Validation.AlfaNumericoConEspecialesDirective())
    .directive('lettersOnly', () => new Validation.LettersOnlyDirective());