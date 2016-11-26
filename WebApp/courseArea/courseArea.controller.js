(function () {
    'use strict';

    angular
        .module('app')
        .controller('CourseAreaController', CourseAreaController);

    CourseAreaController.$inject = ['$location',  'FlashService', '$rootScope', 'CourseService', 'ProfileCourseService', 'RegService'];
    function CourseAreaController($location,  FlashService, $rootScope, CourseService, ProfileCourseService, RegService) {
        var vm = this;

        vm.suggestProject = suggestProject;
        
        vm.technologies=[{ Technology:"Java", TechnologyId: "0"}, { Technology:"C++",TechnologyId: "1"}];
        var Tecnologias = [];
        initController();
        
        function initController(){

            vm.courseData =ProfileCourseService.GetCourseData();
            vm.courseData.Carnet = $rootScope.userData.Carnet;
            //            vm.courseData.status=true;
            vm.gradeWidth = {'width': vm.courseData.Grade+'%'};  


            loadTechnologies();


        }

        function loadTechnologies(){
            RegService.GetTechnologies()
                .then(function (response) {
                if (response.success) {
                    vm.technologies = response.data.technologies;
                } 
            },function(response){
                console.log("supongo1")
            });
        }

        vm.toggleSelectionTech = function toggleSelectionTech(technology) {
            var idx = Tecnologias.indexOf(technology.TechnologyId);

            // is currently selected
            if (idx > -1) {
                Tecnologias.splice(idx, 1);
            }

            // is newly selected
            else {
                Tecnologias.push(technology.TechnologyId);
            }
        };
        
       function suggestProject(name,dateStart,dateEnd,description,additional){
           
           var send = {Tech}
           console.log();
       }


    }
})();