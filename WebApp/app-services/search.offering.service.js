(function () {

    angular
        .module('app')          
        .factory('SearchOfferingService', SearchOfferingService);

    SearchOfferingService.$inject = ['$http','$rootScope'];
    function SearchOfferingService($http, $rootScope) {

        var service = {};

        var searchData;

        service.GetNotifications = GetNotifications;
        service.GetSearchData = GetSearchData;
        service.SetSearchData = SetSearchData;

        return service;

        function GetNotifications(Carnet) {
            var response=$http({
                method:"post",
                url:$rootScope.url+"Api/Notifications/GetByStudent",
                data: {"Carnet":Carnet}
            });
            return response;    
        }

        function GetSearchData( ){

            return searchData;
        }

        function SetSearchData(data){
        
            searchData = data;
        }
    }

})();
