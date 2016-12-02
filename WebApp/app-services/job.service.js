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
        service.CloseJob = CloseJob;
        service.Assign = Assign;
        service.GetProfile = GetProfile;

        return service;

        function Create(createData) {
            var response=$http({
                method:"post",
                url:$rootScope.url+"JobOffer/Create",
                data:createData
            });
            return response;    
        }

        function GetById( jobId ){
            var response=$http({
                method:"post",
                url:$rootScope.url+"JobOffer/GetById",
                data: {JobOfferId:jobId}
            });
            return response;
        }

        function GetByEmployer( jobId){
            var response=$http({
                method:"post",
                url:$rootScope.url+"JobOffer/GetByEmployer",
                data:{EmployerUserId:jobId}
            });
            return response;
        }    

        function GetByTechnology( technology){
            var response=$http({
                method:"post",
                url:$rootScope.url+"JobOffer/GetByTechnology",
                data: {Technology:technology}
            });
            return response;
        }


        function GetByName(name){
            var response=$http({
                method:"post",
                url:$rootScope.url+"JobOffer/GetByName",
                data: {JobOffer:name}
            });
            return response;
        }       

        function GetBidsById(jobOfferId){
            var response=$http({
                method:"post",
                url:$rootScope.url+"JobOffer/GetBidsById",
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

        function CloseJob(data){
            var response= $http({
                method:"post",
                url:$rootScope.url+"JobOffer/Close",
                data:data
            });
            return response;            
        }  
        
        function Assign(data){
            var response= $http({
                method:"post",
                url:$rootScope.url+"JobOffer/Assign",
                data:data
            });
            return response;            
        }
        
        function GetProfile(data){
            var response= $http({
                method:"post",
                url:$rootScope.url+"Student/GetProfile",
                data:data
            });
            return response;            
        }

    }
})();
