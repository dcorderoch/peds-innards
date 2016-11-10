(function () {
    'use strict';

    angular
        .module('app')
        .controller('SearcherController', SearcherController);

    SearcherController.$inject = ['$location',  'FlashService'];
    function SearcherController($location,  FlashService) {
        var vm = this;


    }

})();