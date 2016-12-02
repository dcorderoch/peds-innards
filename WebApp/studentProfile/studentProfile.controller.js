(function () {
    'use strict';

    angular
        .module('app')
        .controller('StudentProfileController', StudentProfileController);

    StudentProfileController.$inject = ['$location', 'FlashService',  '$rootScope', 'CourseService', 'UserService', 'ProfileCourseService', '$localStorage' ];
    function StudentProfileController($location, FlashService, $rootScope, CourseService, UserService, ProfileCourseService, $localStorage) {
        var vm = this;

        vm.goCourseActive = goCourseActive;
        vm.goCourseFinished = goCourseFinished;
        vm.disableAccount =disableAccount;

        initController();

        function initController(){

            vm.userData = $rootScope.userData;

            if (vm.userData.TipoRepositorioArchivos == "0"){

                vm.userData.TipoRepositorioArchivos = "Google Drive"
            }
            if (vm.userData.TipoRepositorioArchivos == "1"){

                vm.userData.TipoRepositorioArchivos = "Dropbox"
            }

            vm.courseAverageWidth = {"width": vm.userData.PromedioCursos+"%"};  
            vm.projectAverageWidth = {"width": vm.userData.PromedioProyectos+"%"};  


            vm.photo = "data:image/jpg;base64," + $localStorage.Foto;


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

            CourseService.GetAllByStudent(vm.userData.UserId)
                .then(function(response){

                vm.userData.FinishedCoursesList = response.data.FinishedCourses;
                vm.userData.ActiveCoursesList = response.data.ActiveCourses;
                console.log(response);

            }, function(response){
                FlashService.Error("Error al traer los cursos del estudiante")
            });
        }

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
                console.log("no sirvio")
            });
        }

        function goCourseActive(id, status){

            var send= {StudentUserId: vm.userData.UserId, CourseId: id}

            CourseService.GetCourseAsStudent(send)
                .then(function(response){
                console.log("aqui abajo")
                console.log(response)
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
                console.log("no sirvio")
            });
        }

        function disableAccount(){

            console.log(vm.userData.UserId);
            console.log(vm.userData.Active);

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
