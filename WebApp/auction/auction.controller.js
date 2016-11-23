(function () {
    'use strict';

    angular
        .module('app')
        .controller('AuctionController', AuctionController);

    AuctionController.$inject = ['$location',  'FlashService', '$rootScope'];
    function AuctionController($location,  FlashService, $rootScope) {
        var vm = this;
        
        initController();

        function initController(){

            $rootScope.currentCourseData={};
        }

    }

})();