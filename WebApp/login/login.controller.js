(function () {
    'use strict';

    angular
        .module('app')
        .controller('LoginController', LoginController);

    LoginController.$inject = ['$location',  'FlashService', 'AuthenticationService', '$rootScope' ];
    function LoginController($location,  FlashService, AuthenticationService, $rootScope) {
        var vm = this;

        vm.login = login;
        vm.loginData={};

        initController();

        function initController(){
            $rootScope.userData = {};
            $rootScope.globals= {};
            $rootScope.currentCourseData = {};

            AuthenticationService.ClearCredentials();
        }

        function login(){

            var loginData = {UserName :vm.loginData.UserName, Password:sha256(vm.loginData.Password) }
            AuthenticationService.Login( vm.loginData)
                .then(function(response){

                console.log(response)
                if (response.data.UserTypeCode== "1" ){

                    FlashService.Success("Login exitoso");

                    AuthenticationService.LoginStudent(vm.loginData)
                        .then(function(response){
                        console.log(response);

                        AuthenticationService.SetCredentials( response.data.UserId, response.data.Password, 
                                                             response.data);    
                        $rootScope.userData= response.data;
                        $location.path('/studentprofile');    

                    },function(response){

                    })
                }
                if (response.data.UserTypeCode== "2" ){

                    FlashService.Success("Login exitoso");
                    AuthenticationService.LoginProfessor(vm.loginData)
                        .then(function(response){

                        var data = response.data;
                        AuthenticationService.SetCredentials( data.UserId, data.Password, 
                                                             data);    
                        $rootScope.userData= data;
                        $location.path('/professorprofile');    

                    },function(response){

                    }) 
                }
                if (response.data.UserTypeCode== "3" ){
                    FlashService.Success("Login exitoso");

                    AuthenticationService.LoginEmployer(vm.loginData)
                        .then(function(response){

                        AuthenticationService.SetCredentials( response.data.UserId, response.data.Password, 
                                                             response.data);    
                        $rootScope.userData= response.data;
                        $location.path('/employerprofile');    

                    },function(response){

                    })
                }
                else{
                    FlashService.Error("Usuario no existe");//errores
                    vm.dataLoading = false;
                }
            },function(response){
                console.log( vm.loginData);
                FlashService.Error("Usuario no existe");//errores
                vm.dataLoading = false;
            }); 
        }
    }

})();