app.controller('flickMeterMovieController', function ($scope, flickMeterMovieService) {
    init();

    function init() {
        getMovies();
    }

    function getMovies() {
        flickMeterMovieService.query(function (data) {
            $scope.movies = data;
        });
    }
});