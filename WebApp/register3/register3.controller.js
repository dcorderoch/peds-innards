(function () {
    'use strict';

    angular
        .module('app')
        .controller('Register3Controller', Register3Controller);

    Register3Controller.$inject = ['$location',  'FlashService', 'UserService', 'RegService'];
    function Register3Controller($location,  FlashService, UserService, RegService) {
        var vm = this;

        vm.register=register;
        vm.repositories=[{tipo:'Google Drive', id:1}, {tipo:'Dropbox',id:0}];

        vm.countries=[ {"Country":"costa rica","CountryId": "1"}, {"Country":"nicaragua","CountryId": "2"}];

        vm.regData={};
        initController();

        function initController(){
            loadCountries();
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

        function register() {
            
            console.log("entro");
            vm.regData.Foto =  "data:image/jpg;base64,"+vm.regData.Foto.base64
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