(function () {

    angular
        .module('app')          //servicio para el cliente
        .factory('JobService', JobService);

    JobService.$inject = ['$http','$rootScope'];
    function JobService($http, $rootScope) {

        var service = {};

        service.Create = Create;


        return service;

        function Create(createData) {
            var response=$http({
                method:"post",
                url:$rootScope.url+"host/Api/JobOffer/Create",
                data:createData
            });
            return response;    
        }

        
    }

})();
