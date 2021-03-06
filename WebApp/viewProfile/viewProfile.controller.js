(function () {
    'use strict';

    angular
        .module('app')
        .controller('ViewProfileController', ViewProfileController);

    ViewProfileController.$inject = ['$location', 'FlashService',   'CourseService', 'UserService', 'ProfileCourseService', '$localStorage' ];
    function ViewProfileController($location, FlashService, CourseService, UserService, ProfileCourseService, $localStorage) {
        var vm = this;


        initController();

        
        //Toggles an account, if successful it will redirect to login or will disable 
        //all interaction. or will do the contrary if it was disabled.
        function initController(){

            vm.userData = ProfileCourseService.GetProfileData2();
            console.log(vm.userData)

            vm.photo = "data:image/jpg;base64," + $localStorage.Foto2;

            if (vm.userData.TipoRepositorioArchivos == "0"){

                vm.userData.TipoRepositorioArchivos = "Google Drive"
            }
            if (vm.userData.TipoRepositorioArchivos == "1"){

                vm.userData.TipoRepositorioArchivos = "Dropbox"
            }            

            vm.courseAverageWidth = {'width': vm.userData.PromedioCursos+'%'};  
            vm.projectAverageWidth = {'width': vm.userData.PromedioProyectos+'%'};  

            console.log(vm.photo);


        }




    }
})();
