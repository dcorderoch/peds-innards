(function () {
    'use strict';

    angular
        .module('app')
        .controller('HomeController', HomeController);

    HomeController.$inject = ['$location',  'FlashService'];
    function HomeController($location,  FlashService) {
        var vm = this;


    }

})();