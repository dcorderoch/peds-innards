(function () {
    'use strict';

    angular
        .module('app')
        .controller('OfferingController', OfferingController);

    OfferingController.$inject = ['$location',  'FlashService', '$rootScope', 'SearchOfferingService'];
    function OfferingController($location,  FlashService, $rootScope, SearchOfferingService) {
        var vm = this;

        initController();
        
        function initController(){
            
            console.log (SearchOfferingService.GetSearchData());
            vm.offerData = SearchOfferingService.GetSearchData();
            
            
        }
    }

})();