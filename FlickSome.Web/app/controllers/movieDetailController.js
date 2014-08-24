app.controller('movieDetailController', function ($scope, flickSomeMovieDetailService, $routeParams) {
    init();

    function init() {
        getMovie();
    }

    function getMovie() {
        $scope.id = $routeParams.id;
        flickSomeMovieDetailService.get({ id: $routeParams.id }, function (data) {            
            $scope.movie = data;
        });
    }
});