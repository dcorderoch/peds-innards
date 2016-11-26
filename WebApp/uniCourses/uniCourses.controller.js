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

            vm.userData = $rootScope.userData;

            vm.courseAverageWidth = {'width': vm.userData.PromedioCursos+'%'};  
            vm.projectAverageWidth = {'width': vm.userData.PromedioProyectos+'%'};   


            $rootScope.currentCourseData={};
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

            UserService.Disable(vm.UserId)
                .then(function(response){

                FlashService.Success("Cuenta deshabilitada");
                $location.path("/login")

            }, function(response){
                console.log("no funcó");
            })
        }

    }

})();

