(function () {
    'use strict';

    angular
        .module('app')
        .controller('WorkProfileController', WorkProfileController);

    WorkProfileController.$inject = ['$location',  'FlashService'];
    function WorkProfileController($location,  FlashService) {
        var vm = this;


    }

})();