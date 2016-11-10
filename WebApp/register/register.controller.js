(function () {
    'use strict';

    angular
        .module('app')
        .controller('RegisterController', RegisterController);

    RegisterController.$inject = ['$location',  'FlashService'];
    function RegisterController($location,  FlashService) {
        var vm = this;


    }

})();