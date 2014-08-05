var app = angular.module('FlickMeterApp', ['ngRoute', 'ngResource', 'ui.bootstrap', 'chieffancypants.loadingBar']);
app.config(function ($routeProvider) {

    $routeProvider.when("/latest", {
        controller: "movieListController",
        templateUrl: "app/views/latestMovies.html"
    });
    $routeProvider.when("/movie/:id", {
        controller: "movieDetailController",
        templateUrl: "app/views/movieDetail.html"
    });
    $routeProvider.when("/about", {
        templateUrl: "app/views/about.html"
    });
    $routeProvider.when("/upcoming", {
        templateUrl: "app/views/upcoming.html"
    });
    $routeProvider.otherwise({ redirectTo: "/latest" });

});