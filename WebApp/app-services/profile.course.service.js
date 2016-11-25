(function () {

    angular
        .module('app')          
        .factory('ProfileCourseService', ProfileCourseService);

    ProfileCourseService.$inject = ['$http','$rootScope', '$cookieStore'];
    function ProfileCourseService($http, $rootScope, $cookieStore) {

        var service = {};

        var courseData;

        service.GetCourseData = GetCourseData;
        service.SetCourseData = SetCourseData;

        return service;

        function GetCourseData( ){

            courseData= $cookieStore.get('currentCourseData') || {};
            return courseData;
        }

        function SetCourseData(data){

            courseData = data;
            $cookieStore.put('currentCourseData', courseData);
        }
    }

})();
