(function () {
    'use strict';

    angular
        .module('app')
        .controller('SharedStudentEmployerController', SharedStudentEmployerController);

    SharedStudentEmployerController.$inject = ['$location',  'FlashService'];
    function SharedStudentEmployerController($location,  FlashService) {
        var vm = this;


    }

})();