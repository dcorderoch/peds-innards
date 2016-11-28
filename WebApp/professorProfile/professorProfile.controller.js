(function () {
    'use strict';

    angular
        .module('app')
        .controller('ProfessorProfileController', ProfessorProfileController);

    ProfessorProfileController.$inject = ['$location',  'FlashService', '$rootScope', 'CourseService', 'ProfileCourseService', 'UserService'];
    function ProfessorProfileController($location,  FlashService, $rootScope, CourseService, ProfileCourseService, UserService) {
        var vm = this;

        initController();

        vm.goCourseActive = goCourseActive;
        vm.goCourseFinished = goCourseFinished;
        vm.disableAccount =disableAccount;


        function initController(){

            $rootScope.userData ={};
            vm.userData = $rootScope.userData;

            vm.userData.Active = "0";
            vm.toggleEnable;
            if (vm.userData.Active == "0"){
                vm.toggleEnable = false;
            }
            if(vm.userData.Active == "1"){
                vm.toggleEnable = true;
            }

            var currentCourseData={
                "CourseName":"algo",
                "UniversityId":"algo",
                "MinGrade":70,
                "CourseId":"algo",
                "CourseDescription":"algo",
                "Group":8,
                "Students":[ { "Nombre":"algo", "StudentUserId":"123"},
                            {"Nombre":"algo","StudentUserId":"124"}],
                status :true

            };


            ProfileCourseService.SetCourseData(currentCourseData);

        }

        function goCourseFinished(id){

            $location.path('/courseoverview');    
            return;

            CourseService.GetCourseAsProfessor(id)
                .then(function(response){

                var currentCourseData = response.data;
                currentCourseData.status = false;

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