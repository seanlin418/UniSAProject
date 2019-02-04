myApp.directive("windowEventCatcher", ["$window", "$document", function ($window, $document) {


    var _link = function (scope, element, attrs) {

        //var win = angular.element($window);

        //win.bind('load', function () {

        //    var aa = 5;

        //});

        //var doc = angular.element($document);

        //doc.ready(function () {
        //    var bbv = 5;
        //});
            

    }


    return {
        restrict: 'E',
        link: _link
    };
}]);