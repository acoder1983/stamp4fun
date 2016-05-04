var app = angular.module('scottApp', []);
app.config(['$httpProvider', function($httpProvider) {
    $httpProvider.defaults.useXDomain = true;
    delete $httpProvider.defaults.headers.common['X-Requested-With'];
}]);
app.controller('clientControl', function($scope, $http) {
    if (typeof String.prototype.endsWith != 'function') {
        String.prototype.endsWith = function(str) {
            return this.slice(-str.length) == str;
        };
    }
    var result;
    $scope.query = function() {
        sendQueryReq();
    }
    $scope.qPrev = function() {
        $scope.searchKeys = result.prevSearch;
        sendQueryReq();
    }
    $scope.qNext = function() {
        $scope.searchKeys = result.nextSearch;
        sendQueryReq();
    }

    function sendQueryReq() {
        var url = "http://acoder1983.net:9000/scott/?search=" + $scope.searchKeys;
        $http.get(url).success(function(data, status, headers, config) {
            if (data.errMsg.length == 0) {
                if (result) {
                    data.pages = removeIdenticalPages(data.pages, result.pages);
                };
                displayResult(data.pages);
                result = data;
                runToTop();
            } else {
                alert(data.errMsg);
            }
        }).error(function(data, status, headers, config) {
            alert(data);
        });
    }

    function removeIdenticalPages(newPages, oldPages) {
        var n = [];
        for (var i = 0; i < newPages.length; i++) {
            var found = false;
            for (var j = 0; j < oldPages.length; j++) {
                if (newPages[i] == oldPages[j]) {
                    found = true;
                    break;
                };
            }
            if (!found) {
                n.push(newPages[i]);
            };
        }
        return n;
    }

    function runToTop() {
        window.scrollTo(0, 0);
    }

    function displayResult(imgList) {
        var imglistContent = "<imglist>";
        for (var i = 0; i < imgList.length; i++) {
            if (i % 4 == 0) {
                imglistContent += "<div class=\"row\">";
            };
            imglistContent += "<div class=\"col-md-3\"><img src=\"" + imgList[i] + "\" class=\"col-center-block\"/></div>";
            if (i % 4 == 3) {
                imglistContent += "<div style=\"margin:0;padding:0; width:100%;height:1px;background-color:#000000;overflow:hidden;margin-top: 15px;\
                " > < /div>";
                imglistContent += "</div>";
            };
        };
        if (!imglistContent.endsWith("</div></div>")) {
            imglistContent += "</div>";
        };
        imglistContent += "</imglist>";
        $('imglist').each(function() {
            $(this).replaceWith(imglistContent);
        });
    }
});
