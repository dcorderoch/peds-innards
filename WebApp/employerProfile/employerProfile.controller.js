(function () {
    'use strict';

    angular
        .module('app')
        .controller('EmployerProfileController', EmployerProfileController);

    EmployerProfileController.$inject = ['$location',  'FlashService', '$rootScope', 'JobService', 'UserService', 'ProfileCourseService'];
    function EmployerProfileController($location,  FlashService, $rootScope, JobService, UserService, ProfileCourseService) {
        var vm = this;

        initController();

        vm.goWorkActive = goWorkActive;
        vm.goWorkFinished = goWorkFinished;
        vm.disableAccount =disableAccount;

        function initController(){

            $rootScope.userData ={};
            vm.userData = $rootScope.userData;

            var currentworkData={
                "JobOffer":"algo",
                "Technologies":["Python","CSS"],
                "Location":"algo",
                "StartDate":"1984-02-03",
                "EndDate":"1990-09-02",
                "Description":"algo",
                "Budget":8
            };
            currentworkData.JobOfferId ="123";
            currentworkData.status = true;
            ProfileCourseService.SetWorkData(currentworkData);   

            vm.userData.Active = "1";
            vm.toggleEnable;
            if (vm.userData.Active == "0"){
                vm.toggleEnable = false;
            }
            if(vm.userData.Active == "1"){
                vm.toggleEnable = true;
            }

        }

        function goWorkFinished(id){

            $location.path('/auction');  
            return;

            JobService.GetById(id)
                .then(function(response){

                var currentworkData = response.data;
                currentworkData.status = false;
                currentworkData.JobOfferId =id;

                ProfileCourseService.SetWorkData(currentWorkData);

                $location.path('/sharedareaemployer');    

            }, function(response){
                console.log("no sirvio")
            });
        }

        function goWorkActive(id){

            JobService.GetById(id)
                .then(function(response){

                var currentworkData = response.data;
                currentworkData.status = true;
                ProfileCourseService.SetWorkData(currentWorkData);

                if(currentworkData.State =="0") 
                    $location.path('/auction'); 
                else
                    $location.path('/sharedareaemployer'); 

            }, function(response){
                console.log("no sirvio")
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