'use strict'
myApp.controller("loginController", ["$scope", "$location", "authService", "$http", "localStorageService", function ($scope, $location, authService, $http, localStorageService) {

    $scope.input = {
        emailOrUserName: "", password: ""
    }

    var init = function () {
        var authData = localStorageService.get('authorizationData');
        $scope.isRefreshTokensAvailable = false;

        if (authData) {
            $scope.input.emailOrUserName = authData.userName;
            $scope.isRefreshTokensAvailable = true;
        } else {
            $scope.isRefreshTokensAvailable = false;
        }

    };

    init();

    var _loginData = {
        userName: "",
        email: "",
        password: ""
    };

    function _validateEmail(email) {
        //var regex = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
        var regex = /^[a-zA-Z0-9'._%+-]+@[a-zA-Z0-9-][a-zA-Z0-9.-]*\.[a-zA-Z]{2,9}$/;
        return regex.test(email);

    }

    $scope.login = function () {

        if ($scope.isRefreshTokensAvailable) {
            authService.refreshToken().then(function (response) {
                $location.path("/Web");
            },
            function (err) {
                $scope.message = error.error_description;
                authService.logout();
                $location.path('/login');
            });

            return;
        }

        _loginData = { userName: "", email: "", password: ""}

        if (_validateEmail($scope.input.emailOrUserName)) {
            _loginData.email = $scope.input.emailOrUserName;
        } else {
            _loginData.userName = $scope.input.emailOrUserName;
        }
        _loginData.password = $scope.input.password;    

        authService.login(_loginData).then(function (response) {
            $location.path("/Web");
        }, function (error) {
            $scope.message = error.error_description;
        });
    }

}]);