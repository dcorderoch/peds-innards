(function () {
    'use strict';

    angular
        .module('app')
        .controller('Register2Controller', Register2Controller);

    Register2Controller.$inject = ['$location',  'FlashService', 'UserService','$rootScope', 'RegService'];
    function Register2Controller($location,  FlashService, UserService, $rootScope,RegService) {
        var vm = this;

        vm.register=register;
        vm.repositories=['Google Drive', 'Dropbox'];
        vm.universities=['Tecnológico de Costa Rica', 'Oxford University', 'Universidad Autónoma de Colombia']
        vm.countries=['Costa Rica', 'Nepal'];

        vm.regData={};

        initController();
        function initController(){
            loadUniversities();
            loadCountries();
        }

        function loadUniversities(){
            RegService.GetUniversities()
                .then(function (response) {
                if (response.success) {
                    vm.universities = response.data.Universidades;
                } 
            },function(response){
                console.log("supongo")
            });
        }

        function loadCountries(){
            RegService.GetCountries()
                .then(function (response) {
                if (response.success) {
                    vm.countries = response.data.Country;
                } 
            },function(response){
                console.log("supongo2")
            });
        }

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