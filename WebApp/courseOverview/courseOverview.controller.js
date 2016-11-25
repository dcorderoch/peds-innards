(function () {
    'use strict';

    angular
        .module('app')
        .controller('CourseOverviewController', CourseOverviewController);

    CourseOverviewController.$inject = ['$location',  'FlashService', '$rootScope', 'ProfileCourseService', 'CourseService'];
    function CourseOverviewController($location,  FlashService, $rootScope, ProfileCourseService) {

        var vm = this;
        vm.goArea = goArea;

        vm.studentList = [];
        
        initController();
        function initController(){

            vm.courseData = $rootScope.currentCourseData;

        }

        function goArea(carnet){

            $location.path('/sharedareaprofessor');  

        }
        
        function getCourseData(){
         
            
            
        }
    }

})();
