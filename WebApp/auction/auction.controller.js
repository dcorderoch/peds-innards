(function () {
    'use strict';

    angular
        .module('app')
        .controller('AuctionController', AuctionController);

    AuctionController.$inject = ['$location',  'FlashService', '$rootScope', 'JobService', 'UserService', 'ProfileCourseService'];
    function AuctionController($location,  FlashService, $rootScope, JobService, UserService, ProfileCourseService) {
        var vm = this;

        initController();
        vm.goArea = goArea;
        vm.goProfile = goProfile;

        function initController(){

            vm.listOfBids=[];
            vm.userData = $rootScope.userData;
            vm.workData= ProfileCourseService.GetWorkData();
            console.log(vm.workData);
            getBids();

        }

        function getBids(){

            JobService.GetBidsById(vm.workData.JobOfferId)
                .then( function(response){

                vm.listOfBids = response.data;

            }, function(response){

                FlashService.Error("No se pudieron cargar los ofertantes");
            });
        }

        function goArea(bid){
            console.log(bid)
            vm.workData.studentInfo = bid;
            ProfileCourseService.SetWorkData(vm.workData);
            $location.path("sharedareaemployer");
        }


        function goProfile(bid){
            console.log(bid)
            vm.workData.studentInfo = bid;
            ProfileCourseService.SetWorkData(vm.workData);
            $location.path("sharedareaemployer");
        }
    }
})();