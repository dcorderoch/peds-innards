(function () {
    'use strict';

    angular
        .module('app')
        .controller('OfferingController', OfferingController);

    OfferingController.$inject = ['$location',  'FlashService', '$rootScope', 'SearchOfferingService', 'JobService'];
    function OfferingController($location,  FlashService, $rootScope, SearchOfferingService, JobService) {
        var vm = this;

        initController();

        function initController(){

            vm.bids=[];
            console.log (SearchOfferingService.GetSearchData());
            vm.offerData = SearchOfferingService.GetSearchData();

            getBidsById();
            vm.bids = [{
                "Money":"345",
                "DurationInDays": "9" ,
                "StudentName":"Daniel Madriz",
                "StudentSurname":"algo",
                "StudentUserId":"Id"
            },
                       {
                           "Money":"123",
                           "DurationInDays": "23",
                           "StudentName":"Kevin Moraga",
                           "StudentSurname":"algo",
                           "StudentUserId":"Id"
                       }];
        }

        function getBidsById(){

            JobService.GetBidsById(vm.offerData.JobOfferId)
                .then(function(response){

                vm.bids = response.data;
                vm.bids = [{
                    "Money":"algo",
                    "DurationInDays": [0-9],
                    "StudentName":"algo",
                    "StudentSurname":"algo",
                    "StudentUserId":"Id"
                },
                           {
                               "Money":"algo",
                               "DurationInDays": [0-9],
                               "StudentName":"algo",
                               "StudentSurname":"algo",
                               "StudentUserId":"Id"
                           }];


            }, function(response){

                console.log("no sirvió "+ response)
            });
        }

    }

})();
