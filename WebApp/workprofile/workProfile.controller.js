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

            vm.userData = $rootScope.userData;

            vm.courseAverageWidth = {'width': vm.userData.PromedioCursos+'%'};  
            vm.projectAverageWidth = {'width': vm.userData.PromedioProyectos+'%'};   


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