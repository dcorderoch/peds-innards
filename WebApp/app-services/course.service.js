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
        service.GiveBadge = GiveBadge;
        service.GetAllByProfessor = GetAllByProfessor;
        service.CloseCourse = CloseCourse;
        service.GetAllByStudent = GetAllByStudent;
        service.GetAllBadges = GetAllBadges;

        return service;

        /**
        *   Metodo para obtener un curso con toda su información excepto comentarios
        *
        */
        function GetCourseAsStudent(data) {
            var response=$http({
                method:"post",
                url:$rootScope.url+"Course/GetCourseAsStudent",
                data:data
            });
            return response;    
        }

        /**
        *   Metodo para obtener la lista de estudiantes 
        *
        */
        function GetCourseAsProfessor(id) {
            var response=$http({
                method:"post",
                url:$rootScope.url+"Course/GetCourseAsProfessor",
                data: {"CourseId":id}
            });
            return response;    
        }

        /**
        *   
        *
        */
        function CreateCourse(data) {
            var response=$http({
                method:"post",
                url:$rootScope.url+"Course/Create",
                data: data
            });
            return response;    
        }

        /**
        *   
        *
        */
        function GetComments(sharedAreaData){
            var response = $http({
                method:"post",
                url: $rootScope.url+"Comment/GetAll",
                data: sharedAreaData
            });
            return response 
        }  

        /**
        *   
        *
        */
        function CloseCourse(courseId){
            var response = $http({
                method:"post",
                url: $rootScope.url+"Course/Close",
                data: {CourseId: courseId}
            });
            return response 
        }

        /**
        *   
        *
        */
        function GetAllByUniversity(universityId){
            var response  = $http({
                method:"post",
                url:$rootScope.url+"Course/GetAllByUniversity",
                data: {UniversityId: universityId}
            });
            return response;
        }

        /**
        *   
        *
        */
        function GetAllByProfessor(profUserId){
            var response  = $http({
                method:"post",
                url:$rootScope.url+"Course/GetAllByProfessor",
                data: {ProfUserId: profUserId}
            });
            return response;
        }

        /**
        *   
        *
        */
        function GetAllByStudent(studentUserId){
            var response  = $http({
                method:"post",
                url:$rootScope.url+"Course/GetAllByStudent",
                data: {StudentUserId: studentUserId}
            });
            return response;
        }

        /**
        *   
        *
        */
        function JoinCourse(dataJoin){
            var response = $http({
                method: "post",
                url: $rootScope.url+"Course/Join",
                data: dataJoin
            });
            return response;
        }

        /**
        *   
        *
        */
        function GetSpecificCourse( getCourseData){
            var response = $http({
                method: "post",
                url: $rootScope.url+"Course/GetSpecificCourse",
                data: getCourseData
            });
            return response;
        }

        /**
        *   
        *
        */
        function ProjectPropose(data){
            var response = $http({
                method: "post",
                url: $rootScope.url+"Project/Propose",
                data: data
            });
            return response;
        }

        /**
        *   
        *
        */
        function CommentCreate(data){
            var response = $http({
                method: "post",
                url: $rootScope.url+"Comment/Create",
                data: data
            });
            return response;            
        }

        /**
        *   
        *
        */
        function Brag(badgeId){
            var response = $http({
                method: "post",
                url: $rootScope.url+"Bad-ge/Brag",
                data: {BadgeId: badgeId}
            });
            return response;                 
        }

        /**
        *   
        *
        */
        function GiveBadge(data){
            var response = $http({
                method: "post",
                url: $rootScope.url+"Badge/Give",
                data: data
            });
            return response;                 

        }
        
        function GetAllBadges(data){
            var response = $http({
                method: "post",
                url: $rootScope.url+"Badge/GetAll",
                data: data
            });
            return response
        }

    }

})();
