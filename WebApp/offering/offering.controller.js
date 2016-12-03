(function () {
    'use strict';

    angular
        .module('app')
        .controller('OfferingController', OfferingController);

    OfferingController.$inject = ['$location',  'FlashService', 'SearchOfferingService', 'JobService', 'ProfileCourseService' ];
    function OfferingController($location,  FlashService, SearchOfferingService, JobService, ProfileCourseService ) {
        var vm = this;

        initController();
        vm.createBid = createBid;

        // Starts controller gets info from cookies.
        function initController(){

            vm.bids=[];

            vm.userData = ProfileCourseService.GetProfileData();
            vm.offerData = SearchOfferingService.GetSearchData();
            console.log(vm.offerData);
            getBidsById();

        }

        //Gets bids from other students
        // Check the bids
        function getBidsById(){

            JobService.GetBidsById(vm.offerData.JobOfferId)
                .then(function(response){

                vm.bids = response.data;
                checkBid();

            }, function(response){
                FlashService.Error("No se pudieron traer los ofertantes ")
                console.log("no sirvió "+ response)
            });
        }

        // Create a new bid to try to get the auction
        // loads the list of bids from server
        function createBid( time,money ){

            var timeString = time.toString();
            var moneyString = money.toString();

            var send={JobOfferId: vm.offerData.JobOfferId, Money:moneyString, DurationDays: timeString, StudentSurname: vm.userData.NombreContacto, StudentUserId: vm.userData.UserId };

            JobService.BidCreate(send)
                .then( function(response){

                FlashService.Success("Oferta laboral creada");
                getBidsById();

            }, function(response){
                console.log("no se pudo crear la oferta");
                FlashService.Error("No se pudo crear la oferta");
                console.log(send);
            });
        }

        
        function checkBid(){

            var i;
            var userId = vm.userId.StudentUserId;
            for ( i=0; i < vm.bids.lenght; i++ ){

                if ( vm.bids[i].StudentUserId == userId){
        
                    
                }   
            }
        }

    }
})();
