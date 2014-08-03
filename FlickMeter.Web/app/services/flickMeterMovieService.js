app.factory('flickMeterMovieService', function ($resource) {
    var requestUrl = "http://localhost/FlickMeterService/api/movies/:page";
    return $resource(requestUrl);
});