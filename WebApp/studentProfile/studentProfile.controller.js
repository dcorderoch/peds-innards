(function () {
    'use strict';

    angular
        .module('app')
        .controller('StudentProfileController', StudentProfileController);

    StudentProfileController.$inject = ['$location', 'FlashService',  'CourseService', 'UserService', 'ProfileCourseService', '$localStorage' ];
    function StudentProfileController($location, FlashService, CourseService, UserService, ProfileCourseService, $localStorage) {
        var vm = this;

        vm.goCourseActive = goCourseActive;
        vm.goCourseFinished = goCourseFinished;
        vm.disableAccount =disableAccount;

        initController();

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

            vm.courseAverageWidth = {"width": vm.userData.PromedioCursos+"%"};  
            vm.projectAverageWidth = {"width": vm.userData.PromedioProyectos+"%"};  


            vm.photo = "data:image/jpg;base64," + $localStorage.Foto;

            console.log(vm.userData);
            vm.toggleEnable;
            if (vm.userData.Active == "0"){
                vm.toggleEnable = false;
            }
            if(vm.userData.Active == "1"){
                vm.toggleEnable = true;
            }

            loadCourses();
        }


        //loads all the students courses from server
        //activa and finished
        function loadCourses(){

            CourseService.GetAllByStudent(vm.userData.UserId)
                .then(function(response){

                vm.userData.FinishedCoursesList = response.data.FinishedCourses;
                vm.userData.ActiveCoursesList = response.data.ActiveCourses;

            }, function(response){
                FlashService.Error("Error al traer los cursos del estudiante")
            });
        }

        // Goes to a finished course, sets data in cookies
        // verifies the status (having or not a project)
        function goCourseFinished(id, status){


            var send= {StudentUserId: vm.userData.UserId, CourseId: id}

            CourseService.GetCourseAsStudent(send)
                .then(function(response){

                var currentCourseData = response.data;
                currentCourseData.status = false;

                ProfileCourseService.SetCourseData(currentCourseData);

                if(status===0){
                    FlashService.Error("Este curso finalizó sin propuesta de proyecto");  
                }
                else{
                    $location.path('/sharedarea');    
                }

            }, function(response){
            });
        }

        // Goes to a finished course, sets data in cookies
        // verifies the status (having or not a project)
        function goCourseActive(id, status){

            var send= {StudentUserId: vm.userData.UserId, CourseId: id}

            CourseService.GetCourseAsStudent(send)
                .then(function(response){

                var currentCourseData = response.data;
                currentCourseData.status=true;

                ProfileCourseService.SetCourseData(currentCourseData);

                if(status===0){
                    $location.path('/coursearea');  
                }
                else{
                    $location.path('/sharedarea');
                }

            }, function(response){
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
