const gulp = require('gulp');
const HubRegistry = require('gulp-hub');
const browserSync = require('browser-sync');
const environments = require('./conf/constants.js').environments;

const conf = require('./conf/gulp.conf');

// Load some files into the registry
const hub = new HubRegistry([conf.path.tasks('*.js')]);

// Tell gulp to use the tasks just loaded
gulp.registry(hub);

gulp.task('inject:local', gulp.series(gulp.parallel('styles', 'lint', 'scripts'), 'inject'));
gulp.task('inject:dist', gulp.series(gulp.parallel('styles:dist', 'lint', 'scripts'), 'inject'));

gulp.task('test', gulp.series('scripts', 'karma:single-run'));
gulp.task('test:auto', gulp.series('watch', 'karma:auto-run'));

environments.forEach(function(env) {
  gulp.task('build:' + env, gulp.series('partials', 'config:' + env, gulp.parallel('inject:dist', 'other'), 'build'));
});

gulp.task('default', gulp.series('clean', 'build:dist'));

// Launch a basic http server to test what will be deployed in local environment
gulp.task('serve:local', gulp.series('clean', 'build:local', 'browsersync:dist'));
// Launch a basic http server to test locally
gulp.task('serve', gulp.series('config:local', 'inject:local', 'watch', 'browsersync'));

gulp.task('watch', watch);

function reloadBrowserSync(cb) {
  browserSync.reload();
  cb();
}

function watch(done) {
  gulp.watch([
    conf.path.src('index.html'),
    'bower.json'
  ], gulp.parallel('inject:local'));

  gulp.watch(conf.path.src('app/**/*.html'), gulp.series('partials', reloadBrowserSync));
  gulp.watch([
    conf.path.src('**/*.scss'),
    conf.path.src('**/*.css')
  ], gulp.series('styles'));
  gulp.watch(conf.path.src('**/*.ts'), gulp.series('inject:local'));
  done();
}
