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
        }

        function login(){

            //            $rootScope.userData ={"NombreContacto":"Nicolas",
            //                                  "ApellidoContacto":"Jimenez",
            //                                  "Ubicacion":"Tres Ríos",
            //                                  "Email":"nicolas.j2007@gmail.com",
            //                                  "Telefono":"87715959",
            //                                  "Fecha_Registro":"05/08/2014",
            //                                  "Password":"algo",
            //                                  "TipoRepositorioArchivos":"algo",
            //                                  "Foto":"algo",
            //                                  "IdEmpleador":"algo",
            //                                  "NombreEmpresarial":"algo",
            //                                  "EnlaceSitioWeb":"algo",
            //                                  "ListaProyectosTerminados":["algo","algo"],
            //                                  "ListaProyectosActivos":["algo","algo"]};

            //            $rootScope.userData ={
            //                "NombreContacto":"Nicolas",
            //                "ApellidoContacto":"Jimenez",
            //                "Ubicacion":"Tres Ríos",
            //                "Email":"nicolas.j2007@gmail.com",
            //                "Telefono":"87715959",
            //                "Password":"crave",
            //                "TipoRepositorioArchivos":"https://www.google.com/",
            //                "Foto":"algo",
            //                "Universidad":"Tecnológico de Costa Rica",
            //                "HorarioAtencion":"8:00",
            //                "Fecha_Registro":"05/08/2014"
            //            };

            $rootScope.userData= {"NombreContacto":"Nicolas",
                                  "ApellidoContacto":"Jimenez",
                                  "Ubicacion":"Tres Ríos",
                                  "Email":"nicolas.j2007@gmail.com",
                                  "Telefono":"87715959",
                                  "Fecha_Registro":"05/08/2014",
                                  "Password":"crave",
                                  "TipoRepositorioArchivos":"Dropbox",
                                  "Foto":"bla ba",
                                  "Carnet":"201258421",
                                  "Universidad":"Tecnológico de Costa Rica",
                                  "EnlaceRepositorioCodigo":"https://www.google.com/",
                                  "EnlaceACurriculum":"https://www.google.com/",
                                  "PromedioProyectos":"86",
                                  "PromedioCursos":"79",
                                  "Idiomas":["Español","Inglés"],
                                  "CursosAprobados":"90",
                                  "CursosReprobados":"4",
                                  "ProyectosExitisos":"90",
                                  "ProyectosFallidos":"90",
                                  "Tecnologias":["Java","C++"],
                                  FinishedCoursesList: [{courseid:"1", course:"curse1"}],
                                  ActiveCoursesList: [{courseid:"1", course:"curse1", accepted:"0", CourseDescription:"algo"}],
                                  FinishedProjectsList:[{projectid:"1", project:"proy"}],
                                  ActiveProjectsList:[{projectid:"1", project:"proy2"}]
                                 };

            AuthenticationService.SetCredentials( $rootScope.userData.Email, $rootScope.userData.Password, $rootScope.userData);
            console.log($rootScope.globals);
            $location.path('/studentprofile');    
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