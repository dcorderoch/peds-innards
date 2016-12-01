(function () {
    'use strict';

    angular
        .module('app')
        .controller('LoginController', LoginController);

    LoginController.$inject = ['$location',  'FlashService', 'AuthenticationService', '$rootScope', '$localStorage' ];
    function LoginController($location,  FlashService, AuthenticationService, $rootScope, $localStorage) {
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

                if (response.data.UserTypeCode== "1" ){

                    FlashService.Success("Login exitoso");

                    AuthenticationService.LoginStudent(vm.loginData)
                        .then(function(response){
                        console.log(response);

                        var userFoto = $localStorage.$default({
                            Foto : response.data.Foto
                        });

                        var data = response.data;
                        delete data.Foto;

                        AuthenticationService.SetCredentials( data.UserId, data.Password, 
                                                             data);    
                        $rootScope.userData= data;
                        $location.path('/studentprofile');    

                    },function(response){

                    })
                }
                if (response.data.UserTypeCode== "2" ){

                    FlashService.Success("Login exitoso");
                    AuthenticationService.LoginProfessor(vm.loginData)
                        .then(function(response){

                        vm.userFoto = $localStorage.$default({
                            Foto : response.data.Foto
                        });

                        var data = response.data;
                        delete data.Foto;

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

                        vm.userFoto = $localStorage.$default({
                            Foto : response.data.Foto
                        });

                        var data = response.data;
                        delete data.Foto;

                        AuthenticationService.SetCredentials( data.UserId, data.Password, 
                                                             data);    
                        $rootScope.userData= data;
                        $location.path('/employerprofile');    

                    },function(response){

                    })
                }
                if (response.data.UserTypeCode =="-1"){
                    vm.dataLoading = false;
                    FlashService.Error("Usuario no existe");//errores
                }
                else{
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