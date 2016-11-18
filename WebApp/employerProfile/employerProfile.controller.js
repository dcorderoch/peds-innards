(function () {
    'use strict';

    angular
        .module('app')
        .controller('EmployerProfileController', EmployerProfileController);

    EmployerProfileController.$inject = ['$location',  'FlashService'];
    function EmployerProfileController($location,  FlashService) {
        var vm = this;


    }

})();