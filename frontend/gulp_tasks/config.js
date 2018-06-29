const gulp = require('gulp');
const conf = require('../conf/gulp.conf');
const ngConstant = require('gulp-ng-constant');
const constants = require('../conf/constants.js').constants;
const environments = require('../conf/constants.js').environments;

environments.forEach(function(env) {
    gulp.task('config:' + env, configBase.bind(this, env));
});

function configBase(env) {
    return ngConstant({
        name: 'appDinersClubOnline.config',
        constants: constants[env],
        wrap: false,
        stream: true
      })
      .pipe(gulp.dest(conf.path.tmp()));
}
