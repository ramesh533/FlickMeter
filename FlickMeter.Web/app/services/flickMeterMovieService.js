app.factory('flickMeterMovieService', function ($resource) {
    var requestUrl = "http://localhost/FlickMeter/api/movies/:page/:pageSize";
    return $resource(requestUrl, { page: '@page', pageSize: '@pageSize'});
});