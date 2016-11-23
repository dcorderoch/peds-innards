(function () {
    'use strict';

    angular
        .module('app')
        .controller('OfferingController', OfferingController);

    OfferingController.$inject = ['$location',  'FlashService', '$rootScope'];
    function OfferingController($location,  FlashService, $rootScope) {
        var vm = this;


    }

})();