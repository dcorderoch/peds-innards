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
        }

        function login(){

            $rootScope.userData ={
                "NombreContacto":"Nicolas",
                "ApellidoContacto":"Jimenez",
                "Ubicacion":"Tres Ríos",
                "Email":"nicolas.j2007@gmail.com",
                "Telefono":"87715959",
                "Password":"crave",
                "TipoRepositorioArchivos":"https://www.google.com/",
                "Foto":"algo",
                "Universidad":"Tecnológico de Costa Rica",
                "HorarioAtencion":"8:00",
                "Fecha_Registro":"05/08/2014"
            };

            //            $rootScope.userData= {"NombreContacto":"Nicolas",
            //                                  "ApellidoContacto":"Jimenez",
            //                                  "Ubicacion":"Tres Ríos",
            //                                  "Email":"nicolas.j2007@gmail.com",
            //                                  "Telefono":"87715959",
            //                                  "Fecha_Registro":"05/08/2014",
            //                                  "Password":"crave",
            //                                  "TipoRepositorioArchivos":"Dropbox",
            //                                  "Foto":"bla ba",
            //                                  "Carnet":"201258421",
            //                                  "Universidad":"Tecnológico de Costa Rica",
            //                                  "EnlaceRepositorioCodigo":"https://www.google.com/",
            //                                  "EnlaceACurriculum":"https://www.google.com/",
            //                                  "PromedioProyectos":"98",
            //                                  "PromedioCursos":"99",
            //                                  "Idiomas":["Español","Inglés"],
            //                                  "CursosAprobados":"90",
            //                                  "CursosReprobados":"4",
            //                                  "ProyectosExitisos":"90",
            //                                  "ProyectosFallidos":"90",
            //                                  "Tecnologias":["Java","C++"]};

            AuthenticationService.SetCredentials( $rootScope.userData.Email, $rootScope.userData.Password, $rootScope.userData);
            console.log($rootScope.globals);
            $location.path('/professorprofile');    
            return;

            console.log(vm.loginData);
            AuthenticationService.Login( vm.loginData)
                .then(function(response){

                if (response.data.UserTypeCode=== "0" ){
                    FlashService.Success("Login exitoso");

                    AuthenticationService.loginStudent(vm.loginData)
                        .then(function(response){

                        $location.path('/studentprofile');    
                        $rootScope.userData= response.data;
                    },function(response){

                    })
                }
                if (response.data.UserTypeCode=== "1" ){
                    FlashService.Success("Login exitoso");

                    AuthenticationService.loginProfessor(vm.loginData)
                        .then(function(response){

                        $location.path('/professorprofile');    
                        $rootScope.userId= response.data.UserId;

                    },function(response){

                    }) 
                }
                if (response.data.UserTypeCode=== "2" ){
                    FlashService.Success("Login exitoso");

                    AuthenticationService.loginEmployer(vm.loginData)
                        .then(function(response){

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