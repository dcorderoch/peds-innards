(function () {
    'use strict';

    angular
        .module('app')
        .controller('Register3Controller', Register3Controller);

    Register3Controller.$inject = ['$location',  'FlashService', 'UserService', 'RegService',  'AuthenticationService', '$localStorage', 'ProfileCourseService'];
    function Register3Controller($location,  FlashService, UserService, RegService,  AuthenticationService, $localStorage, ProfileCourseService) {
        var vm = this;

        vm.register=register;
        vm.repositories=[{tipo:'Google Drive', id:"0"}, {tipo:'Dropbox',id:"1"}];
        vm.countries = [{ CountryId: "2", CountryName:"a" }, { CountryId: "0", CountryName:"b" } ]
        vm.regData={};

        initController();

        function initController(){
            loadCountries();
        }


        function loadCountries(){
            RegService.GetCountries()
                .then(function (response) {

                vm.countries = response.data;

            },function(response){
            });
        }

        function register() {


            var oFile = document.getElementById("fileUpload").files[0]; // <input type="file" id="fileUpload" accept=".jpg,.png,.gif,.jpeg"/>

            if (oFile.size > 2097152) // 2 mb for bytes.
            {
                alert("El tamaño de la foto excede el tamaño máximo de 2mb");
                return;
            }


            if (vm.regData.hasOwnProperty("Foto")){
                vm.regData.Foto =  vm.regData.Foto.base64;
            }

            if (!vm.regData.hasOwnProperty("Foto")){
                vm.regData.Foto =  "";
            }


            vm.regData.Telefono = vm.regData.Telefono.toString();
            vm.dataLoading = true;


            UserService.RegisterEmployer(vm.regData)
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

                $location.path('/employerprofile');    

            },function(response){

                FlashService.Error("Registro fallido");//errores
                vm.dataLoading = false;
            });
        }


    }
})();