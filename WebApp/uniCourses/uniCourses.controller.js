(function () {
    'use strict';

    angular
        .module('app')
        .controller('UniCoursesController', UniCoursesController);

    UniCoursesController.$inject = ['$location',  'FlashService', 'CourseService', 'UserService', '$localStorage', 'ProfileCourseService'];
    function UniCoursesController($location,  FlashService,  CourseService, UserService, $localStorage, ProfileCourseService) {
        var vm = this;

        initController();

        vm.courses=[];
        vm.joinCourse = joinCourse;
        vm.disableAccount = disableAccount;

        //Toggles an account, if successful it will redirect to login or will disable 
        //all interaction. or will do the contrary if it was disabled.
        function initController(){

            vm.userData = ProfileCourseService.GetProfileData();

            if (vm.userData.TipoRepositorioArchivos == "0"){

                vm.userData.TipoRepositorioArchivos = "Google Drive"
            }
            if (vm.userData.TipoRepositorioArchivos == "1"){

                vm.userData.TipoRepositorioArchivos = "Dropbox"
            }

            vm.courseAverageWidth = {'width': vm.userData.PromedioCursos+'%'};  
            vm.projectAverageWidth = {'width': vm.userData.PromedioProyectos+'%'};   


            vm.photo = "data:image/jpg;base64," + $localStorage.Foto;
            console.log(ProfileCourseService.GetProfileData());

            vm.toggleEnable;
            if (vm.userData.Active == "0"){
                vm.toggleEnable = false;
            }
            if(vm.userData.Active == "1"){
                vm.toggleEnable = true;
            }

            loadCourses();
        }

        //loads all the university courses
        // marks the finished courses as unable to join.
        function loadCourses(){

            CourseService.GetAllByUniversity(vm.userData.UniversityId)

                .then(function(response){

                vm.courses = response.data; 

            }, function(response){
                FlashService.Error("No se pudieron traer los cursos de la universidad");
            });
        }

        //
        function joinCourse( CourseId ){

            var send = {StudentUserId: vm.userData.UserId, CourseId: CourseId}
            CourseService.JoinCourse( send)
                .then( function(response){

                if (response.data.ReturnStatus =="-1"){

                    FlashService.Success("Ya estás unido a este curso")
                }
                if (response.data.ReturnStatus=="1"){ 
                    FlashService.Success("Te has unido exitosamente a este curso");
                }
                if (response.data.ReturnStatus =="0"){

                    FlashService.Error("Ha habido un error, y no se pudo unirte a este curso");
                }
                loadCourses();

            }, function(response){

                FlashService.Error("Ha habido un error, y no se pudo unirte a este curso");
            });
        }

        //Toggles an account, if successful it will redirect to login or will disable 
        //all interaction. or will do the contrary if it was disabled.
        function disableAccount(){


            UserService.Disable(vm.userData.UserId)
                .then(function(response){

                if (vm.userData.Active == "1"){
                    if (response.data.ReturnStatus == "1"){ 

                        FlashService.Success("La cuenta ha sido deshabilitada",true);
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
                        ProfileCourseService.SetProfileData(vm.userData);

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

            })
        }

    }

})();

