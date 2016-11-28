(function () {

    angular
        .module('app')          
        .factory('NotificationsService', NotificationsService);

    NotificationsService.$inject = ['$http','$rootScope'];
    function NotificationsService($http, $rootScope) {

        var service = {};

        service.GetNotifications = GetNotifications;

        return service;
  
        function GetNotifications(Carnet) {
            var response=$http({
                method:"post",
                url:$rootScope.url+"Notifications/GetByStudent",
                data: {"Carnet":Carnet}
            });
            return response;    
        }

       
        // private functions

        function handleSuccess(res) {
            return res.data;
        }

        function handleError(error) {
            return function () {
                return { success: false, message: error };
            };
        }
    }

})();
