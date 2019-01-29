'use strict'
myApp.controller("loginController", ["$scope", "$location", "authService", "$http", "localStorageService", function ($scope, $location, authService, $http, localStorageService) {

    $scope.inputData = {
        emailOrUserName: "",
        password: ""
    }

    var init = function () {
        var authData = localStorageService.get('authorizationData');
        $scope.refreshTokenByPass = false;

        if (authData) {
            $scope.inputData.emailOrUserName = authData.userName;
            $scope.refreshTokenByPass = true;
        } else {
            $scope.refreshTokenByPass = false;
        }

    };

    init();

    var _loginData = {
        userName: "",
        email: "",
        password: "",
        useRefreshTokens: true
    };

    function _validateEmail(email) {
        //var regex = /^(([^<>()[\]\\.,;:\s@\"]+(\.[^<>()[\]\\.,;:\s@\"]+)*)|(\".+\"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
        var regex = /^[a-zA-Z0-9'._%+-]+@[a-zA-Z0-9-][a-zA-Z0-9.-]*\.[a-zA-Z]{2,9}$/;
        return regex.test(email);

    }

    $scope.login = function () {

        if ($scope.refreshTokenByPass) {
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

        if (_validateEmail($scope.inputData.emailOrUserName)) {
            _loginData.email = $scope.inputData.emailOrUserName;
        } else {
            _loginData.userName = $scope.inputData.emailOrUserName;
        }
        _loginData.password = $scope.inputData.password;    

        authService.login(_loginData).then(function (response) {
            $location.path("/Web");
        }, function (error) {
            $scope.message = error.error_description;
        });
    }

}]);