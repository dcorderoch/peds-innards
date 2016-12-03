(function () {
    'use strict';

    angular
        .module('app')
        .controller('CourseAreaController', CourseAreaController);

    CourseAreaController.$inject = ['$location',  'FlashService', 'CourseService', 'ProfileCourseService', 'RegService'];
    function CourseAreaController($location,  FlashService,  CourseService, ProfileCourseService, RegService) {
        var vm = this;

        vm.suggestProject = suggestProject;

        var Tecnologias = [];
        initController();

        function initController(){


            vm.courseData =ProfileCourseService.GetCourseData();
            vm.courseData.Carnet = ProfileCourseService.GetProfileData().Carnet
            //            vm.courseData.status=true;
            vm.gradeWidth = {'width': vm.courseData.Grade+'%'};  

            console.log( vm.courseData);

            loadTechnologies();
        }

        function loadTechnologies(){
            RegService.GetTechnologies()
                .then(function (response) {

                vm.technologies = response.data;

            },function(response){
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


            if (additional !=="" ){
                var array = additional.split(',');
            }

            var send = {ProjectName: name, Technologies: Tecnologias, EndDate:dateEnd, StartDate:dateStart, Description: description,  
                        CourseId: vm.courseData.CourseId, StudentUserId: vm.courseData.StudentUserId, OtherFiles:array }
            console.log(send);

            CourseService.ProjectPropose(send)
                .then(function(response){

                if (response.data.ReturnStatus =="1"){
                    FlashService.Success("Proyecto sugerido exitosamente", true);
                    $location.path('/studentprofile')
                }
                else
                    FlashService.Error("El proyecto no se ha podido sugerir");

            }, function(response){

                FlashService.Error("El proyecto no se ha podido sugerir");
            });

        }

    }
})();
