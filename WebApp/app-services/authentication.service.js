(function () {
    'use strict';

    angular
        .module('app')//El modulo es app, el cual hace referencia 
        .factory('AuthenticationService', AuthenticationService);

    AuthenticationService.$inject = ['$http', '$cookieStore', '$rootScope', '$localStorage',  'ProfileCourseService'];
    function AuthenticationService($http, $cookieStore, $rootScope, $localStorage, ProfileCourseService) {
        var service = {};

        service.Login = Login;
        service.SetCredentials = SetCredentials; //funciones que este servicio presenta
        service.ClearCredentials = ClearCredentials; //para el usuario global
        service.LoginStudent = LoginStudent;
        service.LoginProfessor = LoginProfessor;
        service.LoginEmployer = LoginEmployer;


        return service;


        /**
        * Description for Login, First step login to server
        * If it works the the second stage (specific login) is executed
        * @private
        * @method Login
        * @param {Object} loginData, JSON to send to the server
        * @return {Object} description Response of the server
        */
        function Login(loginData){
            var request = $http({
                method:"post",
                url:    $rootScope.url+"Login/Login",
                data:loginData
            }); 
            return request;
        }

        /**
        * Description for LoginStudent, Second stage login for student only
        * @private
        * @method LoginStudent
        * @param {Object} loginData JSON to send to the server
        * @return {Object} description Response of the server
        */
        function LoginStudent(loginData)  {
            var request = $http({
                method:"post",
                url:    $rootScope.url+"Login/LoginStudent",
                data:loginData
            });
            return request;
        }

        /**
        * Description for LoginProfessor Second stage login for professor only
        * @private
        * @method LoginProfessor
        * @param {Object} loginData loginData JSON to send to the server
        * @return {Object} description Response of the server
        */
        function LoginProfessor(loginData)  { 
            var request = $http({
                method:"post",
                url:    $rootScope.url+"Login/LoginProfessor",
                data:loginData
            });
            return request;
        }

        /**
        * Description for LoginEmployer Second stage login for employer only
        * @private
        * @method LoginEmployer
        * @param {Object} loginData JSON to send to the server
        * @return {Object} description Response from the server
        */
        function LoginEmployer(loginData) {
            console.log(loginData);
            var request = $http({
                method:"post",
                url:    $rootScope.url+"Login/LoginEmployer",
                data:loginData
            });
            return request;
        }

        /**
        * Description for SetCredentialsn Saves auth data for the user
        * @private
        * @method SetCredentials
        * @param {Object} Id    username of the user
        * @param {Object} Password  user's password
        * @param {Object} data  data capted on login
        * @return {Object} description  
        */
        function SetCredentials(Id, Password, data) {   

            var authdata = (Id + ':' + Password);
            console.log(authdata);
            $rootScope.globals = {
                currentUser: {
                    Id: Id,
                    authdata: authdata
                }
            };

            $http.defaults.headers.common['Authorization'] = 'Basic ' + authdata; 
            $cookieStore.put('globals', $rootScope.globals);
            $cookieStore.put('dataLogin',data);
        }

        /**
        * Description for ClearCredentials Clears suth data, this function is called
        * on login, so that the app won't store data after a logout 
        * @private
        * @method ClearCredentials
        * @return {Object} description
        */
        function ClearCredentials() {   // cuando se deslogea el usuariofunction ClearCredentials() {   // cuando se deslogea el usuario

            $rootScope.globals = {};
            $rootScope.userId = "";
            $cookieStore.remove('globals');
            $localStorage.$reset();
            $http.defaults.headers.common.Authorization = 'Basic';
            ProfileCourseService.SetProfileData({});   
            ProfileCourseService.SetProfileData2({});   
        }





        //Para no perder datos, no hay que cambiar nada aqui
        // Base64 encoding service used by AuthenticationService
        var Base64 = {

            keyStr: 'ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/=',

            encode: function (input) {
                var output = "";
                var chr1, chr2, chr3 = "";
                var enc1, enc2, enc3, enc4 = "";
                var i = 0;

                do {
                    chr1 = input.charCodeAt(i++);
                    chr2 = input.charCodeAt(i++);
                    chr3 = input.charCodeAt(i++);

                    enc1 = chr1 >> 2;
                    enc2 = ((chr1 & 3) << 4) | (chr2 >> 4);
                    enc3 = ((chr2 & 15) << 2) | (chr3 >> 6);
                    enc4 = chr3 & 63;

                    if (isNaN(chr2)) {
                        enc3 = enc4 = 64;
                    } else if (isNaN(chr3)) {
                        enc4 = 64;
                    }

                    output = output +
                        this.keyStr.charAt(enc1) +
                        this.keyStr.charAt(enc2) +
                        this.keyStr.charAt(enc3) +
                        this.keyStr.charAt(enc4);
                    chr1 = chr2 = chr3 = "";
                    enc1 = enc2 = enc3 = enc4 = "";
                } while (i < input.length);

                return output;
            },

            decode: function (input) {
                var output = "";
                var chr1, chr2, chr3 = "";
                var enc1, enc2, enc3, enc4 = "";
                var i = 0;

                // remove all characters that are not A-Z, a-z, 0-9, +, /, or =
                var base64test = /[^A-Za-z0-9\+\/\=]/g;
                if (base64test.exec(input)) {
                    window.alert("There were invalid base64 characters in the input text.\n" +
                                 "Valid base64 characters are A-Z, a-z, 0-9, '+', '/',and '='\n" +
                                 "Expect errors in decoding.");
                }
                input = input.replace(/[^A-Za-z0-9\+\/\=]/g, "");

                do {
                    enc1 = this.keyStr.indexOf(input.charAt(i++));
                    enc2 = this.keyStr.indexOf(input.charAt(i++));
                    enc3 = this.keyStr.indexOf(input.charAt(i++));
                    enc4 = this.keyStr.indexOf(input.charAt(i++));

                    chr1 = (enc1 << 2) | (enc2 >> 4);
                    chr2 = ((enc2 & 15) << 4) | (enc3 >> 2);
                    chr3 = ((enc3 & 3) << 6) | enc4;

                    output = output + String.fromCharCode(chr1);

                    if (enc3 != 64) {
                        output = output + String.fromCharCode(chr2);
                    }
                    if (enc4 != 64) {
                        output = output + String.fromCharCode(chr3);
                    }

                    chr1 = chr2 = chr3 = "";
                    enc1 = enc2 = enc3 = enc4 = "";

                } while (i < input.length);

                return output;
            }
        };

    }
})();