(function () {

    angular
        .module('app')          
        .factory('SearchOfferingService', SearchOfferingService);

    SearchOfferingService.$inject = ['$http','$rootScope', '$cookieStore'];
    function SearchOfferingService($http, $rootScope, $cookieStore) {

        var service = {};

        var searchData;

        service.GetSearchData = GetSearchData;
        service.SetSearchData = SetSearchData;

        return service;

        function GetSearchData( ){

            searchData= $cookieStore.get('offeringData') || {};
            return searchData;
        }

        function SetSearchData(data){

            searchData = data;
            $cookieStore.put('offeringData', searchData);
        }
    }

})();
