'user strict';
myApp.controller('indexController', ['$scope', '$location', '$http', 'authService', function ($scope, $location, $http, authService) {

    $scope.$on('$routeChangeStart', function ($event, next, current) {

    });


    $scope.authentication = authService.authentication;

    $scope.logout = function () {
        authService.logout();
        $location.path('/Web');
    }

    $scope.authentication = authService.authentication;

}]);