(function () {
    'use strict';

    angular
        .module('app')
        .controller('ViewProfileController', ViewProfileController);

    ViewProfileController.$inject = ['$location', 'FlashService',  '$rootScope', 'CourseService', 'UserService', 'ProfileCourseService', '$localStorage' ];
    function ViewProfileController($location, FlashService, $rootScope, CourseService, UserService, ProfileCourseService, $localStorage) {
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


            vm.photo = "data:image/jpg;base64," + $localStorage.Foto;
            console.log($localStorage);

            ProfileCourseService.SetCourseData(currentCourseData);

        }




    }
})();
