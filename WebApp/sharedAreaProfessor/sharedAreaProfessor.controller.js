(function () {
    'use strict';

    angular
        .module('app')
        .controller('SharedAreaProfessorController', SharedAreaProfessorController);

    SharedAreaProfessorController.$inject = ['$location',  'FlashService', '$rootScope' ];
    function SharedAreaProfessorController($location,  FlashService, $rootScope) {
        var vm = this;

        initController();
        function initController(){
            console.log($rootScope.currentCourseData);
            vm.courseData = $rootScope.currentCourseData;

        }
    }

})();