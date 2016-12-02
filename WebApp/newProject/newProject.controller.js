(function () {
    'use strict';

    angular
        .module('app')
        .controller('NewProjectController', NewProjectController);

    NewProjectController.$inject = ['$location',  'FlashService' ,'$rootScope', 'RegService', 'JobService', 'UserService', '$localStorage'];
    function NewProjectController($location,  FlashService, $rootScope, RegService, JobService, UserService, $localStorage) {
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

            vm.photo = "data:image/jpg;base64," + $localStorage.Foto;
            console.log($localStorage);

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
            vm.job.EmployerId = vm.userData.UserId; 
            JobService.Create(vm.job)
                .then(function(response){

                setTimeout( function(){console.log(response.data)},1000);

                var result = response.data.ReturnStatus;
                console.log(result);
                
                if (result === 2){

                    FlashService.Success("Trabajo creado y twiteado", true);
                    $location.path('/employerprofile')
                }
                if (result === 1){

                    FlashService.Success("Trabajo creado pero no twiteado", true);
                    $location.path('/employerprofile')
                }
                if (result === 0){

                    FlashService.Error("No se pudo crear el trabajo");

                }

                console.log(response.data);
            },function(response){
                console.log(vm.job);
                FlashService.Error("No se pudo crear el trabajo")
            });
        }

        function disableAccount(){

            console.log(vm.userData.userId);
            console.log(vm.userData.Active);

            UserService.Disable(vm.userData.UserId)
                .then(function(response){

                if (vm.userData.Active == "1"){
                    if (response.data.ReturnStatus == "1"){ 

                        FlashService.Success("La cuenta ha sido deshabilitada",true);
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