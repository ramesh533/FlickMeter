app.controller('movieDetailController', function ($scope, flickMeterMovieDetailService, $routeParams) {
    init();

    function init() {
        getMovie();
    }

    function getMovie() {
        $scope.id = $routeParams.id;
        flickMeterMovieDetailService.get({ id: $routeParams.id }, function (data) {            
            $scope.movie = data;
        });
    }
});