(function () {
    'use strict';

    angular
        .module('app')
        .controller('SharedAreaController', SharedAreaController);

    SharedAreaController.$inject = ['$location',  'FlashService', '$rootScope'];
    function SharedAreaController($location,  FlashService, $rootScope) {
        var vm = this;

        initController();
        function initController(){
            
            console.log($rootScope.currentCourseData);
            vm.courseData = $rootScope.currentCourseData;

        }
    }

})();