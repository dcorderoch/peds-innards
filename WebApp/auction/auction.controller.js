(function () {
    'use strict';

    angular
        .module('app')
        .controller('AuctionController', AuctionController);

    AuctionController.$inject = ['$location',  'FlashService'];
    function AuctionController($location,  FlashService) {
        var vm = this;


    }

})();