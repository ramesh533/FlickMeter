app.factory('flickSomeMovieService', function ($resource) {
    var requestUrl = "http://localhost/FlickSome/api/movies/:page/:pageSize";
    return $resource(requestUrl, { page: '@page', pageSize: '@pageSize'});
});