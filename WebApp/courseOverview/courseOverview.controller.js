(function () {
    'use strict';

    angular
        .module('app')
        .controller('CourseOverviewController', CourseOverviewController);

    CourseOverviewController.$inject = ['$location',  'FlashService', '$rootScope'];
    function CourseOverviewController($location,  FlashService, $rootScope) {

        var vm = this;
        vm.goArea = goArea;

        initController();
        function initController(){

            vm.courseData = $rootScope.currentCourseData;

        }

        function goArea(carnet){

            $location.path('/coursearea');  

        }
    }

})();

