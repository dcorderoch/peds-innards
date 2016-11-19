(function () {
    'use strict';

    angular
        .module('app')
        .controller('LoginController', LoginController);

    LoginController.$inject = ['$location',  'FlashService', 'AuthenticationService' ];
    function LoginController($location,  FlashService, AuthenticationService) {
        var vm = this;

        vm.login = login;
        vm.loginData={};

        function login(){

            console.log(vm.loginData);
            AuthenticationService.login( vm.loginData)
                .then(function(response){

                if (response.data.UserTypeCode=== "0" ){
                    FlashService.Success("Login exitoso");

                    AuthenticationService.loginStudent(vm.loginData)
                        .then(function(response){

                    },function(response){

                    })
                }
                if (response.data.UserTypeCode=== "1" ){
                    FlashService.Success("Login exitoso");

                    AuthenticationService.loginStudent(vm.loginData)
                        .then(function(response){

                    },function(response){

                    }) 
                }
                if (response.data.UserTypeCode=== "2" ){
                    FlashService.Success("Login exitoso");

                    AuthenticationService.loginStudent(vm.loginData)
                        .then(function(response){

                    },function(response){

                    })
                }
                if (response.data.UserTypeCode=== "3" ){
                    FlashService.Success("Login exitoso");

                    AuthenticationService.loginStudent(vm.loginData)
                        .then(function(response){

                    },function(response){

                    })
                }
                else{
                    $location.path('/homeP');    
                    $rootScope.userId= response.data.UserId;
                    $rootScope.patientName = response.data.Name;
                    console.log($rootScope.userId);
                }
            },function(response){
                console.log( vm.loginData);
                FlashService.Error("Usuario no existe");//errores
                vm.dataLoading = false;
            }); 
        }


    })();