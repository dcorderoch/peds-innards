(function () {
    'use strict';

    angular
        .module('app')
        .controller('Register2Controller', Register2Controller);

    Register2Controller.$inject = ['$location',  'FlashService', 'UserService','$rootScope'];
    function Register2Controller($location,  FlashService, UserService, $rootScope) {
        var vm = this;

        vm.register=register;
        vm.repositories=['Google Drive', 'Dropbox'];
        vm.universities=['Tecnológico de Costa Rica', 'Oxford University', 'Universidad Autónoma de Colombia']
        vm.regData={};
        // selected fruits

        function register() {
            console.log("entro");
            vm.regData.Telefono = vm.regData.Telefono.toString();
            vm.dataLoading = true;
            UserService.RegisterProfessor(vm.regData)
                .then(function (response) {
                if (response.success) {
                    FlashService.Success('Registration successful', true);
                    $location.path('/professorprofile');    
                    $rootScope.userData= response.data;
                } else {
                    FlashService.Error(response.message);
                    vm.dataLoading = false;
                }
            },function(response){
                console.log( vm.regData);
                FlashService.Error("Usuario no existe");//errores
                vm.dataLoading = false;
            });
        }
    }

})();