(function () {
    'use strict';

    angular
        .module('app')
        .controller('OfferingController', OfferingController);

    OfferingController.$inject = ['$location',  'FlashService'];
    function OfferingController($location,  FlashService) {
        var vm = this;


    }

})();