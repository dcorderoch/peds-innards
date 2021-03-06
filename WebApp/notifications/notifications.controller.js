(function () {
    'use strict';

    angular
        .module('app')
        .controller('NotificationsController', NotificationsController);

    NotificationsController.$inject = ['$location',  'FlashService',  'NotificationsService', 'ProfileCourseService' ];
    function NotificationsController($location,  FlashService,  NotificationsService, ProfileCourseService ) {
        var vm = this;

        vm.notifications=[];
        initController();

        // Starts controller
        function initController(){

            getNotifications();
            console.log(vm.notifications);
        }

        //Loads user's notifications from server
        function getNotifications(){

            var id = ProfileCourseService.GetProfileData().UserId;

            NotificationsService.GetNotifications( id )
                .then( function(response){

                vm.notifications = response.data;

            }, function(response){
                FlashService.Error("No se pudieron obtener las notificaciones")
            });       
        }


    }

})();
