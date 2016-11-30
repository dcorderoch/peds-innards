(function () {
    'use strict';

    angular
        .module('app')
        .controller('UniCoursesController', UniCoursesController);

    UniCoursesController.$inject = ['$location',  'FlashService', '$rootScope', 'CourseService', 'UserService', '$localStorage'];
    function UniCoursesController($location,  FlashService, $rootScope, CourseService, UserService, $localStorage) {
        var vm = this;

        initController();

        vm.courses=[];
        vm.joinCourse = joinCourse;
        vm.disableAccount = disableAccount;

        function initController(){

            vm.userData = $rootScope.userData;

            if (vm.userData.TipoRepositorioArchivos == "0"){

                vm.userData.TipoRepositorioArchivos = "Google Drive"
            }
            if (vm.userData.TipoRepositorioArchivos == "1"){

                vm.userData.TipoRepositorioArchivos = "Dropbox"
            }

            vm.courseAverageWidth = {'width': vm.userData.PromedioCursos+'%'};  
            vm.projectAverageWidth = {'width': vm.userData.PromedioProyectos+'%'};   


            vm.photo = "data:image/jpg;base64," + $localStorage.Foto;
            console.log($localStorage);
            
            vm.toggleEnable;
            if (vm.userData.Active == "0"){
                vm.toggleEnable = false;
            }
            if(vm.userData.Active == "1"){
                vm.toggleEnable = true;
            }

            loadCourses();
        }

        function loadCourses(){

            console.log(vm.userData.UniversityId );
            CourseService.GetAllByUniversity(vm.userData.UniversityId)

                .then(function(response){

                console.log( response );
                vm.courses = response.data; 

            }, function(response){
                FlashService.Error("No se pudieron traer los cursos de la universidad");
                console.log("No funcó")
            });
        }

        function joinCourse( CourseId ){

            var send = {StudentUserId: vm.userData.StudentUserId, CourseId: CourseId}
            CourseService.JoinCourse( send)
                .then( function(response){

                if (response.ReturnStatus==1)
                    FlashService.Success("Te has unido exitosamente a este curso");
                loadCourses();

            }, function(response){

                FlashService.Error("No te has podido unir exitosamente a este curso");
                console.log("No sirvió el unirse a curso"); 
            });
        }

        function disableAccount(){

            console.log(vm.userData.userId);
            console.log(vm.userData.Active);

            UserService.Disable(vm.userData.UserId)
                .then(function(response){

                if (vm.userData.Active == "1"){
                    if (response.data.ReturnStatus == "1"){ 

                        FlashService.Success("La cuenta ha sido deshabilitada");
                        $location.path("/login")

                    }
                    else{
                        FlashService.Error("No se pudo deshabilitar la cuenta");
                    }
                }
                if (vm.userData.Active == "0"){

                    if (response.data.ReturnStatus == "1"){ 

                        FlashService.Success("Cuenta habilitada");
                        vm.toggleEnable =true;
                        vm.userData.Active = "1";
                    }
                    else{
                        FlashService.Error("No se pudo habilitar la cuenta");
                    }
                }

            }, function(response){
                if (vm.userData.Active == "0"){ 
                    FlashService.Error("No se pudo habilitar la cuenta");
                } 
                if (vm.userData.Active == "1"){ 
                    FlashService.Error("No se pudo deshabilitar la cuenta");
                }

                console.log("no funcó");
            })
        }

    }

})();

