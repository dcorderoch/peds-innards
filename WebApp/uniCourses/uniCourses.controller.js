(function () {
    'use strict';

    angular
        .module('app')
        .controller('UniCoursesController', UniCoursesController);

    UniCoursesController.$inject = ['$location',  'FlashService'];
    function UniCoursesController($location,  FlashService) {
        var vm = this;


    }

})();