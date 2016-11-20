(function () {

    angular
        .module('app')          
        .factory('RegService', RegService);

    RegService.$inject = ['$http','$rootScope'];
    function RegService($http, $rootScope) {

        var service = {};

        service.GetTechnologies = GetTechnologies;
        service.GetUniversities = GetUniversities;
        service.GetCountries = GetCountries;
        service.GetLanguages= GetLanguages;

        return service;
        /**
       *  Obtiene los registros  por un paciente, 
       * los que estan ligados a el
       */  
        function GetTechnologies() {
            var response=$http({
                method:"get",
                url:$rootScope.url+"Api/Technology/GetAll"
            });
            return response;    
        }

        function GetCountries() {
            var response=$http({
                method:"get",
                url:$rootScope.url+"Api/Country/GetAll"
            });
            return response;    
        }

        function GetUniversities() {
            var response=$http({
                method:"get",
                url:$rootScope.url+"Api/University/GetAll"
            });
            return response;    
        }
        
        function GetLanguages() {
            var response=$http({
                method:"get",
                url:$rootScope.url+"Api/Language/GetAll"
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
