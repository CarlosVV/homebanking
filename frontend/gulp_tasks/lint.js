const gulp = require('gulp');
const tslint = require('gulp-tslint');

gulp.task('lint', scripts);

function scripts() {
    return gulp.src([
        "src/**/*.ts"
    ])
    .pipe(tslint({
        formatter: "verbose"
    }))
    .pipe(tslint.report({
        summarizeFailureOutput: true
    }));
}