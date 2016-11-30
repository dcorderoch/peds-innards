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

            vm.userData = $rootScope.userData;

            if (vm.userData.TipoRepositorioArchivos == "0"){

                vm.userData.TipoRepositorioArchivos = "Google Drive"
            }
            if (vm.userData.TipoRepositorioArchivos == "1"){

                vm.userData.TipoRepositorioArchivos = "Dropbox"
            }            

            vm.courseAverageWidth = {'width': vm.userData.PromedioCursos+'%'};  
            vm.projectAverageWidth = {'width': vm.userData.PromedioProyectos+'%'};  


            ProfileCourseService.SetCourseData(currentCourseData);

        }




    }
})();
