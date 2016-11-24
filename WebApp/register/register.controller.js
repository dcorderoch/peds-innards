(function () {
    'use strict';

    angular
        .module('app')
        .controller('RegisterController', RegisterController);

    RegisterController.$inject = ['$location',  'FlashService', 'UserService','$rootScope', 'RegService'];
    function RegisterController($location,  FlashService, UserService, $rootScope, RegService) {
        var vm = this;

        vm.register=register;
        vm.repositories=[{tipo:'Google Drive', id:1}, {tipo:'Dropbox',id:0}];
        
        vm.technologies=[{ technology:"Java",technologyid: "0"}, { technology:"C++",technologyid: "1"}];
        
        vm.universities=[{university:'Tecnológico de Costa Rica',universityid:0}, {university:'Oxford University' ,universityid:1}, {university:'Universidad Autónoma de Colombia',universityid:2}];
        
        vm.countries=[ {"country":"costa rica","countryid": "1"}, {"country":"nicaragua","countryid": "2"}];
        
        vm.languages = [{language:"Español",languageid: "0"}, {language:"Inglés",languageid: "1"}];
        
        vm.regData={};
        // selected fruits
        vm.regData.Idiomas = [];
        vm.regData.Tecnologias = [];

        initController();
        function initController(){
            loadTechnologies();
            loadUniversities();
            loadCountries();
            loadLanguages();
        }

        function loadLanguages(){
            RegService.GetLanguages()
                .then(function (response) {
                if (response.success) {
                    vm.languages = response.data.languages;
                } 
            },function(response){
                console.log("supongo4")
            });
        }       

        function loadTechnologies(){
            RegService.GetTechnologies()
                .then(function (response) {
                if (response.success) {
                    vm.technologies = response.data.technologies;
                } 
            },function(response){
                console.log("supongo1")
            });
        }

        function loadCountries(){
            RegService.GetCountries()
                .then(function (response) {
                if (response.success) {
                    vm.countries = response.data.countries;
                } 
            },function(response){
                console.log("supongo2")
            });
        }

        function loadUniversities(){
            RegService.GetUniversities()
                .then(function (response) {
                if (response.success) {
                    vm.universities = response.data.universities;
                } 
            },function(response){
                console.log("supongo3")
            });
        }

        // toggle selection for a given fruit by name
        vm.toggleSelectionLanguage = function toggleSelectionTech(language) {
            var idx = vm.regData.Idiomas.indexOf(language.languageid);

            // is currently selected
            if (idx > -1) {
                vm.regData.Idiomas.splice(idx, 1);
            }

            // is newly selected
            else {
                    vm.regData.Idiomas.push(language.languageid);
            }
        };
        
        vm.toggleSelectionTech = function toggleSelectionTech(technology) {
            var idx = vm.regData.Tecnologias.indexOf(technology.technologyid);

            // is currently selected
            if (idx > -1) {
                vm.regData.Tecnologias.splice(idx, 1);
            }

            // is newly selected
            else {
                vm.regData.Tecnologias.push(technology.technologyid);
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
