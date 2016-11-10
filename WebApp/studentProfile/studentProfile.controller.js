(function () {
    'use strict';

    angular
        .module('app')
        .controller('StudentProfileController', StudentProfileController);

    StudentProfileController.$inject = ['$location',  'FlashService'];
    function StudentProfileController($location,  FlashService) {
        var vm = this;


    }

})();