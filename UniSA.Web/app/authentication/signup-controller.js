'use strict'

myApp.controller('signUpController', ["$scope", "$http", "$timeout", "$location", "authService", function ($scope, $http, $timeout, $location, authService) {

    var vm = this;

    vm.isProcessing = false;

    vm.signUp = function (registration) {

        $scope.errors = [];
        $scope.isProcessing = true;

        authService.saveRegistration(registration)
            .then(function (response) {
                $scope.errors.push("User has been registered successfully, you will be redicted to login page in 2 seconds.");
                startTimer();
            }, function (response) {
                if (response.data.ModelState != null) {
                    if (response.data.ModelState["innerExceptions"].length != 0) {
                        $scope.errors = response.data.ModelState["innerExceptions"];
                    };
                };
                $scope.isProcessing = false;
            });
    }

    var startTimer = function () {
        var timer = $timeout(function () {
            $timeout.cancel(timer);
            $location.path('/home');
        }, 2000);
    }


    vm.genderOptions = { "Male": 0, "Female": 1, "Others": 2 };
    Object.freeze(vm.genderOptions);

    vm.onGenderChanged = function (selectedGender) {

        if (selectedGender == vm.genderOptions.Male) {
            var aa = 5;
        } else if (selectedGender == vm.genderOptions.Female) {
            var aa = 5;
        } else if (selectedGender == vm.genderOptions.Others) {
            var aa = 5;
        }

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