(function () {
    'use strict';

    angular
        .module('app')
        .controller('Register2Controller', Register2Controller);

    Register2Controller.$inject = ['$location',  'FlashService', 'UserService','$rootScope', 'RegService'];
    function Register2Controller($location,  FlashService, UserService, $rootScope,RegService) {
        var vm = this;

        vm.register=register;
        vm.repositories=[{tipo:'Google Drive', id:1}, {tipo:'Dropbox',id:0}];
        
        vm.universities=[{University:'Tecnológico de Costa Rica',UniversityId:0}, {University:'Oxford University' ,UniversityId:1}];
        
        vm.countries=[ {"Country":"costa rica","CountryId": "1"}, {"Country":"nicaragua","CountryId": "2"}];

        vm.regData={};

        initController();
        function initController(){
            loadUniversities();
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