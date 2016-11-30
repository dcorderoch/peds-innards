(function () {
    'use strict';

    angular
        .module('app')
        .controller('CourseOverviewController', CourseOverviewController);

    CourseOverviewController.$inject = ['$location',  'FlashService', '$rootScope', 'ProfileCourseService', 'CourseService'];
    function CourseOverviewController($location,  FlashService, $rootScope, ProfileCourseService, CourseService) {

        var vm = this;
        vm.goArea = goArea;
        vm.finishCourse= finishCourse;

        vm.studentList = [];

        initController();
        function initController(){

            vm.courseData =ProfileCourseService.GetCourseData();
            vm.userData = $rootScope.userData;
            console.log(vm.courseData);
        }

        function goArea(studentId){


            var dataSend = {StudentUserId: studentId, ProfUserId: vm.userData.UserId, UniversityId: vm.courseData.UniversityId, 
                            Group: vm.courseData.Group, CourseId: vm.courseData.CourseId}
            console.log(dataSend);

            CourseService.GetSpecificCourse(dataSend)
                .then(function(response){

                vm.courseData.NombreContacto = response.data.NombreContacto;
                vm.courseData.ApellidoContacto = response.data.ApellidoContacto;
                vm.courseData.Grade = response.data.Grade;
                vm.courseData.Badges = response.data.Badges;
                vm.courseData.StudentUserId = studentId;
                ProfileCourseService.SetCourseData(vm.courseData);
                $location.path('/sharedareaprofessor');  

            }, function(response){
                console.log("no sirvió")
            })
        }
        
        function finishCourse(){
            
            CourseService.CloseCourse(vm.courseData.CourseId)
                .then( function(response){
                
                FlashService.Success("Curso cerrado exitosamente", true);
                $location.path('/professorprofile')
                
            }, function(response){
               
                FlashService.Error("No se pudo cerrar el curso, intentalo más tarde")
            });
            
        }


    }
})();
