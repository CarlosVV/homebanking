const gulp = require('gulp');
const typescript = require('gulp-typescript');
const sourcemaps = require('gulp-sourcemaps');
const conf = require('../conf/gulp.conf');

gulp.task('scripts', scripts);

function scripts() {
    var project = typescript.createProject('tsconfig.json');
    return gulp.src([
        "src/**/*.js",
        "src/**/*.ts",
        "typings/**/*.d.ts"
    ])
        .pipe(sourcemaps.init())
        .pipe(project())
        .pipe(sourcemaps.write('.'))
        .pipe(gulp.dest(conf.path.tmp()));
}