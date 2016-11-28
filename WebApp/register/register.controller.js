(function () {
    'use strict';

    angular
        .module('app')
        .controller('RegisterController', RegisterController);

    RegisterController.$inject = ['$location',  'FlashService', 'UserService','$rootScope', 'RegService'];
    function RegisterController($location,  FlashService, UserService, $rootScope, RegService) {
        var vm = this;

        vm.register=register;
        vm.repositories=[{tipo:'Google Drive', id:"0"}, {tipo:'Dropbox',id:"1"}];

        vm.regData={};
        // selected fruits
        vm.regData.Idiomas = [];
        vm.regData.Tecnologias = [];

        initController();
        function initController(){
            vm.countries;
            loadTechnologies();
            loadUniversities();
            loadCountries();
            loadLanguages();
        }

        function loadLanguages(){
            RegService.GetLanguages()
                .then(function (response) {

                console.log(response.data)
                vm.languages = response.data;

            },function(response){
                console.log("supongo4")
            });
        }       

        function loadTechnologies(){
            RegService.GetTechnologies()
                .then(function (response) {

                console.log(response.data)
                vm.technologies = response.data;

            },function(response){
                console.log("supongo1")
            });
        }

        function loadCountries(){
            RegService.GetCountries()
                .then(function (response) {

                console.log(response.data)
                vm.countries = response.data;

            },function(response){
                console.log("supongo2")
            });
        }

        function loadUniversities(){
            RegService.GetUniversities()
                .then(function (response) {
                vm.universities = response.data;
                console.log(response.data)

            },function(response){
                console.log("supongo3")
            });
        }

        // toggle selection for a given fruit by name
        vm.toggleSelectionLanguage = function toggleSelectionTech(language) {
            var idx = vm.regData.Idiomas.indexOf(language.LanguageId);

            // is currently selected
            if (idx > -1) {
                vm.regData.Idiomas.splice(idx, 1);
            }

            // is newly selected
            else {
                vm.regData.Idiomas.push(language.LanguageId);
            }
        };

        vm.toggleSelectionTech = function toggleSelectionTech(technology) {
            var idx = vm.regData.Tecnologias.indexOf(technology.TechnologyId);

            // is currently selected
            if (idx > -1) {
                vm.regData.Tecnologias.splice(idx, 1);
            }

            // is newly selected
            else {
                vm.regData.Tecnologias.push(technology.TechnologyId);
            }
        };


        function register() {

            vm.regData.Carnet = vm.regData.Carnet.toString();
            vm.regData.Telefono = vm.regData.Telefono.toString();
            
//            vm.regData.TipoRepositorioArchivos = (vm.regData.TipoRepositorioArchivos == "Google Drive") ? "0" : "1"

            if (vm.regData.hasOwnProperty("Foto")){
                vm.regData.Foto =  "data:image/jpg;base64,"+vm.regData.Foto.base64
            }
            
            if (!vm.regData.hasOwnProperty("Foto")){
                vm.regData.Foto =  "";
            }
            
            if (!vm.regData.hasOwnProperty("EnlaceRepositorioCodigo")){
                vm.regData.EnlaceRepositorioCodigo = "";
            }
            
            if (!vm.regData.hasOwnProperty("EnlaceACurriculum")){
                vm.regData.EnlaceACurriculum = "";
            }


            var pass = vm.regData.Password ;
            vm.regData.Password = sha256(vm.regData.Password);
            
            console.log(vm.regData);
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
                FlashService.Error("Registro fallido");//errores
                vm.dataLoading = false;
            });
            vm.regData.Password = pass;
            console.log(vm.regData)

        }


    }
})();
