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

        function initController(){


            $rootScope.currentCourseData={};
            loadTechnologies();

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

            JobService.Create(vm.job)
                .then(function(response){
                console.log(response.data);
                FlashService.success("Trabajo creado");
            },function(response){
                console.log("supongo3");
                FlashService.Error("No se pudo crear el proyecto")
            });
        }

        function disableAccount(){

            UserService.Disable(vm.UserId)
                .then(function(response){

                FlashService.Success("Cuenta deshabilitada");
                $location.path("/login")

            }, function(response){
                console.log("no funcó");
            })
        }

    }

})();