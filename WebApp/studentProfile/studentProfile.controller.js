(function () {
    'use strict';

    angular
        .module('app')
        .controller('StudentProfileController', StudentProfileController);

    StudentProfileController.$inject = ['$location',  'FlashService'];
    function StudentProfileController($location,  FlashService) {
        var vm = this;

        vm.goArea =goArea;
        vm.goArea2= goArea2;
        
        function goArea(){
            
            $location.path('/coursearea');
        }
        function goArea2(){
            $location.path('/sharedarea');
        }
    }

})();