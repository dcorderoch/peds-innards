(function () {

    angular
        .module('app')          
        .factory('RegService', RegService);

    RegService.$inject = ['$http','$rootScope'];
    function RegService($http, $rootScope) {

        var service = {};

        service.GetTechnologies = GetTechnologies;
        service.GetUniversities = GetUniversities;

        return service;
        /**
       *  Obtiene los registros  por un paciente, 
       * los que estan ligados a el
       */  
        function GetTechnologies(regData) {
            var response=$http({
                method:"get",
                url:$rootScope.url+"api/medicalrecords/viewallbypatient"
            });
            return response;    
        }
        
        function GetUniversities(regData) {
            var response=$http({
                method:"post",
                url:$rootScope.url+"api/medicalrecords/viewallbypatient"
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
