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
        }


        function getNotifications(){

            var id = $rootScope.userData.UserId;

            NotificationsService.GetNotifications( id )
                .then( function(response){

                vm.notifications = response.data;

            }, function(response){
                FlashService.Error("No se pudieron obtener las notificaciones")
            });       
        }


    }

})();
