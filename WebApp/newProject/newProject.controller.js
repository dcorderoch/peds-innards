(function () {
    'use strict';

    angular
        .module('app')
        .controller('NewProjectController', NewProjectController);

    NewProjectController.$inject = ['$location',  'FlashService' ,'$rootScope', 'RegService', 'JobService', 'UserService'];
    function NewProjectController($location,  FlashService, $rootScope, RegService, JobService, UserService) {
        var vm = this;

        initController();

        vm.job={};
        vm.createJob = createJob;
        vm.disableAccount =disableAccount;

        vm.job.Technologies = [];


        function initController(){

            vm.userData = $rootScope.userData;

            if (vm.userData.TipoRepositorioArchivos == "0"){

                vm.userData.TipoRepositorioArchivos = "Google Drive"
            }
            if (vm.userData.TipoRepositorioArchivos == "1"){

                vm.userData.TipoRepositorioArchivos = "Dropbox"
            }

            loadTechnologies();
            loadCountries();

            vm.photo = "data:image/jpg;base64," + vm.userData.Foto


            vm.toggleEnable;
            if (vm.userData.Active == "0"){
                vm.toggleEnable = false;
            }
            if(vm.userData.Active == "1"){
                vm.toggleEnable = true;
            }

        }

        function loadTechnologies(){
            RegService.GetTechnologies()
                .then(function (response) {

                vm.technologies = response.data;
                console.log( response.data)

            },function(response){
                console.log("supongo1")
            });
        }

        function loadCountries(){
            RegService.GetCountries()
                .then(function (response) {

                vm.countries = response.data;
                console.log( response.data)

            },function(response){
                console.log("supongo2")
            });
        }



        vm.toggleSelectionTech = function toggleSelectionTech(technology) {
            var idx = vm.job.Technologies.indexOf(technology.TechnologyId);

            // is currently selected
            if (idx > -1) {
                vm.job.Technologies.splice(idx, 1);
            }

            // is newly selected
            else {
                vm.job.Technologies.push(technology.TechnologyId);
            }
        };

        function createJob(){

            vm.job.Budget = vm.job.Budget.toString();
            JobService.Create(vm.job)
                .then(function(response){

                if (response.data.ReturnStatus == "2"){

                    FlashService.success("Trabajo creado y twiteado");

                }
                if (response.data.ReturnStatus == "1"){

                    FlashService.success("Trabajo creado pero no twiteado");

                }
                else{ 
                    FlashService.Error("No se pudo crear el proyecto")
                }

                console.log(response.data);
            },function(response){
                console.log(vm.job);
                FlashService.Error("No se pudo crear el proyecto")
            });
        }

        function disableAccount(){

            console.log(vm.userData.userId);
            console.log(vm.userData.Active);

            UserService.Disable(vm.userData.UserId)
                .then(function(response){

                if (vm.userData.Active == "1"){
                    if (response.data.ReturnStatus == "1"){ 

                        FlashService.Success("La cuenta ha sido deshabilitada");
                        $location.path("/login")

                    }
                    else{
                        FlashService.Error("No se pudo deshabilitar la cuenta");
                    }
                }
                if (vm.userData.Active == "0"){

                    if (response.data.ReturnStatus == "1"){ 

                        FlashService.Success("Cuenta habilitada");
                        vm.toggleEnable =true;
                        vm.userData.Active = "1";
                    }
                    else{
                        FlashService.Error("No se pudo habilitar la cuenta");
                    }
                }

            }, function(response){
                if (vm.userData.Active == "0"){ 
                    FlashService.Error("No se pudo habilitar la cuenta");
                } 
                if (vm.userData.Active == "1"){ 
                    FlashService.Error("No se pudo deshabilitar la cuenta");
                }

                console.log("no funcó");
            })
        }


    }
})();