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

            vm.userData = $rootScope.userData;
            
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

            UserService.Disable(vm.UserId)
                .then(function(response){

                if (response.data.ReturnStatus == "1"){ 

                    FlashService.Success("Cuenta deshabilitada");
                    $location.path("/login")
                }
                else{
                    FlashService.Error("No se pudo cerrar la cuenta");
                }
                
            }, function(response){
                console.log("no funcó");
            })
        }
        
    }
})();