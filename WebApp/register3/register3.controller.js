(function () {
    'use strict';

    angular
        .module('app')
        .controller('Register3Controller', Register3Controller);

    Register3Controller.$inject = ['$location',  'FlashService', 'UserService', 'RegService', '$rootScope', 'AuthenticationService'];
    function Register3Controller($location,  FlashService, UserService, RegService, $rootScope, AuthenticationService) {
        var vm = this;

        vm.register=register;
        vm.repositories=[{tipo:'Google Drive', id:"0"}, {tipo:'Dropbox',id:"1"}];

        vm.regData={};

        initController();

        function initController(){
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

        function register() {


            if (vm.regData.hasOwnProperty("Foto")){
                vm.regData.Foto =  vm.regData.Foto.base64;
            }

            if (!vm.regData.hasOwnProperty("Foto")){
                console.log(vm.regData.hasOwnProperty("Foto"));
                vm.regData.Foto =  "";
            }


            vm.regData.Telefono = vm.regData.Telefono.toString();
            vm.dataLoading = true;

            console.log(vm.regData);

            UserService.RegisterEmployer(vm.regData)
                .then(function (response) {

                FlashService.Success('Registro exitoso', true);

                AuthenticationService.SetCredentials( response.data.UserId, response.data.Password, 
                                                     response.data);    
                $rootScope.userData= response.data;
                
                $location.path('/employerprofile');    

            },function(response){
                console.log( vm.regData);
                FlashService.Error("Registro fallido");//errores
                vm.dataLoading = false;
            });
        }

    }

})();