(function () {
    'use strict';

    angular
        .module('app')
        .controller('ProfessorProfileController', ProfessorProfileController);

    ProfessorProfileController.$inject = ['$location',  'FlashService', '$rootScope', 'CourseService', 'ProfileCourseService', 'UserService', '$localStorage'];
    function ProfessorProfileController($location,  FlashService, $rootScope, CourseService, ProfileCourseService, UserService, $localStorage) {
        var vm = this;

        initController();

        vm.goCourseActive = goCourseActive;
        vm.goCourseFinished = goCourseFinished;
        vm.disableAccount =disableAccount;


        function initController(){

            vm.userData = $rootScope.userData;

            if (vm.userData.TipoRepositorioArchivos == "0"){

                vm.userData.TipoRepositorioArchivos = "Google Drive"
            }
            if (vm.userData.TipoRepositorioArchivos == "1"){

                vm.userData.TipoRepositorioArchivos = "Dropbox"
            }


            vm.photo = "data:image/jpg;base64," + $localStorage.Foto
            console.log($localStorage);
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

        function loadCourses(){

            CourseService.GetAllByProfessor(vm.userData.UserId)
                .then(function(response){

                vm.userData.FinishedCoursesList = response.data.FinishedCourses;
                vm.userData.ActiveCoursesList = response.data.ActiveCourses;
                console.log(response);

            }, function(response){
                FlashService.Error("Error al traer los cursos de profesor")
            });
        }

        function goCourseFinished(id){

            $location.path('/courseoverview');    
            return;

            CourseService.GetCourseAsProfessor(id)
                .then(function(response){

                var currentCourseData = response.data;
                currentCourseData.status = false;
                console.log(currentCourseData )
                ProfileCourseService.SetCourseData(currentCourseData);
                $location.path('/courseoverview');    

            }, function(response){
                console.log("no sirvio")
            });
        }
        function goCourseActive(id, status){

            CourseService.GetCourseAsProfessor(id)
                .then(function(response){

                var currentCourseData = response.data;
                currentCourseData.status=true;
                console.log(currentCourseData )
                ProfileCourseService.SetCourseData(currentCourseData);
                $location.path('/courseoverview');    


            }, function(response){
                console.log("no sirvio")
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