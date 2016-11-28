(function () {
    'use strict';

    angular
        .module('app')
        .controller('CourseOverviewController', CourseOverviewController);

    CourseOverviewController.$inject = ['$location',  'FlashService', '$rootScope', 'ProfileCourseService', 'CourseService'];
    function CourseOverviewController($location,  FlashService, $rootScope, ProfileCourseService, CourseService) {

        var vm = this;
        vm.goArea = goArea;

        vm.studentList = [];

        initController();
        function initController(){

            vm.courseData =ProfileCourseService.GetCourseData();
            vm.userData = $rootScope.userData;

        }

        function goArea(studentId){


            var dataSend = {StudentUserId: studentId, ProfUserId: vm.userData.UserId, UniversityId: vm.courseData.UniversityId, 
                            Group: vm.courseData.Group, CourseId: vm.courseData.CourseId}
            console.log(dataSend);

            //Borrar
            vm.courseData.NombreContacto = "StudentMan";
            vm.courseData.ApellidoContacto = "Vergara";
            vm.courseData.Grade = "89";
            vm.courseData.StudentUserId = studentId;
            vm.courseData.Badges = [{BadgeDescription:"algo",Value:56, Alardeado:0},{BadgeDescription:"algo",Value:34,Alardeado:1}];
            
            ProfileCourseService.SetCourseData(vm.courseData);
            $location.path('/sharedareaprofessor');  


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


    }
})();
