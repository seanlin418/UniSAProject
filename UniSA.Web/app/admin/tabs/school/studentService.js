'use strict'

myApp.factory("studentService", ["$http", "$q", "UniSAApiSettings", function ($http, $q, UniSAApiSettings) {

    var _deferred = $q.defer();

    var _add = function (registerVm) {

        $http.post(serviceBase + 'api/students/register').then(function (results) {

            _deferred.resolv(results.data);
        }, function (results) {

            _deferred.reject(results.data);
        });

        return _deferred.promise;
    };

}]);
