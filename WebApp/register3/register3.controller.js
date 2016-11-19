(function () {
    'use strict';

    angular
        .module('app')
        .controller('Register3Controller', Register3Controller);

    Register3Controller.$inject = ['$location',  'FlashService', 'UserService'];
    function Register3Controller($location,  FlashService, UserService) {
        var vm = this;

        vm.register=register;
        vm.repositories=['Google Drive', 'Dropbox'];
        vm.regData={};

        function register() {
            console.log("entro");
            vm.regData.Telefono = vm.regData.Telefono.toString();
            vm.dataLoading = true;
            UserService.RegisterEmployer(vm.regData)
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