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

        vm.countries=[ {"country":"costa rica","countryid": "1"}, {"country":"nicaragua","countryid": "2"}];
        vm.technologies=[{ Technology:"Java", TechnologyId: "0"}, { Technology:"C++",TechnologyId: "1"}];

        vm.job.Technologies = [];


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

        function initController(){


            vm.userData = $rootScope.userData;
            loadTechnologies();
            loadCountries();

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

            UserService.Disable(vm.UserId)
                .then(function(response){

                if (response.data.ReturnStatus == "1"){ 

                    FlashService.Success("Cuenta deshabilitada");
                    $location.path("/login")
                }
                else{
                    FlashService.Error("No se pudo cerrar la cuenta");
                }
                
            }, function(response){
                console.log("no funcó");
            })
        }

    }

})();