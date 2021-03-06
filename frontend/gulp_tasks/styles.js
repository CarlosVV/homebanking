const gulp = require('gulp');
const browserSync = require('browser-sync');
const wiredep = require('wiredep').stream;
const sourcemaps = require('gulp-sourcemaps');
const sass = require('gulp-sass');
const postcss = require('gulp-postcss');
const autoprefixer = require('autoprefixer');

const conf = require('../conf/gulp.conf');

gulp.task('styles', styles);
gulp.task('styles:dist', stylesDist);

function styles() {
  return gulp.src(conf.path.src('index.scss'))
    .pipe(sourcemaps.init())
    .pipe(wiredep())
    .pipe(sass({ outputStyle: 'expanded' })).on('error', conf.errorHandler('Sass'))
    .pipe(postcss([autoprefixer()])).on('error', conf.errorHandler('Autoprefixer'))
    .pipe(sourcemaps.write())
    .pipe(gulp.dest(conf.path.tmp()))
    .pipe(browserSync.stream());
}

function stylesDist() {
  return gulp.src(conf.path.src('index.scss'))
    .pipe(wiredep())
    .pipe(sass({ outputStyle: 'expanded' })).on('error', conf.errorHandler('Sass'))
    .pipe(postcss([autoprefixer()])).on('error', conf.errorHandler('Autoprefixer'))
    .pipe(gulp.dest(conf.path.tmp()));
}
