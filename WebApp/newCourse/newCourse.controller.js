(function () {
    'use strict';

    angular
        .module('app')
        .controller('NewCourseController', NewCourseController);

    NewCourseController.$inject = ['$location',  'FlashService'];
    function NewCourseController($location,  FlashService) {
        var vm = this;

    }

})();