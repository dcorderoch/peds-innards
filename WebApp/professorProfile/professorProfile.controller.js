(function () {
    'use strict';

    angular
        .module('app')
        .controller('ProfessorProfileController', ProfessorProfileController);

    ProfessorProfileController.$inject = ['$location',  'FlashService',  'CourseService', 'ProfileCourseService', 'UserService', '$localStorage'];
    function ProfessorProfileController($location,  FlashService,  CourseService, ProfileCourseService, UserService, $localStorage) {
        var vm = this;

        initController();

        vm.goCourseActive = goCourseActive;
        vm.goCourseFinished = goCourseFinished;
        vm.disableAccount =disableAccount;


        function initController(){

            vm.userData = ProfileCourseService.GetProfileData();

            if (vm.userData.TipoRepositorioArchivos == "0"){

                vm.userData.TipoRepositorioArchivos = "Google Drive"
            }
            if (vm.userData.TipoRepositorioArchivos == "1"){

                vm.userData.TipoRepositorioArchivos = "Dropbox"
            }


            vm.photo = "data:image/jpg;base64," + $localStorage.Foto

            console.log(vm.userData);


            if (vm.userData.Active == "0"){
                vm.toggleEnable = false;
            }
            if(vm.userData.Active == "1"){
                vm.toggleEnable = true;
            }

            loadCourses();
        }

        function loadCourses(){

            CourseService.GetAllByProfessor(vm.userData.UserId)
                .then(function(response){

                vm.userData.FinishedCoursesList = response.data.FinishedCourses;
                vm.userData.ActiveCoursesList = response.data.ActiveCourses;

            }, function(response){
                FlashService.Error("Error al traer los cursos de profesor")
            });
        }

        function goCourseFinished(course){


            CourseService.GetCourseAsProfessor(course.CourseId)
                .then(function(response){

                var currentCourseData = response.data;
                currentCourseData.status = false;
                currentCourseData.Accepted = course.Accepted;
                ProfileCourseService.SetCourseData(currentCourseData);


                $location.path('/courseoverview');    

            }, function(response){
            });
        }
        function goCourseActive(course){

            CourseService.GetCourseAsProfessor(course.CourseId)
                .then(function(response){

                var currentCourseData = response.data;
                currentCourseData.status=true;
                ProfileCourseService.SetCourseData(currentCourseData);
                $location.path('/courseoverview');    


            }, function(response){
            });
        }

        function disableAccount(){



            UserService.Disable(vm.userData.UserId)
                .then(function(response){

                if (vm.userData.Active == "1"){
                    if (response.data.ReturnStatus == "1"){ 

                        FlashService.Success("La cuenta ha sido deshabilitada",true);
                        $location.path('/login');

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