var app = angular.module('scottApp', []);
app.config(['$httpProvider', function($httpProvider) {
    $httpProvider.defaults.useXDomain = true;
    delete $httpProvider.defaults.headers.common['X-Requested-With'];
}]);
app.controller('clientControl', function($scope, $http) {
    // $scope.scrwidth = document.body.clientWidth;
    $scope.query = function() {
        // app.imglistContent = '<imglist><a href=\"' + $scope.queryKey + '\"> Click me to go </a></imglist>';
        // $('imglist').each(function() {
        //     $(this).replaceWith(app.imglistContent);    
        // });
        var config = {
            url: "http://localhost:8080/greeting?name=" + $scope.searchKeys,
            method: "get"
        };
        $http.get(config.url).success(function(data, status, headers, config) {
            $scope.result = data;
        }).error(function(data, status, headers, config) {
            $scope.result = "error: " + data;
        });
    }
});