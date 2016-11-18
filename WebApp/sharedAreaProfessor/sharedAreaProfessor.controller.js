(function () {
    'use strict';

    angular
        .module('app')
        .controller('SharedAreaProfessorController', SharedAreaProfessorController);

    SharedAreaProfessorController.$inject = ['$location',  'FlashService'];
    function SharedAreaProfessorController($location,  FlashService) {
        var vm = this;


    }

})();