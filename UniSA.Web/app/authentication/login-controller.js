'use strict'
myApp.controller("loginController", ["$scope", "$location", "authService", function ($scope, $location, authService) {
    $scope.login = function (user) {

        if (user.useRefreshTokens == null) user.useRefreshTokens = false; 

        authService.login(user).then(function (response) {
            $location.path("/order");
        }, function (error) {
            $scope.message = error.error_description;
        });
    }
}]);