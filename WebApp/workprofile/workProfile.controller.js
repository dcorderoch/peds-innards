(function () {
    'use strict';

    angular
        .module('app')
        .controller('WorkProfileController', WorkProfileController);

    WorkProfileController.$inject = ['$location',  'FlashService', '$rootScope','JobService', 'UserService', 'ProfileCourseService'];
    function WorkProfileController($location,  FlashService, $rootScope, JobService, UserService, ProfileCourseService) {
        var vm = this;

        vm.goWorkActive = goWorkActive;
        vm.goWorkFinished = goWorkFinished;
        vm.disableAccount =disableAccount;

        initController();

        function initController(){

            $rootScope.userData ={};
            vm.userData = $rootScope.userData;

            vm.courseAverageWidth = {'width': vm.userData.PromedioCursos+'%'};  
            vm.projectAverageWidth = {'width': vm.userData.PromedioProyectos+'%'};   


            vm.userData.Active = "0";
            vm.toggleEnable;
            if (vm.userData.Active == "0"){
                vm.toggleEnable = false;
            }
            if(vm.userData.Active == "1"){
                vm.toggleEnable = true;
            }

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
            console.log(currentworkData)
        }


        function goWorkFinished(id){

            $location.path('/sharedstudentemployer');  
            return;

            JobService.GetById(id)
                .then(function(response){

                var currentworkData = response.data;
                currentworkData.status = false;
                currentworkData.JobOfferId =id;

                ProfileCourseService.SetWorkData(currentWorkData);

                $location.path('/sharedStudentEmployer');    

            }, function(response){
                console.log("no sirvio")
            });
        }
        function goWorkActive(id){

            JobService.GetById(id)
                .then(function(response){

                var currentworkData = response.data;
                currentworkData.status = false;
                ProfileCourseService.SetWorkData(currentWorkData);
                $location.path('/sharedStudentEmployer'); 

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