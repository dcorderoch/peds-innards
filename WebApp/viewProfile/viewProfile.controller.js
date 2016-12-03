(function () {
    'use strict';

    angular
        .module('app')
        .controller('ViewProfileController', ViewProfileController);

    ViewProfileController.$inject = ['$location', 'FlashService',   'CourseService', 'UserService', 'ProfileCourseService', '$localStorage' ];
    function ViewProfileController($location, FlashService, CourseService, UserService, ProfileCourseService, $localStorage) {
        var vm = this;



        initController();

        function initController(){

            vm.userData = ProfileCourseService.GetProfileData2();
            console.log(vm.userData)

            if (vm.userData.TipoRepositorioArchivos == "0"){

                vm.userData.TipoRepositorioArchivos = "Google Drive"
            }
            if (vm.userData.TipoRepositorioArchivos == "1"){

                vm.userData.TipoRepositorioArchivos = "Dropbox"
            }            

            vm.courseAverageWidth = {'width': vm.userData.PromedioCursos+'%'};  
            vm.projectAverageWidth = {'width': vm.userData.PromedioProyectos+'%'};  


            vm.photo = "data:image/jpg;base64," + $localStorage.Foto2;
            console.log($localStorage);


        }




    }
})();
