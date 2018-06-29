angular.module('appDinersClubOnline.timer', [])
    .controller('timer', ['sessionFactory', '$timeout', '$rootScope', 'configuracion', Timer.TimerController]);