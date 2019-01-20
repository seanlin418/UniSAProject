'use strict'

myApp.controller('signUpController', ["$http", "$scope", "$timeout", "$location", "authService", function ($http, $scope, $timeout, $location, authService) {
    
    $scope.signUp = function (registration) {

        $scope.errors = [];
        authService.saveRegistration(registration)
            .then(function (response) {
                $scope.errors.push("User has been registered successfully, you will be redicted to login page in 2 seconds.");
                startTimer();

            }, function (response) {
                $scope.errors = response.data;
            });
    }

    var startTimer = function () {
        var timer = $timeout(function () {
            $timeout.cancel(timer);
            $location.path('/home');
        }, 2000);
    }
}]);

myApp.directive("compareTo", function () {
    return {
        require: "ngModel",
        scope: {
            otherModelValue: "=compareTo"
        },
        link: function (scope, element, attributes, ngModel) {

            ngModel.$validators.compareTo = function (modelValue) {
                return modelValue == scope.otherModelValue;
            };

            scope.$watch("otherModelValue", function () {
                ngModel.$validate();
            });
        }
    };
});