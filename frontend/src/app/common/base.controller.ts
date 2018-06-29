namespace Common {
    export class BaseController {
        constructor(protected $rootScope: angular.IRootScopeService) {
        }

        resetTimer(): void {
            this.$rootScope.$broadcast('restaurarTimer', { message: 'start timer' });
        }
    }
}