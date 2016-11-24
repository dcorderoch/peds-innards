(function () {
    'use strict';

    angular
        .module('app')
        .controller('SharedAreaController', SharedAreaController);

    SharedAreaController.$inject = ['$location',  'FlashService', '$rootScope', 'CourseService'];
    function SharedAreaController($location,  FlashService, $rootScope, CourseService) {
        var vm = this;

        vm.comments = [];
        vm.edu ={las:false};
        
        initController();
        function initController(){

            vm.courseData = {};
            console.log($rootScope.currentCourseData);
            //            vm.courseData = $rootScope.currentCourseData;
            vm.courseData.status=true;
            //            vm.gradeWidth = {'width': vm.courseData.grade+'%'};  
            //            vm.courseData.grade = vm.courseData;
            getComments();
        }


        function getComments (){

            CourseService.GetComments()
                .then( function(response){

                comments = response.data;

            }, function(response){
                console.log("no sirvió")
            })
        }


    }

})();
