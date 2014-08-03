var app = angular.module('FlickMeterApp', ['ngRoute', 'ngResource', 'ui.bootstrap']);
app.config(function ($routeProvider) {

    $routeProvider.when("/latest", {
        controller: "flickMeterMovieController",
        templateUrl: "app/views/latestMovies.html"
    });
    $routeProvider.otherwise({ redirectTo: "/latest" });

});