(function () {
    'use strict';

    angular
        .module('app')
        .controller('NotificationsController', NotificationsController);

    NotificationsController.$inject = ['$location',  'FlashService'];
    function NotificationsController($location,  FlashService) {
        var vm = this;


    }

})();