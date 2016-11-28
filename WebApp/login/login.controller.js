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
            console.log($rootScope.globals);
            $rootScope.currentCourseData = {};

            AuthenticationService.ClearCredentials();
        }

        function login(){
            console.log("entro")
//            $location.path('/employerprofile');    
//            AuthenticationService.SetCredentials(" response.data.userData.UserId", "response.data.userData.Password", 
//                                                 "response.data.userData");        
//            return;
            var loginData = {UserName :vm.loginData.UserName, Password:sha256(vm.loginData.Password) }
            AuthenticationService.Login( vm.loginData)
                .then(function(response){

                if (response.data.UserTypeCode=== "1" ){
                    FlashService.Success("Login exitoso");

                    AuthenticationService.loginStudent(vm.loginData)
                        .then(function(response){

                        AuthenticationService.SetCredentials( response.data.userData.UserId, response.data.userData.Password, 
                                                             response.data.userData);

                        $location.path('/studentprofile');    
                        $rootScope.userData= response.data;
                    },function(response){

                    })
                }
                if (response.data.UserTypeCode=== "2" ){
                    FlashService.Success("Login exitoso");

                    AuthenticationService.SetCredentials( response.data.userData.UserId, response.data.userData.Password, 
                                                         response.data.userData);

                    AuthenticationService.loginProfessor(vm.loginData)
                        .then(function(response){

                        $location.path('/professorprofile');    
                        $rootScope.userId= response.data.UserId;

                    },function(response){

                    }) 
                }
                if (response.data.UserTypeCode=== "3" ){
                    FlashService.Success("Login exitoso");

                    AuthenticationService.loginEmployer(vm.loginData)
                        .then(function(response){

                        AuthenticationService.SetCredentials( response.data.userData.UserId, response.data.userData.Password, 
                                                             response.data.userData);

                        $location.path('/employerprofile');    
                        $rootScope.userId= response.data.UserId;

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