(function () {
    'use strict';

    angular
        .module('app')
        .controller('NewProjectController', NewProjectController);

    NewProjectController.$inject = ['$location',  'FlashService'];
    function NewProjectController($location,  FlashService) {
        var vm = this;


    }

})();