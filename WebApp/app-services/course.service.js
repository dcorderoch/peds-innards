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
        service.CreateWithFile = CreateWithFile;

        return service;


        /**
        * Description for GetCourseAsStudent Get data like 
        * grades, badges, comments for student
        * @private
        * @property undefined
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
        * Description for GetCourseAsProfessor
        * @private
        * @method GetCourseAsProfessor
        * @param {Object} id
        * @return {Object} description
        */
        function GetCourseAsProfessor(id)  {
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
        function Brag(data){
            var response = $http({
                method: "post",
                url: $rootScope.url+"Badge/Brag",
                data: data
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

        function GetAllBadges(data){
            var response = $http({
                method: "post",
                url: $rootScope.url+"Badge/GetAll",
                data: data
            });
            return response
        }

        function CreateWithFile(data) {
            var response = $http({
                method: "post",
                url: $rootScope.url+"Comment/CreateWithFile",
                data: data
            });
            return response    
        }

    }

})();
