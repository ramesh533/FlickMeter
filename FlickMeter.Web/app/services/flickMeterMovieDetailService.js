app.factory('flickMeterMovieDetailService', function ($resource) {
    var requestUrl = "http://localhost/FlickMeter/api/movie/:id";
    return $resource(requestUrl, { id: '@id'});
});