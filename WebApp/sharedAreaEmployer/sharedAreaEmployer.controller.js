(function () {
    'use strict';

    angular
        .module('app')
        .controller('SharedAreaEmployerController', SharedAreaEmployerController);

    SharedAreaEmployerController.$inject = ['$location',  'FlashService', '$scope', '$rootScope', 'ProfileCourseService'];
    function SharedAreaEmployerController($location,  FlashService, $scope, $rootScope, ProfileCourseService) {
        var vm = this;

        initController();
        
        vm.comments =[];
        vm.sendReply = sendReply;
        
        
        function initController(){

            vm.courseData = $rootScope.currentCourseData;

        }
    }

})();