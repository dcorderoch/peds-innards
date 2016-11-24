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

        return service;

        function GetCourseAsStudent(id) {
            var response=$http({
                method:"post",
                url:$rootScope.url+"Api/Course/GetCourseAsProfessor",
                data: {"CourseId":id}
            });
            return response;    
        }

        function GetCourseAsProfessor(id) {
            var response=$http({
                method:"post",
                url:$rootScope.url+"Api/Course/GetCourseAsStudent",
                data: {"CourseId":id}
            });
            return response;    
        }

        function CreateCourse(data) {
            var response=$http({
                method:"post",
                url:$rootScope.url+"Api/Course/Create",
                data: data
            });
            return response;    
        }

        function GetComments(sharedAreaData){
            var response = $http({
                method:"post",
                url: $rootScope.url+"/Comment/GetAll",
                data: sharedAreaData
            });
            return response 
        }
    }


})();
