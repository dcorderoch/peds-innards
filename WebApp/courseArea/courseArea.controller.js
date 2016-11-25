(function () {
    'use strict';

    angular
        .module('app')
        .controller('CourseAreaController', CourseAreaController);

    CourseAreaController.$inject = ['$location',  'FlashService', '$rootScope', 'CourseService', 'ProfileCourseService'];
    function CourseAreaController($location,  FlashService, $rootScope, CourseService, ProfileCourseService) {
        var vm = this;
        
        
        
        initController();
        function initController(){

            vm.courseData =ProfileCourseService.GetCourseData();
            vm.courseData.Carnet = $rootScope.userData.Carnet;
            //            vm.courseData.status=true;
            vm.gradeWidth = {'width': vm.courseData.Grade+'%'};  
            console.log(vm.gradeWidth);
            console.log( vm.courseData);
        }
    
    
    
    
    }
})();