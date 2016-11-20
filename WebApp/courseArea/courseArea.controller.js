(function () {
    'use strict';

    angular
        .module('app')
        .controller('CourseAreaController', CourseAreaController);

    CourseAreaController.$inject = ['$location',  'FlashService', '$rootScope'];
    function CourseAreaController($location,  FlashService, $rootScope) {
        var vm = this;
        vm.courseData ={};
        
        initController();
        function initController(){

            vm.courseData = $rootScope.currentCourseData;

        }
    }

})();