app.factory('flickSomeMovieDetailService', function ($resource) {
    var requestUrl = "http://localhost/FlickSome/api/movie/:id";
    return $resource(requestUrl, { id: '@id'});
});