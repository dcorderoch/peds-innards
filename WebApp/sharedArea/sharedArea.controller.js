(function () {
    'use strict';

    angular
        .module('app')
        .controller('SharedAreaController', SharedAreaController);

    SharedAreaController.$inject = ['$location',  'FlashService'];
    function SharedAreaController($location,  FlashService) {
        var vm = this;


    }

})();