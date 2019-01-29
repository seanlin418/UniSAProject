'use strict';

var myApp = angular.module('myApp', ['ngRoute', 'LocalStorageModule'])
    .config(function ($routeProvider) {

        $routeProvider.when('/', {
            templateUrl: '/app/home/home.html',
            controller: 'homeController'
        });

        $routeProvider.when('/login', {
            templateUrl: '/app/authentication/login.html',
            controller: 'loginController'
        });

        $routeProvider.when('/signup', {
            templateUrl: '/app/authentication/signup.html',
            controller: 'signUpController'
        });

    }).config(['$locationProvider', function ($locationProvider) {
        $locationProvider.hashPrefix('');
    }]);


var serviceBase = 'http://localhost:8000/';
myApp.constant('UniSAApiSettings', {
    apiServiceBaseUri: serviceBase,
    clientId: 'UniSA.WebApp'
});

myApp.run(['authService', function (authService) {
    authService.fillAuthData();
}]);


myApp.config(['$qProvider', function ($qProvider) {
    $qProvider.errorOnUnhandledRejections(false);
}]);


myApp.factory("authInterceptorService", ["$q", "$injector", "$location", "localStorageService", function ($q, $injector, $location, localStorageService) {

    var authInterceptorServiceFactory = {};

    return {
        request: function (config) {
            config.headers = config.headers || {};

            var authData = localStorageService.get('authorizationData');
            if (authData) {
                config.headers.Authorization = 'Bearer ' + authData.token;
            }

            return config;
        },
        requestError: function (rejection) {
            return $q.reject(rejection);
        },
        response: function (response) {
            return response;
        },
        responseError: function (rejection) {

            if (rejection.status === 401) {
                var authService = $injector.get('authService');
                var authData = localStorageService.get('authorizationData');

                if (!authData) {
                    authService.logout(false);
                } else {
                    authService.logout(true);
                }
                
                $location.path('/login');
            }
            return $q.reject(rejection);
        }
    }

}]);

myApp.config(['$httpProvider', function ($httpProvider) {
    $httpProvider.interceptors.push('authInterceptorService');
}]);