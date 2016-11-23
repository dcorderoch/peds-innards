(function () {
    'use strict';

    angular
        .module('app')
        .controller('CourseOverviewController', CourseOverviewController);

    CourseOverviewController.$inject = ['$location',  'FlashService'];
    function CourseOverviewController($location,  FlashService) {
        var vm = this;

        initController();
        function initController(){

            vm.courseData = $rootScope.currentCourseData;

        }
    }

})();