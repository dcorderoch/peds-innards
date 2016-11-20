(function () {
    'use strict';

    angular
        .module('app')
        .controller('RegisterController', RegisterController);

    RegisterController.$inject = ['$location',  'FlashService', 'UserService','$rootScope', 'RegService'];
    function RegisterController($location,  FlashService, UserService, $rootScope, RegService) {
        var vm = this;

        vm.register=register;
        vm.repositories=['Google Drive', 'Dropbox'];
        vm.technologies=['Java', 'C++'];
        vm.universities=['Tecnológico de Costa Rica', 'Oxford University', 'Universidad Autónoma de Colombia']
        vm.countries=['Costa Rica', 'Nepal'];

        vm.languages = ['Español', 'Inglés', 'Francés', 'Portugués'];
        vm.regData={};
        // selected fruits
        vm.regData.Idiomas = [];
        vm.regData.Tecnologias = [];

        initController();
        function initController(){
            loadTechnologies();
            loadUniversities();
            loadCountries();
        }
        function loadTechnologies(){
            RegService.GetTechnologies()
                .then(function (response) {
                if (response.success) {
                    vm.technologies = response.data.Technologies;
                } 
            },function(response){
                console.log("supongo1")
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

        function loadUniversities(){
            RegService.GetUniversities()
                .then(function (response) {
                if (response.success) {
                    vm.universities = response.data.Universidades;
                } 
            },function(response){
                console.log("supongo3")
            });
        }

        // toggle selection for a given fruit by name
        vm.toggleSelectionLanguage = function toggleSelectionTech(language) {
            var idx = vm.regData.Idiomas.indexOf(language);

            // is currently selected
            if (idx > -1) {
                vm.regData.Idiomas.splice(idx, 1);
            }

            // is newly selected
            else {
                vm.regData.Idiomas.push(language);
            }
        };
        vm.toggleSelectionTech = function toggleSelectionTech(technology) {
            var idx = vm.regData.Tecnologias.indexOf(technology);

            // is currently selected
            if (idx > -1) {
                vm.regData.Tecnologias.splice(idx, 1);
            }

            // is newly selected
            else {
                vm.regData.Tecnologias.push(technology);
            }
        };



        function register() {
            console.log("entro");
            vm.regData.Carnet = vm.regData.Carnet.toString();
            vm.regData.Telefono = vm.regData.Telefono.toString();
            vm.dataLoading = true;
            UserService.RegisterStudent(vm.regData)
                .then(function (response) {
                if (response.success) {
                    FlashService.Success('Registration successful', true);
                    $location.path('/studentprofile');    
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
