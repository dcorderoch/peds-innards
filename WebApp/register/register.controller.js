(function () {
    'use strict';

    angular
        .module('app')
        .controller('RegisterController', RegisterController);

    RegisterController.$inject = ['$location',  'FlashService', 'UserService', 'RegService', 'AuthenticationService', '$localStorage', 'ProfileCourseService'];
    function RegisterController($location,  FlashService, UserService, RegService,  AuthenticationService, $localStorage, ProfileCourseService) {
        var vm = this;

        vm.register=register;
        vm.repositories=[{tipo:'Google Drive', id:"0"}, {tipo:'Dropbox',id:"1"}];

        vm.regData={};
        // selected fruits
        vm.regData.Idiomas = [];
        vm.regData.Tecnologias = [];


        initController();

        // Loads all necessary for register
        function initController(){
            vm.countries;
            loadTechnologies();
            loadUniversities();
            loadCountries();
            loadLanguages();
        }

        //loads languages from server 
        function loadLanguages(){
            RegService.GetLanguages()
                .then(function (response) {

                vm.languages = response.data;

            },function(response){
            });
        }       

        //loads technologies from server 
        function loadTechnologies(){
            RegService.GetTechnologies()
                .then(function (response) {

                vm.technologies = response.data;

            },function(response){
            });
        }

        //loads countries from server 
        function loadCountries(){
            RegService.GetCountries()
                .then(function (response) {

                vm.countries = response.data;

            },function(response){
            });
        }

        //loads universities from server
        function loadUniversities(){
            RegService.GetUniversities()
                .then(function (response) {
                vm.universities = response.data;

            },function(response){
            });
        }

        // toggle selection for a given language
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

        // toggle selection for a given technology
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

        //Register for a student, if successful redirects to profile
        // transforms and validate fields if neccessary
        function register() {

            var oFile = document.getElementById("fileUpload").files[0]; // <input type="file" id="fileUpload" accept=".jpg,.png,.gif,.jpeg"/>

            if (oFile.size > 2097152) // 2 mb for bytes.
            {
                alert("El tamaño de la foto excede el tamaño máximo de 2mb");
                return;
            }

            vm.regData.Telefono = vm.regData.Telefono.toString();

            //            vm.regData.TipoRepositorioArchivos = (vm.regData.TipoRepositorioArchivos == "Google Drive") ? "0" : "1"

            if (vm.regData.hasOwnProperty("Foto")){
                vm.regData.Foto =  vm.regData.Foto.base64;
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

            //            var pass = vm.regData.Password ;
            //            vm.regData.Password = sha256(vm.regData.Password);

            vm.dataLoading = true;
            UserService.RegisterStudent(vm.regData)
                .then(function (response) {

                FlashService.Success('Registro exitoso', true);


                var userFoto = $localStorage.$default({
                    Foto : response.data.Foto
                });

                var data = response.data;
                delete data.Foto;

                AuthenticationService.SetCredentials( data.UserId, data.Password, 
                                                     data);    
                ProfileCourseService.SetProfileData(data);
                $location.path('/studentprofile');    

            },function(response){
                FlashService.Error("Registro fallido");//errores
                vm.dataLoading = false;
            });
            //vm.regData.Password = pass;
        }

    }
})();
