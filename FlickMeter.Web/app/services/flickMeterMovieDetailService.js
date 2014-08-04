app.factory('flickMeterMovieDetailService', function ($resource) {
    var requestUrl = "http://localhost/FlickMeterService/api/movie/:id";
    return $resource(requestUrl, { id: '@id'});
});