app.controller('movieListController', function ($scope, flickMeterMovieService) {
    $scope.totalRecordsCount = 0;
    $scope.pageSize = 10;
    $scope.currentPage = 1;

    init();

    function init() {
        getMovies();
    }

    function getMovies() {
        flickMeterMovieService.get({ page: $scope.currentPage, pageSize: $scope.pageSize }, function (data) {
            $scope.totalRecordsCount = data.count;
            $scope.movies = data.movies;
        });
    }

    $scope.pageChanged = function (page) {
        $scope.currentPage = page;
        getMovies();
    };
});