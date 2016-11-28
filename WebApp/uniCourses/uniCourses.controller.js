(function () {
    'use strict';

    angular
        .module('app')
        .controller('UniCoursesController', UniCoursesController);

    UniCoursesController.$inject = ['$location',  'FlashService', '$rootScope', 'CourseService', 'UserService'];
    function UniCoursesController($location,  FlashService, $rootScope, CourseService, UserService) {
        var vm = this;

        initController();

        vm.courses=[];
        vm.joinCourse = joinCourse;
        vm.disableAccount = disableAccount;

        function initController(){

            $rootScope.userData ={};
            vm.userData = $rootScope.userData;

            vm.courseAverageWidth = {'width': vm.userData.PromedioCursos+'%'};  
            vm.projectAverageWidth = {'width': vm.userData.PromedioProyectos+'%'};   


            vm.userData.Active = "0";
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

            CourseService.GetAllByUniversity(vm.UniversidadId)
                .then(function(response){

                vm.courses = response.data; 

            }, function(response){
                console.log("No funcó")
            });
        }

        function joinCourse( universityId ){

            var send = {StudentUserId: vm.StudentUserId, CourseId: vm.UniversityId}
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

