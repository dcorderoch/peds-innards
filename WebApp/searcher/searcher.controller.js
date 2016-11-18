(function () {
    'use strict';

    angular
        .module('app')
        .controller('SearcherController', SearcherController);

    SearcherController.$inject = ['$location',  'FlashService', '$scope'];
    function SearcherController($location,  FlashService, $scope) {
        var vm = this;

        vm.goOffering = goOffering;
        
        function goOffering(){
            $location.path('/offering');
        }
    }

})();