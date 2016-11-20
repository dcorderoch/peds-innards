(function () {
    'use strict';

    angular
        .module('app')
        .controller('SharedAreaEmployerController', SharedAreaEmployerController);

    SharedAreaEmployerController.$inject = ['$location',  'FlashService', '$scope', '$rootScope'];
    function SharedAreaEmployerController($location,  FlashService, $scope, $rootScope) {
        var vm = this;

        initController();
        function initController(){

            vm.courseData = $rootScope.currentCourseData;

        }
    }

})();