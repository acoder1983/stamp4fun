var app = angular.module('scottApp', []);
app.config(['$httpProvider', function($httpProvider) {
    $httpProvider.defaults.useXDomain = true;
    delete $httpProvider.defaults.headers.common['X-Requested-With'];
}]);
app.controller('clientControl', function($scope, $http) {
    if (typeof String.prototype.endsWith != 'function') {
       String.prototype.endsWith = function (str){
          return this.slice(-str.length) == str;
       };
     }
    // $scope.scrwidth = document.body.clientWidth;
    $scope.query = function() {
        // app.imglistContent = '<imglist><a href=\"' + $scope.queryKey + '\"> Click me to go </a></imglist>';
        // $('imglist').each(function() {
        //     $(this).replaceWith(app.imglistContent);    
        // });
        var config = {
            url: "http://localhost:8080/SpringMVC/movie/" + $scope.searchKeys,
            method: "get"
        };
        $http.get(config.url).success(function(data, status, headers, config) {
            $scope.result = data;
        }).error(function(data, status, headers, config) {
            $scope.result = "error: " + data;
        });
    }

    function displayResult (imgList) {
        var imglistContent="<imglist>";
        for (var i = 0; i < imglist.length; i++) {
            if (i%4==0) {
                imglistContent+="<div class=\"row\"";
            };
            imglistContent+="<div class=\"col-md-3\"><img src=\""+imgList[i]+"\" class=\"col-center-block\"/></div>";
            if (i%4==3) {
                imglistContent+="</div>";
            };
        };
        if (!imglistContent.endsWith("</div></div>")) {
            imglistContent+="</div>";
        };
        imglistContent+="</imglist>";
        $('imglist').each(function() {
            $(this).replaceWith(imglistContent);    
        });
    }
});

