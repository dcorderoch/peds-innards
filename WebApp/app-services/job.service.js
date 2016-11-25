(function () {

    angular
        .module('app')          //servicio para el cliente
        .factory('JobService', JobService);

    JobService.$inject = ['$http','$rootScope'];
    function JobService($http, $rootScope) {

        var service = {};

        service.Create = Create;
        service.GetById = GetById;
        service.GetByEmployer = GetByEmployer;
        service.GetByTechnology = GetByTechnology;
        service.GetByName = GetByName;
        service.GetBidsById = GetBidsById;
        service.GetAllComments = GetAllComments;
        service.CommentCreate = CommentCreate;
        service.BidCreate = BidCreate;
        
        return service;

        function Create(createData) {
            var response=$http({
                method:"post",
                url:$rootScope.url+"host/Api/JobOffer/Create",
                data:createData
            });
            return response;    
        }

        function GetById( jobId){
            var response=$http({
                method:"post",
                url:$rootScope.url+"host/Api/JobOffer/GetById",
                data:{JobOfferId:jobId}
            });
            return response;
        }

        function GetByEmployer( idEmployer){
            var response=$http({
                method:"post",
                url:$rootScope.url+"host/Api/JobOffer/GetByEmployer",
                data:{JobOfferId:jobId}
            });
            return response;
        }    

        function GetByTechnology( technology){
            var response=$http({
                method:"post",
                url:$rootScope.url+"host/Api/JobOffer/GetByTechnology",
                data: {Technology:technology}
            });
            return response;
        }


        function GetByName(name){
            var response=$http({
                method:"post",
                url:$rootScope.url+"host/Api/JobOffer/GetByName",
                data: {JobOffer:name}
            });
            return response;
        }       

        function GetBidsById(jobOfferId){
            var response=$http({
                method:"post",
                url:$rootScope.url+"host/Api/JobOffer/GetBidsById",
                data: {JobOfferId:jobOfferId}
            });
            return response;
        }    

        function GetAllComments (jobOfferId){
            var response = $http({
                method:"post",
                url: $rootScope.url +"JobOfferComment/GetAll",
                data: {JobOfferId: jobOfferId}
            });
            return response;
        }
        
        function CommentCreate(dataCreate){
            var response =$http({
                method:"post",
                url:$rootScope.url+"JobOfferComment/Create",
                data:dataCreate
            });
            return response;
        }
        
        function BidCreate( data){
            var response= $http({
                method:"post",
                url:$rootScope.url+"Bid/Create",
                data:data
            });
            return response;
        }
    
    }
})();
