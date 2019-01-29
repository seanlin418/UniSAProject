'use strict'

myApp.factory("authService", ["$http", "$q", "localStorageService", "UniSAApiSettings", function ($http, $q, localStorageService, UniSAApiSettings) {

    var serviceBase = UniSAApiSettings.apiServiceBaseUri;

    var _authentication = {
        isAuth: false,
        userName: "",
        useRefreshTokens: false
    };


    var _saveRegistration = function (registration) {
        _logout();

        return $http.post(serviceBase + "api/account/register", registration)
            .then(function (response) {
                return response;
            });
                
    }; 

    var _login = function (loginData) {

        var dataString = "grant_type=password&username=" + loginData.userName + "&password=" + loginData.password;

        //if (loginData.useRefreshTokens) {
        //    dataString = dataString + "&client_id=" + UniSAApiSettings.clientId;
        //}

        dataString = dataString + "&client_id=" + UniSAApiSettings.clientId + "&Scope=" + loginData.email;

        //SeanLin: The whole reason of using q service is to reject the ErrorStatusCode = 400,401...
        //Otherwise, error code will not be rejected and will call onSuccess function,
        //REGARDLESS whether there is interceptor service to handle the errorResponse implementation!
        var _deferred = $q.defer();

        $http.post(serviceBase + 'token', dataString, { headers: { 'Content-Type': 'application/x-www-form-urlencoded' } })
            .then(function (response) {

                localStorageService.set('authorizationData', { token: response.data.access_token, userName: response.data.userName, refreshToken: response.data.refresh_token, useRefreshTokens: true });
                _authentication.isAuth = true;
                _authentication.userName = response.data.userName;
                _authentication.useRefreshTokens = loginData.useRefreshTokens;

                _deferred.resolve(response); 

            }, function (error) {
                _logout();
                _deferred.reject(error.data);
            });

        return _deferred.promise;
    }

    var _logout = function (keepData = false) {

        if (!keepData)
            localStorageService.remove('authorizationData');

        _authentication.isAuth = false;
        _authentication.userName = "";
        _authentication.useRefreshTokens = false;
    };

    var _fillAuthData = function () {

        var authData = localStorageService.get('authorizationData');
        if (authData) {
            _authentication.isAuth = true;
            _authentication.userName = authData.userName;
            _authentication.useRefreshTokens = authData.useRefreshTokens;
        }

    };

    var _obtainAccessToken = function (externalData) {

        $http.get(serviceBase + 'api/account/ObtainLocalAccessToken', { params: { provider: externalData.provider, externalAccessToken: externalData.externalAccessToken } })
            .then(function (response) {

                localStorageService.set('authorizationData', { token: response.access_token, userName: response.userName, refreshToken: "", useRefreshTokens: false });

                _authentication.isAuth = true;
                _authentication.userName = response.userName;
                _authentication.useRefreshTokens = false;

                return response;

            }, function (err, status) {
                _logout();
                return err;
            });

    };

    var _refreshToken = function () {
        var authData = localStorageService.get('authorizationData');


        //SeanLin: The whole reason of using q service is to reject the ErrorStatusCode = 400,401...
        //Otherwise, error code will not be rejected and will call onSuccess function,
        //REGARDLESS whether there is interceptor service to handle the errorResponse implementation!
        var _deferred = $q.defer();

        if (authData) {

            //if (authData.useRefreshTokens) {

            var data = "grant_type=refresh_token&refresh_token=" + authData.refreshToken + "&client_id=" + UniSAApiSettings.clientId;

            localStorageService.remove('authorizationData');

            $http.post(serviceBase + 'token', data, { headers: { 'Content-Type': 'application/x-www-form-urlencoded' } })
                .then(function (response) {

                    localStorageService.set('authorizationData', { token: response.access_token, userName: response.userName, refreshToken: response.refresh_token, useRefreshTokens: true });

                    _deferred.resolve(response);

                }, function (err, status) {
                    _logout();
                    _deferred.reject(err);
                });
            //}

            return _deferred.promise;
        }
    };

    return {
        authentication: _authentication,
        login : _login,
        logout: _logout,
        saveRegistration: _saveRegistration,
        fillAuthData: _fillAuthData,
        obtainAccessToken: _obtainAccessToken,
        refreshToken: _refreshToken
    }

}]);