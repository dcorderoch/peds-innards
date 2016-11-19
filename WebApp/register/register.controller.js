(function () {
    'use strict';

    angular
        .module('app')
        .controller('RegisterController', RegisterController);

    RegisterController.$inject = ['$location',  'FlashService'];
    function RegisterController($location,  FlashService) {
        var vm = this;
        
        vm.fruits = ['Español', 'Inglés', 'Francés', 'Portugués'];

        // selected fruits
        vm.selection = [];

        // toggle selection for a given fruit by name
        vm.toggleSelection = function toggleSelection(fruitName) {
            var idx = vm.selection.indexOf(fruitName);

            // is currently selected
            if (idx > -1) {
                vm.selection.splice(idx, 1);
            }

            // is newly selected
            else {
                vm.selection.push(fruitName);
            }
        };

    }

})();