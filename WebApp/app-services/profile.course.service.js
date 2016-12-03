(function () {

    angular
        .module('app')          
        .factory('ProfileCourseService', ProfileCourseService);

    ProfileCourseService.$inject = ['$http','$rootScope', '$cookieStore'];
    function ProfileCourseService($http, $rootScope, $cookieStore) {

        var service = {};

        var courseData;
        var workData;
        var userData;
        var userData2; 

        service.GetCourseData = GetCourseData;
        service.SetCourseData = SetCourseData;
        service.GetWorkData = GetWorkData;
        service.SetWorkData = SetWorkData;
        service.GetProfileData = GetProfileData;
        service.SetProfileData = SetProfileData;
        service.SetProfileData2 = SetProfileData2
        service.GetProfileData2 = GetProfileData2

        return service;

        function GetCourseData( ){

            courseData= $cookieStore.get('currentCourseData') || {};
            return courseData;
        }

        function SetCourseData(data){

            courseData = data;
            $cookieStore.put('currentCourseData', courseData);
        }

        
        
        function GetWorkData(){

            workData = $cookieStore.get('currentWorkData') || {};
            return workData;
        }

        function SetWorkData(data){

            workData = data;
            $cookieStore.put('currentWorkData',workData) || {}
        }

        
        
        function SetProfileData(data){

            userData = data;
            $cookieStore.put('userData', data);
        }

        function GetProfileData(){

            userData = $cookieStore.get('userData') || {};
            return userData;
        }

        
        
        
        function SetProfileData2(data){

            userData2 = data;
            $cookieStore.put('userData2', data);
        }

        function GetProfileData2(){

            userData2 = $cookieStore.get('userData2') || {};
            return userData2;
        }
    }

})();
