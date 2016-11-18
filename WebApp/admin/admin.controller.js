(function () {
    'use strict';

    angular
        .module('app')
        .controller('AdminController', AdminController);

    AdminController.$inject = ['$location',  'FlashService'];
    function AdminController($location,  FlashService) {
        var vm = this;


    }

})();