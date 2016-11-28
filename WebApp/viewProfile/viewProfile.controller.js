(function () {
    'use strict';

    angular
        .module('app')
        .controller('ViewProfileController', ViewProfileController);

    ViewProfileController.$inject = ['$location', 'FlashService',  '$rootScope', 'CourseService', 'UserService', 'ProfileCourseService' ];
    function ViewProfileController($location, FlashService, $rootScope, CourseService, UserService, ProfileCourseService) {
        var vm = this;

        vm.goCourseActive = goCourseActive;
        vm.goCourseFinished = goCourseFinished;
        vm.disableAccount =disableAccount;

        initController();

        function initController(){

            $rootScope.userData ={};
            vm.userData = $rootScope.userData;

            vm.courseAverageWidth = {'width': vm.userData.PromedioCursos+'%'};  
            vm.projectAverageWidth = {'width': vm.userData.PromedioProyectos+'%'};  


            ProfileCourseService.SetCourseData(currentCourseData);

        }




    }
})();
