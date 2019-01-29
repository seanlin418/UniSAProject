'user strict';
myApp.controller('indexController', ['$scope', '$location', '$http', 'authService', function ($scope, $location, $http, authService) {

    $scope.authentication = authService.authentication;

    $scope.logout = function () {
        authService.logout();
        $location.path('/Web');
    }

    $scope.authentication = authService.authentication;


    $scope.relocate = function () {
        $http.get(serviceBase + 'api/orders')
            .then(function (response) {
                $scope.debugStr = response.data;
            }, function (response) {
                $scope.debugStr = "Unauthorized!"
            });
    }

}]);