(function () {
    'use strict';

    angular
        .module('app')
        .controller('NotificationsController', NotificationsController);

    NotificationsController.$inject = ['$location',  'FlashService', '$rootScope', 'NotificationsService'];
    function NotificationsController($location,  FlashService, $rootScope, NotificationsService) {
        var vm = this;

        vm.notifications=[];
        initController();

        function initController(){

            getNotifications();
            console.log(vm.notifications);
            $rootScope.currentCourseData={};
        }

        var Carnet = $rootScope.userData.Carnet;

        function getNotifications(){
            NotificationsService.GetNotifications( Carnet )
                .then( function(response){
                
                if (response.data.ReturnStatus == "1"){
                    vm.notifications = response.data.Notifications;
                }
            }, function(response){
                console.log("no se pudo obtener")
            });       
        }


    }

})();
