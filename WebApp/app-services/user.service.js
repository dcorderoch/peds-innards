(function () {

    angular
        .module('app')          //servicio para el cliente
        .factory('UserService', UserService);

    UserService.$inject = ['$http','$rootScope'];
    function UserService($http, $rootScope) {

        var service = {};

        service.RegisterStudent = RegisterStudent;
        service.RegisterProfessor = RegisterProfessor;
        service.RegisterEmployer = RegisterEmployer;

        return service;
        /**
       *  Obtiene los registros  por un paciente, 
       * los que estan ligados a el
       */  
        function RegisterStudent(regData) {
            var response=$http({
                method:"post",
                url:$rootScope.url+"api/medicalrecords/viewallbypatient",
                data:regData
            });
            return response;    
        }
        
        function RegisterProfessor(regData) {
            var response=$http({
                method:"post",
                url:$rootScope.url+"api/medicalrecords/viewallbypatient",
                data:regData
            });
            return response;    
        }
        
        function RegisterEmployer(regData) {
            var response=$http({
                method:"post",
                url:$rootScope.url+"api/medicalrecords/viewallbypatient",
                data:regData
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