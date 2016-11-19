(function () {
    'use strict';

    angular
        .module('app')
        .controller('SharedAreaEmployerController', SharedAreaEmployerController);

    SharedAreaEmployerController.$inject = ['$location',  'FlashService', '$scope'];
    function SharedAreaEmployerController($location,  FlashService, $scope) {
        var vm = this;


    }

})();