(function () {
    'use strict';

    angular
        .module('app')
        .controller('ProfessorProfileController', ProfessorProfileController);

    ProfessorProfileController.$inject = ['$location',  'FlashService'];
    function ProfessorProfileController($location,  FlashService) {
        var vm = this;

        vm.goArea =goArea;
        vm.goArea2= goArea2;
        
        function goArea(){
            
            $location.path('/courseoverview');
        }
        function goArea2(){
           // $location.path('/sharedarea');
        }
    }

})();