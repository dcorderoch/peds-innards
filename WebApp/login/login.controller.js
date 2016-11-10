(function () {
    'use strict';

    angular
        .module('app')
        .controller('LoginController', LoginController);

    LoginController.$inject = ['$location',  'FlashService'];
    function LoginController($location,  FlashService) {
        var vm = this;


    }

})();