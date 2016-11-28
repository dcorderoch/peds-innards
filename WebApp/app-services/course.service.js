(function () {

    angular
        .module('app')          
        .factory('CourseService', CourseService);

    CourseService.$inject = ['$http','$rootScope'];
    function CourseService($http, $rootScope) {

        var service = {};

        service.GetCourseAsStudent = GetCourseAsStudent;
        service.GetCourseAsProfessor = GetCourseAsProfessor;
        service.CreateCourse = CreateCourse;
        service.GetComments = GetComments;
        service.GetAllByUniversity = GetAllByUniversity;
        service.JoinCourse = JoinCourse;
        service.GetSpecificCourse = GetSpecificCourse;
        service.ProjectPropose = ProjectPropose;
        service.CommentCreate = CommentCreate;
        service.Brag = Brag;
        service.GiveBadge = GiveBadge

        return service;

        function GetCourseAsStudent(id) {
            var response=$http({
                method:"post",
                url:$rootScope.url+"Course/GetCourseAsStudent",
                data: {"CourseId":id}
            });
            return response;    
        }

        function GetCourseAsProfessor(id) {
            var response=$http({
                method:"post",
                url:$rootScope.url+"Course/GetCourseAsProfessor",
                data: {"CourseId":id}
            });
            return response;    
        }

        function CreateCourse(data) {
            var response=$http({
                method:"post",
                url:$rootScope.url+"Course/Create",
                data: data
            });
            return response;    
        }

        function GetComments(sharedAreaData){
            var response = $http({
                method:"post",
                url: $rootScope.url+"Comment/GetAll",
                data: sharedAreaData
            });
            return response 
        }

        function GetAllByUniversity(universityId){
            var response  = $http({
                method:"post",
                url:$rootScope.url+"Course/GetAllByUniversity",
                data: {UniversityId: universityId}
            });
            return response;
        }

        function JoinCourse(dataJoin){
            var response = $http({
                method: "post",
                url: $rootScope.url+"Course/Join",
                data: dataJoin
            });
            return response;
        }

        function GetSpecificCourse( getCourseData){
            var response = $http({
                method: "post",
                url: $rootScope.url+"Course/GetSpecificCourse",
                data: getCourseData
            });
            return response;
        }

        function ProjectPropose(data){
            var response = $http({
                method: "post",
                url: $rootScope.url+"Project/Propose",
                data: data
            });
            return response;
        }

        function CommentCreate(data){
            var response = $http({
                method: "post",
                url: $rootScope.url+"Comment/Create",
                data: data
            });
            return response;            
        }

        function Brag(badgeId){
            var response = $http({
                method: "post",
                url: $rootScope.url+"Bad-ge/Brag",
                data: {BadgeId: badgeId}
            });
            return response;                 
        }

        function GiveBadge(data){
            var response = $http({
                method: "post",
                url: $rootScope.url+"Badge/Give",
                data: data
            });
            return response;                 

        }

    }

})();
