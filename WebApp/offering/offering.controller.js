(function () {
    'use strict';

    angular
        .module('app')
        .controller('OfferingController', OfferingController);

    OfferingController.$inject = ['$location',  'FlashService', '$rootScope', 'SearchOfferingService', 'JobService' ];
    function OfferingController($location,  FlashService, $rootScope, SearchOfferingService, JobService ) {
        var vm = this;

        initController();
        vm.createBid = createBid;

        function initController(){

            vm.bids=[];
            console.log (SearchOfferingService.GetSearchData());
            console.log($rootScope.userData);

            vm.userData = $rootScope.userData;
            vm.offerData = SearchOfferingService.GetSearchData();
            console.log(vm.offerData);
            getBidsById();

        }

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

        function createBid( time,money ){

            var timeString = time.toString();
            var moneyString = money.toString();

            var send={JobOfferId: vm.offerData.JobOfferId, Money:moneyString, DurationDays: timeString, StudentSurname: vm.userData.NombreContacto, StudentUserId: vm.userData.UserId };

            console.log(send)

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
