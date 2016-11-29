(function () {
    'use strict';

    angular
        .module('app')
        .controller('Register2Controller', Register2Controller);

    Register2Controller.$inject = ['$location',  'FlashService', 'UserService','$rootScope', 'RegService'];
    function Register2Controller($location,  FlashService, UserService, $rootScope,RegService) {
        var vm = this;

        vm.register=register;
        vm.repositories=[{tipo:'Google Drive', id:"0"}, {tipo:'Dropbox',id:"1"}];

        vm.regData={};

        initController();
        function initController(){
            loadUniversities();
            loadCountries();
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

        function register() {

            if (vm.regData.hasOwnProperty("Foto")){
                vm.regData.Foto =  "data:image/jpg;base64,"+vm.regData.Foto.base64
            }

            if (!vm.regData.hasOwnProperty("Foto")){
                vm.regData.Foto =  "";
            }

            vm.regData.Telefono = vm.regData.Telefono.toString();
            vm.dataLoading = true;

            console.log( vm.regData);

            UserService.RegisterProfessor(vm.regData)
                .then(function (response) {

                FlashService.Success('Registro exitoso', true);
                $location.path('/professorprofile');    
                $rootScope.userData= response.data;

            },function(response){
                console.log( vm.regData);
                FlashService.Error("Registro fallido");//errores
                vm.dataLoading = false;
            });
        }
        
    }
})();