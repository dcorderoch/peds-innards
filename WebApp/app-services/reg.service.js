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
        service.GetGoogleAuthURI = GetGoogleAuthURI;

        return service;
        /**
       *  Obtiene los registros  por un paciente, 
       * los que estan ligados a el
       */  
        function GetTechnologies() {
            var response=$http({
                method:"get",
                url:$rootScope.url+"Technology/GetAll"
            });
            return response;    
        }

        function GetCountries() {
            var response=$http({
                method:"get",
                url:$rootScope.url+"Country/GetAll"
            });
            return response;    
        }

        function GetUniversities() {
            var response=$http({
                method:"get",
                url:$rootScope.url+"University/GetAll"
            });
            return response;    
        }

        function GetLanguages() {
            var response=$http({
                method:"get",
                url:$rootScope.url+"Language/GetAll"
            });
            return response;    
        }
        
        function GetGoogleAuthURI( ) {
            var response=$http({
                method:"get",
                url:$rootScope.url+"Register/GetGoogleAuthURI"
            });
            return response;    
        }



    }
})();
